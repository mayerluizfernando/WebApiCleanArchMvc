using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Infra.IoC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApiCleanArchMvc.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] //Somente com token válido
    public class CategoriesController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var result = await _categoryService.GetCategories();
        //    return Ok(result);
        //}
        //*******************************************************
        
        
        
        //https://jasonwatmore.com/post/2022/01/19/net-6-create-and-validate-jwt-tokens-use-custom-jwt-middleware
        // verifica se o Bearer Token é Válido, possivelmente em CleanArchMvc.Infra.IoC, porem verificar 
        //TODO Refator levando para area utils 
        public static bool VerifyToken(string token)
        {
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InVzZXJAZXhhbXBsZS5jb20iLCJtZXV2YWxvciI6ImRjYzc4MGY2LTJmMzMtMTFlZS1iZTU2LTAyNDJhYzEyMDAwMiIsImp0aSI6Ijg2MDliMWJkLTVhYzAtNDNmOS1hZTg4LWEyNWFiNzM1Nzg0NiIsImV4cCI6MTY5MDkzMDE1NSwiaXNzIjoidGVzdGUubmV0IiwiYXVkIjoiaHR0cDovL3Rlc3RlLm5ldCJ9.nMtqLYqZm79ZBRjAQFcqtZoiLVGYebT2QoZWMDnQYSU";
            try
            {
                string[] onlyToken = token.Split(' ');
                token = onlyToken[1].ToString();
            }
            catch (Exception e)
            {
                return false;
            }
            var mySecret = "jkkH AH SJKGHDSDjhgjg7867868769&$%*****45";
            //var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mySecret));

            var myIssuer = "teste.net";
            var myAudience = "http://teste.net";
            
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = mySecurityKey,
                ValidAudience = myAudience,
                ValidIssuer = myIssuer,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch(SecurityTokenException)
            {
                return false; 
            }
            catch(Exception e)
            { 
                //log(e.ToString()); //something else happened
                Debug.WriteLine(e.ToString()); //something else happened
                throw;
            }
            //... manual validations return false if anything untoward is discovered
            return validatedToken != null;
        }
        //*******************************************************
        [HttpGet]
        //[Authorize] //Somente com token válido
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {

            //var x = new UtilConfigApp();
            //var ModuleTitle = x.ModuleTitle;
            //var BundlingActive = x.BundlingActive;
            //
            var ModuleTitle = UtilConfigApp.ModuleTitle;


            var Explode = UtilConfigApp.Setting("HML:parametro1");
            
            
            //#### Teste LFernando
            var secretKey = "jkkH AH SJKGHDSDjhgjg7867868769&$%*****45"; //appsettings.json 
            var Issuer = "teste.net"; //appsettings 
            var Audience = "http://teste.net";  //appsettings
            string xyzHeader = Request.Headers["Authorization"];
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var kxy = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true, 
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Issuer,       //appsettings
                ValidAudience = Audience,   //appsettings
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey)), //appsettings
                ClockSkew = TimeSpan.Zero   //Sem tempo adicional na expiração do token 
            };

            //Deu pau tokenHandler.ValidateToken(xyzHeader,kxy, out var jksdfjh);
            var klm = VerifyToken(xyzHeader);
            if (!klm)
            {
                return Unauthorized(new { MessageError = "Token inválido. Ooohhh fucking so dick ... man" } );
            }
            
            //#### Teste LFernando
            
            var categories = await _categoryService.GetCategories();
            if (categories == null)
            {
                return NotFound("Categories Not Found.");
            }
            return Ok(categories);
        }
        //
        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category Not Found.");
            }
            return Ok(category);
        }
        //
        [HttpPost]
        
        public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Invalid Data.");
            } else
            {
                var k = await _categoryService.Add(categoryDto);

                //retorna statusCode 201, chamando o Get GetCategory
                return new CreatedAtRouteResult("GetCategory", new { id = k.Id }, k);
                // OkObjectResult(new Foo { Id = id }) ***Não pode usar no post, somente no get 
            }
            //retorna statusCode 201, chamando o Get GetCategory
            //return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.Id }, categoryDTO);

            
        }



    }
}
