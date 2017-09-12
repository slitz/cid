using System.Collections.Generic;
using System.Linq;

namespace ChargerID.Configuration
{
    public interface IConfigurationDataProvider
    {
        string GetStringValue(string name, string defaultValue);
    }

    class ConfigurationDataProvider : IConfigurationDataProvider
    {
        private readonly IConfigurationWrapper _configurationWrapper;
        private List<AppConfiguration> _configurationItems;

        public ConfigurationDataProvider(IConfigurationWrapper configurationWrapper = null)
        {
            _configurationWrapper = configurationWrapper ?? new ConfigurationWrapper();
            Initialize();
        }

        public void Initialize(string machineNameOverride = null)
        {
            _configurationItems = _configurationWrapper.GetAllConfigurationItems().ToList();

        }

        public string GetStringValue(string name, string defaultValue)
        {
            string nameLower = name.ToLower();

            var item = _configurationItems.FirstOrDefault(r => r.Name.ToLower() == nameLower);
            if (item != null)
            {
                return item.Value;
            }

            return defaultValue;
        }
    }
}
