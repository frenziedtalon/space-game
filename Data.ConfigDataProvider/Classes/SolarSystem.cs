namespace Data.ConfigDataProvider.Classes
{
    public class SolarSystem
    {
        public ConnectionStringsConfig ConnectionStrings { get; set; }
        public ApiSettingsConfig ApiSettings { get; set; }

        public class ConnectionStringsConfig
        {
            public string MyDb { get; set; }
        }

        public class ApiSettingsConfig
        {
            public string Url { get; set; }
            public string ApiKey { get; set; }
            public bool UseCache { get; set; }
        }
    }
}
