using CleanArchMvc.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCleanArchMvc.Controllers
{

    //Teste/Simulação de como ficaria a arquitetura com acesso a serviços REST´s externos
    [Route("api/[controller]")]
    [ApiController]
    public class InfraPayController : Controller
    {
        [Produces("application/json")]
        [HttpGet]
        public ActionResult Get()
        {
            string jsonData = "{ \"FirstName\":\"Jignesh\",\"LastName\":\"Trivedi\" }";
            //var categories = await _categoryService.GetCategories();
            //if (categories == null)
            //{
            //    return NotFound("Categories Not Found.");
            //}

            //proximo passo fazer o teste implementando a camada de application.9.ExternalServices
            return Ok(jsonData);
        }
    }
}
