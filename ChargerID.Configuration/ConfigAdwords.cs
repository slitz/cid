
namespace ChargerID.Configuration
{
    public interface IConfigAdwords
    {
        string DeveloperToken { get; }
        string ClientAccount { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string RefreshToken { get; }
        string UserAgent { get; }
    }

    public class ConfigAdwords : IConfigAdwords
    {
        private IConfigurationDataProvider _configurationDataProvider;
        private const string SettingNameBase = "adwords";
        public const string DeveloperTokenAttribute = "developerToken";
        public const string ClientAccountAttribute = "clientAccount";
        public const string ClientIdAttribute = "clientId";
        public const string ClientSecretAttribute = "clientSecret";
        public const string RefreshTokenAttribute = "refreshToken";
        public const string UserAgentAttribute = "userAgent";

        public ConfigAdwords(IConfigurationDataProvider configurationDataProvider)
        {
            this._configurationDataProvider = configurationDataProvider;
        }

        public string DeveloperToken
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, DeveloperTokenAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string ClientAccount
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, ClientAccountAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string ClientId
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, ClientIdAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string ClientSecret
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, ClientSecretAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string RefreshToken
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, RefreshTokenAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string UserAgent
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, UserAgentAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }
    }
}
