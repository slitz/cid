
namespace ChargerID.Configuration
{
    public interface IConfigNrel
    {
        string Url { get; }
        string FuelType { get; }
        string Radius { get; }
        string ApiKey { get; }
    }

    public class ConfigNrel : IConfigNrel
    {
        private IConfigurationDataProvider _configurationDataProvider;
        private const string SettingNameBase = "nrel";
        public const string UrlAttribute = "url";
        public const string FuelTypeAttribute = "fuelType";
        public const string RadiusAttribute = "radius";
        public const string ApiKeyAttribute = "apiKey";

        public ConfigNrel(IConfigurationDataProvider configurationDataProvider)
        {
            this._configurationDataProvider = configurationDataProvider;
        }

        public string Url
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, UrlAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string FuelType
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, FuelTypeAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string Radius
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, RadiusAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string ApiKey
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, ApiKeyAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }
    }
}
