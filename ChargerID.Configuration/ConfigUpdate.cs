
namespace ChargerID.Configuration
{
    public interface IConfigUpdate
    {
        string NrelStationsUrl { get; }
    }

    public class ConfigUpdate : IConfigUpdate
    {
        private IConfigurationDataProvider _configurationDataProvider;
        private const string SettingNameBase = "update";
        public const string NrelStationsUrlAttribute = "nrelStationsUrl";

        public ConfigUpdate(IConfigurationDataProvider configurationDataProvider)
        {
            this._configurationDataProvider = configurationDataProvider;
        }

        public string NrelStationsUrl
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, NrelStationsUrlAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }
    }
}
