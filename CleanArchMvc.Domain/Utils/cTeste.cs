using Microsoft.Extensions.Configuration;

namespace CleanArchMvc.Domain.Utils;

public static class cTeste
{
    private static string _moduleTitle;
    private static bool? _bundlingActive;
 
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