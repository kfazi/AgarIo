namespace AgarIo.Server
{
    using AgarIo.Server.Connections;

    using Westwind.Utilities.Configuration;

    public class AppSettings : AppConfiguration, IConnectionSettings, IAdminCredentials
    {
        public int Port { get; set; }

        public string AdminLogin { get; set; }

        public string AdminPassword { get; set; }

        protected override IConfigurationProvider OnCreateDefaultProvider(string sectionName, object configData)
        {
            var provider = new ConfigurationFileConfigurationProvider<AppSettings>();
            return provider;
        }

        public static AppSettings Create()
        {
            var appSettings = new AppSettings();
            appSettings.Initialize();
            return appSettings;
        }
    }
}