
using System;
namespace ChargerID.Configuration
{
    public interface IConfigUpdate
    {
        string NrelStationsUrl { get; }
        string AdwordsTargetsUrl { get; }
        long AdwordsCampaignId { get; }
        int MaxAdwordsTargets { get; }
        bool EnableLocationIndicatorDataRefresh { get; }
        bool EnableCampaignUpdate { get; }
        DateTime LastRunDateTime { get; }
        int RunIntervalDays { get; }
        DateTime NextRunDateTime { get; }
    }

    public class ConfigUpdate : IConfigUpdate
    {
        private IConfigurationDataProvider _configurationDataProvider;
        private const string SettingNameBase = "update";
        public const string NrelStationsUrlAttribute = "nrelStationsUrl";
        public const string AdwordsTargetsUrlAttribute = "adwordsTargetsUrl";
        public const string AdwordsCampaignIdAttribute = "adwordsCampaignId";
        public const string MaxAdwordsTargetsAttribute = "maxAdwordsTargets";
        public const string EnableLocationIndicatorDataRefreshAttribute = "enableLocationIndicatorDataRefresh";
        public const string EnableCampaignUpdateAttribute = "enableCampaignUpdate";
        public const string LastRunDateTimeAttribute = "lastRunDateTime";
        public const string RunIntervalDaysAttribute = "runIntervalDays";
        public const string NextRunDateTimeAttribute = "nextRunDateTime";

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

        public bool EnableLocationIndicatorDataRefresh
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, EnableLocationIndicatorDataRefreshAttribute);
                return _configurationDataProvider.GetBoolValue(name, defaultValue: null);
            }
        }

        public bool EnableCampaignUpdate
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, EnableCampaignUpdateAttribute);
                return _configurationDataProvider.GetBoolValue(name, defaultValue: null);
            }
        }

        public DateTime LastRunDateTime
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, LastRunDateTimeAttribute);
                return _configurationDataProvider.GetDateTimeValue(name, defaultValue: null);
            }
        }

        public int RunIntervalDays
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, RunIntervalDaysAttribute);
                return _configurationDataProvider.GetIntValue(name, defaultValue: null);
            }
        }

        public DateTime NextRunDateTime
        {
            get
            {
                string name = string.Format("{0}/@{1}", SettingNameBase, NextRunDateTimeAttribute);
                return _configurationDataProvider.GetDateTimeValue(name, defaultValue: null);
            }
        }
    }
}
