using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//https://www.infoworld.com/article/3669188/how-to-implement-jwt-authentication-in-aspnet-core-6.html
//https://renatogroffe.medium.com/net-6-asp-net-core-jwt-swagger-implementando-a-utiliza%C3%A7%C3%A3o-de-tokens-5d04cda20fa8


namespace WebApiCleanArchMvc.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SecurityLoginController : Controller
{
    //[HttpGet]
    //[HttpGet("{id:int}", Name = "GetToken")]
    [HttpGet(Name = "GetToken")]
    [AllowAnonymous]
    
    public IActionResult GetToken()
    {
        //return View();
        return Ok(new {Id=12, Teste="Teste"});
    }
}