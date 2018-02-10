using Data.ConfigDataProvider;
using Jespers.Config;
using System.Web.Http;
using Data.ConfigDataProvider.Classes;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Jespers.Mappings.RegisterMappings.Register();
            RegisterConfig();
        }

        private void RegisterConfig()
        {
            AppConfig.Provider = new RegisterConfig().Provider();



            var x = AppConfig.Provider.Get<SolarSystem>();

            var z = 1;
        }
    }
}
