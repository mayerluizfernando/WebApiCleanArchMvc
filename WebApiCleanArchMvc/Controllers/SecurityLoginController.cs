using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiCleanArchMvc.Jwt.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

//https://www.infoworld.com/article/3669188/how-to-implement-jwt-authentication-in-aspnet-core-6.html
//https://renatogroffe.medium.com/net-6-asp-net-core-jwt-swagger-implementando-a-utiliza%C3%A7%C3%A3o-de-tokens-5d04cda20fa8

/*JWT Tokens
 *JWT Token possui 3 partes distintas sendo header.payload.signature 
 * 1.)Header - Contém o tipo de token e a criptografia utilizada. O algoritimo HASH usado pode ser: HMAC, SHA256 ou RSA
 *      Exemplo: { "alg":"HS256", "typ":"JWT"}
 *      HS256 é o combinado entre HMAC + SHA256
 *
 * 2.)Payload - É o corpo do JWT e contém as claims que são as declarações sobre uma entidade(usuário) e dados adicionais (metadados).
 * Existem 3 tipos de claims, sendo: 1-registradas(reserved), 2-públicas(public) e 3-privadas(private)
 *      Exemplo: {"inss":"199.0.0.13","exp":"14681938","user":"ffigo","IsAdmin":"true"}
 *
 * 3.)Signature - Assinatura utilizada na validação do token usando uma chave secreta.
 * Exemplo: 122884reuir7f.reoo93&534.0349Tgr4%$#490
 *
 * Usando o algoritimo definido no Header, o Header e o Payload são concatenados e assinados usando uma chave secreta.
 * A assinatura é anexada ao final do token que pode ser usado (por quem tiver a chave secreta) para verificar se o emissor do Token JWT
 * é quem ele afirma ser, e se o Toekn é váliado e não expirou.
 *
 * header.payload.signature
 * Exemplo: eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOiJhYmNkMTIzIiwiZXhwaXJ5IjoxNjQ2NjM1NjExMzAxfQ.3Thp81rDFrKXr3WrY1MyMnNK8kKoZBX9lg-JwFznR-M
 * Onde:
 * 1.)Header=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9
 * 2.)Payload=eyJ1c2VySWQiOiJhYmNkMTIzIiwiZXhwaXJ5IjoxNjQ2NjM1NjExMzAxfQ
 * 3.)Signature=3Thp81rDFrKXr3WrY1MyMnNK8kKoZBX9lg-JwFznR-M
 *
 *Packages:
 * Microsoft.AspNetCore.Authentication.JwtBearer
 *  Midleware asp.net core que permite que um aplciativo receba um token 'Bearer OpendId Connect';
 *  Contém tipos que habilitam o suporte para a autenticação baseada no bearer JWT;
 *      https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.jwtbearer?view=aspnetcore-6.0
 * 
 * System.IdentityModel.Tokens
 *  https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens?view=dotnet-plat-ext-7.0&viewFallbackFrom=net-6.0
 *  Inclui tipos que fornecem suporte para SecurityTokens, operações criptográficas: assinatura, verificação de assinaturas, criptografia.
 * 
 *  SymmetricSecurityKey
 *      Classe base abstrata para todas as chaves geradas usando algoritimos simétricos
 *      https://learn.microsoft.com/pt-br/dotnet/api/system.identitymodel.tokens.symmetricsecuritykey?view=dotnet-plat-ext-7.0&viewFallbackFrom=net-6.0
 * 
 *  SigningCredentials
 *      Representa a chave de criptografia e os algoritimos de segurança usados p/ gerar a assinatura digital
 *      https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.signingcredentials?view=netframework-4.8.1&viewFallbackFrom=net-6.0
 *
 *  SecurityAlgorithms
 *      Representam os algoritmos criptográficos usados para realizar a criptografia do XML e assinaturas
 *      https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.securityalgorithms?view=dotnet-plat-ext-7.0&viewFallbackFrom=net-6.0
 *
 * System.IdentityModel.Tokens.Jwt
 *  https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt?view=msal-web-dotnet-latest
 *  Inclui tipos que fornecem suporte para criar, serializar e validar JSON Web Tokens
 *
 *  JwtSecurityToken
 *  Usada para representar um JSON Web Token
 *  https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt.jwtsecuritytoken?view=msal-web-dotnet-latest
 *
 *  JwtSecurityTokenHandler
 *  Usada para criar e validar um jSON Web Token
 *  https://learn.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt.jwtsecuritytokenhandler?view=msal-web-dotnet-latest
 * 
 *  Outros:
 *  Microsoft.AspNetCore.Identity
 *  Microsoft.AspNetCore.Identity.EntityFrameworkCore
 *
 * Processo em 3 Passos (*seção 11.97)
 *  - Geração do Token
 *      - verifica credenciais dp usuário
 *      - define as claims do usuário (nome, email, etc ...)
 *      - define a chave secreta e o algoritmo de encriptação de dados
 *      - gera o token com base no emissor, audiência, claims, e define a data de expiração
 * 
 *  - Validação do Token
 *      - valida emissor
 *      - valida audiência
 *      - valida a assinatura com a chave secreta
 * 
 *  - Ativação da Autenticação Jwt e da segurança de API´s
 *
 * Alterações/Implementações Ref Bearer Token/Tokenização (a*seção11.98/99)
 *
 * WebApiCleanArchMvc
 * CleanArchMvc.Infra.IoC
 */

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

    [HttpPost(Name = "LoginUser")] //A*11-101
    [AllowAnonymous]
    //[Produces("application/json")]
    //[Consumes("application/json")] 
    //https://www.c-sharpcorner.com/article/understanding-produces-and-consumes-attribute-in-mvc-6/
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //Não Implementado - consome metodo de validação e autenticação de usuário (A*11/100)
        //se usuário não existe return BadRequest
        //Se result Pode Gerar o Token
        var token = GenerateToken(userInfo);

        //return Ok(token);
        //return Created("Token Criado com sucesso", token);
        return token;
        
        //return  Ok(new { id = 1, name = "Juka" });
        return Ok(userInfo);
    }
  
    
    
    private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
    {
        //declarações/propriedades do usuário 
        var claims = new[]
        {
            new Claim("email", userInfo.Email),
            new Claim("meuvalor", "dcc780f6-2f33-11ee-be56-0242ac120002"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        //gerar chave privada para assinar o token 
        var secretKey = "jkkH AH SJKGHDSDjhgjg7867868769&$%*****45"; //appsettings.json 
        var privateKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(secretKey));
        
        //gerar assinatura digital do Token 
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
        
        //definir o tempo de expiração do Token 
        var expiration = DateTime.UtcNow.AddMinutes(1);
        
        //gerar Token 
        var Issuer = "teste.net"; //appsettings 
        var Audience = "http://teste.net";  //appsettings
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: Issuer,
            audience:  Audience,
            claims: claims, 
            expires: expiration,
            signingCredentials: credentials 
            
            );

        var result = new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };

        //return Ok(result);
        return result;

        //return Ok("181d0e58-2f31-11ee-be56-0242ac120002");
        //throw new NotImplementedException();
    }
}