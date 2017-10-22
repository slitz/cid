﻿
namespace ChargerID.Configuration
{
    public interface IConfigUpdate
    {
        string NrelStationsUrl { get; }
        string AdwordsTargetsUrl { get; }
        long AdwordsCampaignId { get; }
        int MaxAdwordsTargets { get; }
    }

    public class ConfigUpdate : IConfigUpdate
    {
        private IConfigurationDataProvider _configurationDataProvider;
        private const string SettingNameBase = "update";
        public const string NrelStationsUrlAttribute = "nrelStationsUrl";
        public const string AdwordsTargetsUrlAttribute = "adwordsTargetsUrl";
        public const string AdwordsCampaignIdAttribute = "adwordsCampaignId";
        public const string MaxAdwordsTargetsAttribute = "maxAdwordsTargets";

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

        public string AdwordsTargetsUrl
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, AdwordsTargetsUrlAttribute);
                return _configurationDataProvider.GetStringValue(name, defaultValue: null);
            }
        }

        public long AdwordsCampaignId
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, AdwordsCampaignIdAttribute);
                return _configurationDataProvider.GetLongValue(name, defaultValue: null);
            }
        }

        public int MaxAdwordsTargets
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, MaxAdwordsTargetsAttribute);
                return _configurationDataProvider.GetIntValue(name, defaultValue: null);
            }
        }
    }
}
