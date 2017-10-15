using Data.ConfigDataProvider.Classes;
using Jespers.Config.Interfaces;
using Jespers.Config.Providers;

namespace Data.ConfigDataProvider
{
    public class RegisterConfig
    {
        public IConfigProvider Provider()
        {
            IConfigProvider provider = new ConfigProvider();
            provider.Add<SolarSystem>("bin/Config/SolarSystem.jsonconfig");
            return provider;
        }
    }
}
