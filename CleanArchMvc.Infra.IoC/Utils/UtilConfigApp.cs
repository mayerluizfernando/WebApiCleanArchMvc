using Microsoft.Extensions.Configuration;

//Dependências 
//Microsoft.Extensions.Configuration.Abstractions 
//Microsoft.Extensions.Configuration.Json
//Microsoft.Extensions.Configuration


namespace CleanArchMvc.Infra.IoC.Utils;

public static class UtilConfigApp
{
    private static string _moduleTitle;
    private static bool? _bundlingActive;
 
    
    //#### Teste L.Fernando
    //http://www.developerslearnit.com/2022/07/access-configuration-settings-in.html
    private static IConfiguration _configuration;
    public static void AppSettingsConfigure(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string Setting(string Key)
    {
        return _configuration.GetSection(Key).Value;
        //return "123456";
    }
    
    
    //#### Teste L.Fernando
    
    
    public static string ModuleTitle
    {
        get
        {

            //var smtp = new Configuration();
            // System.Configuration.Configuration config =
            //     ConfigurationManager.OpenExeConfiguration(
            //         ConfigurationUserLevel.None);
            
            
            //https://www.macoratti.net/20/10/aspc_uconfg1.htm
            
            string c = Directory.GetCurrentDirectory();
            //IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
            
            string xKey = configuration.GetSection("HML").GetSection("parametro1").Value; 
            string xKey1 = configuration.GetSection("xKey").Value;
            
            var xpar1 = configuration["HML:parametro1"];

            if (string.IsNullOrEmpty(_moduleTitle))
                //_moduleTitle = ConfigurationManager.AppSettings["ModuleTitle"];
                //_moduleTitle = "####456";
                //config.Sections["CustomSection"]
                //_moduleTitle = config.AppSettings["xKey"];
                _moduleTitle = xpar1;
            
                //configuration["ConnectionStrings:NorthwindDatabase"];
            return _moduleTitle;
        }
    }
 
    public static bool BundlingActive
    {
        get
        {
            if (!_bundlingActive.HasValue)
                //_bundlingActive = ConfigurationManager.AppSettings["BundlingActive"] == "1";
                _bundlingActive = false;
            return _bundlingActive.Value;
        }
    }
}