namespace AgarIo.AdminPanel
{
    using Westwind.Utilities.Configuration;

    public class AppSettings : AppConfiguration
    {
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