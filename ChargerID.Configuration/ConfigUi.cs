
namespace ChargerID.Configuration
{
    public interface IConfigUi
    {
        string Username { get; }
        string Password { get; }
    }

    public class ConfigUi : IConfigUi
    {
        private IConfigurationDataProvider _configurationDataProvider;
        private const string SettingNameBase = "ui"; 
        public const string UsernameAttribute = "username";
        public const string PasswordAttribute = "password";

        public ConfigUi(IConfigurationDataProvider configurationDataProvider)
        {
            this._configurationDataProvider = configurationDataProvider;
        }

        public string Username
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, UsernameAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string Password
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, PasswordAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

    }
}
