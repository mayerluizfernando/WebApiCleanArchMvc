using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
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

        [HttpGet]
        [Authorize] //Somente com token válido
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
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
