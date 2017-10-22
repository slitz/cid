using System;
using System.Collections.Generic;
using System.Linq;

namespace ChargerID.Configuration
{
    public interface IConfigurationDataProvider
    {
        string GetStringValue(string name, string defaultValue);
        long GetLongValue(string name, long? defaultValue);
        int GetIntValue(string name, int? defaultValue);
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

        public int GetIntValue(string name, int? defaultValue)
        {
            string defaultStringValue = defaultValue != null ? defaultValue.ToString() : null;
            string stringValue = GetStringValue(name, defaultStringValue);

            int result;
            if (!int.TryParse(stringValue, out result))
            {
                throw new InvalidCastException(string.Format("Value for setting name '{0}' cannot convert value '{1}' to an Int32 value.", name, stringValue));
            }

            return result;
        }

        public long GetLongValue(string name, long? defaultValue)
        {
            string defaultStringValue = defaultValue != null ? defaultValue.ToString() : null;
            string stringValue = GetStringValue(name, defaultStringValue);

            long result;
            if (!long.TryParse(stringValue, out result))
            {
                throw new InvalidCastException(string.Format("Value for setting name '{0}' cannot convert value '{1}' to an Int32 value.", name, stringValue));
            }

            return result;
        }
    }
}
