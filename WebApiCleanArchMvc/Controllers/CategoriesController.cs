using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCleanArchMvc.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
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
        
        public async Task<ActionResult> Post([FromBody] CategoryDto categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid Data.");
            } else
            {
                var k = await _categoryService.Add(categoryDTO);

                //retorna statusCode 201, chamando o Get GetCategory
                return new CreatedAtRouteResult("GetCategory", new { id = k.Id }, k);
                // OkObjectResult(new Foo { Id = id }) ***Nãop pode usar no post, somente no get 
            }
            //retorna statusCode 201, chamando o Get GetCategory
            //return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.Id }, categoryDTO);

            
        }



    }
}
