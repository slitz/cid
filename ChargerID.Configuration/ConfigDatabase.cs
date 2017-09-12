
namespace ChargerID.Configuration
{
    public interface IConfigDatabase
    {
        string Test { get; }
        string TestName { get; }
    }

    public class ConfigDatabase : IConfigDatabase
    {
        private IConfigurationDataProvider _configurationDataProvider;
        private const string SettingNameBase = "test"; 
        public const string TestAttribute = "test";
        public const string TestNameAttribute = "testname";

        public ConfigDatabase(IConfigurationDataProvider configurationDataProvider)
        {
            this._configurationDataProvider = configurationDataProvider;
        }

        public string Test
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, TestAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public string TestName
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, TestNameAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

    }
}
