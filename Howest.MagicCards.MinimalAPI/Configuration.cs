namespace Howest.MagicCards.MinimalAPI
{
    public class Configuration
    {
        const string _defaultSettingsFile = "appsettings.json";

        private static IConfigurationRoot GetConfiguration(string? settingsFile = null)
        {
            IConfigurationBuilder confbuilder =
                    new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(settingsFile ?? _defaultSettingsFile, optional: false, reloadOnChange: true);

            return confbuilder.Build();
        }

        public static string GetSetting(string key)
        {
            IConfigurationRoot appSettingsRoot = GetConfiguration();

            return appSettingsRoot.GetSection(key).Value;
        }
    }
}
