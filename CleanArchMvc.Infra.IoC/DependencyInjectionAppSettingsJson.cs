using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.Infra.IoC;

public static class DependencyInjectionAppSettingsJson
{

    public static IServiceCollection AddInfraStructureAppSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        //Material Ref. Dependency Injection
        //http://www.developerslearnit.com/2022/07/access-configuration-settings-in.html
        //https://www.youtube.com/watch?v=W3CZNbvOGcw
        //https://www.youtube.com/watch?v=bw-OlSchyWU
        //Existem basicamente 3 maneiras diferentes de implementar a DI (Dependency Injection)
        // Sendo:
        //1.) p/ construtor, 2.) p/ anotação e 3.) p/ provider/ServiceProvider  
            
        //para incluir um serviço de Help p/ o AppSettings.json
        //[]criar Interface em        CleanArchMvc.Application/Interfaces
        //[✓]criar Classe Abstrata em  CleanArchMvc.Application/Services
        //[✓]Disponibilizar o serviço na coleção de serviços IServiceCollection services.AddScoped<...>
        //exemplo services.AddScoped<IProductRepository, ProductRepository>();
        
        
        
        return services;
    }

}