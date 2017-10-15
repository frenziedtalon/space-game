using Data.ConfigDataProvider;
using Jespers.Config;
using System.Web.Http;

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
        }
    }
}
