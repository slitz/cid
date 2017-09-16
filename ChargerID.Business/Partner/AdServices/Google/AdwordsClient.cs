using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargerID.Configuration;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.AdWords.v201708;
using ChargerID.Business.Models;

namespace ChargerID.Business.Partner.AdServices.Google
{
    public interface IAdwordsClient
    {
        List<AdwordsCampaign> GetCampaigns();
    }

    public class AdwordsClient : IAdwordsClient
    {
        private AdWordsUser _adwordsUser;
        private AdWordsAppConfig _adwordsConfig;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public AdwordsClient(IConfig config = null)
        {
            _config = config ?? new Config();
            _adwordsUser = new AdWordsUser();
            _adwordsConfig = (AdWordsAppConfig)_adwordsUser.Config;
            AssignAdwordsUserConfigs();
        }

        public List<AdwordsCampaign> GetCampaigns()
        {
            CampaignService campaignService = (CampaignService)_adwordsUser.GetService(AdWordsService.v201708.CampaignService);

            Selector selector = new Selector();
            selector.fields = new string[] { "Id", "Name", "Status" };

            CampaignPage campaigns = campaignService.get(selector);
            List<AdwordsCampaign> list = PopulateCampaignList(campaigns);

            return list;
        }

        public void GetCampaignGeoTargets()
        {
            CampaignCriterionService campaignCriterionService = (CampaignCriterionService)_adwordsUser.GetService(AdWordsService.v201708.CampaignCriterionService);

            Selector selector = new Selector();
            selector.fields = new string[] { "CampaignId", "Id", "CriteriaType", "LocationName" };

            var response = campaignCriterionService.get(selector);
        }

        private void AssignAdwordsUserConfigs()
        {
            _adwordsConfig.UserAgent = _config.Adwords.UserAgent;
            _adwordsConfig.DeveloperToken = _config.Adwords.DeveloperToken;
            _adwordsConfig.ClientCustomerId = _config.Adwords.ClientAccount;
            _adwordsConfig.OAuth2ClientId = _config.Adwords.ClientId;
            _adwordsConfig.OAuth2ClientSecret = _config.Adwords.ClientSecret;
            _adwordsConfig.OAuth2RefreshToken = _config.Adwords.RefreshToken;
        }

        private List<AdwordsCampaign> PopulateCampaignList(CampaignPage campaigns)
        {
            var list = new List<AdwordsCampaign>();

            for (int x = 0; x < campaigns.totalNumEntries; x++)
            {
                list.Add(new AdwordsCampaign () {
                    Id = campaigns.entries[x].id,
                    CampaignName = campaigns.entries[x].name,
                    Status = campaigns.entries[x].status
                });
            }

            return list;
        }
    }
}
