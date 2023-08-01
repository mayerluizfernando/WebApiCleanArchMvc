using System.Text;
using CleanArchMvc.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//#### Custom Change LFernando 
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddInfrastructureAPI(configuration);
//#### Custom Change LFernando


//#### Custom Change LFernando - Configurar a validação ao Token Bearer A*11/103
//TODO Importar para dentro do projeto CleanArchMvc.Infra.IoC, classe DependencyInjectionJwt.cs
//Microsoft.AspNetCore.Authentication.JwtBearer
//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-6.0

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
//         options => builder.Configuration.Bind("JwtSettings", options))
//     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//         options => builder.Configuration.Bind("CookieSettings", options));

var secretKey = "jkkH AH SJKGHDSDjhgjg7867868769&$%*****45"; //appsettings.json 
var Issuer = "teste.net"; //appsettings 
var Audience = "http://teste.net";  //appsettings

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
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
    });
//#### Custom Change LFernando

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//#### Custom Change LFernando
//Configurar o Swagger para inputar o token
//Incluir pacotes necessários do Swagger 
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using #### custom text"
        
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
//#### Custom Change LFernando

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//#### Custom Change LFernando
app.UseStatusCodePages();
app.UseAuthentication();
//#### Custom Change LFernando

app.UseAuthorization();

app.MapControllers();

app.Run();
