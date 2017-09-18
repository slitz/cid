using System.Collections.Generic;
using ChargerID.Configuration;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201708;
using ChargerID.Business.Models;
using System;
using ChargerID.Business.Exceptions;

namespace ChargerID.Business.Partner.AdServices.Google
{
    public interface IAdwordsClient
    {
        List<AdwordsCampaign> GetCampaigns();
        List<GeoTarget> GetCampaignGeoTargets(string campaignId);
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

            if (campaigns != null)
            {
                List<AdwordsCampaign> list = PopulateCampaignList(campaigns);
                return list;
            }
            else
            {
                throw new ValidationException("Unable to retrieve campaigns.");
            }
        }

        public List<GeoTarget> GetCampaignGeoTargets(string campaignId)
        {
            CampaignCriterionService campaignCriterionService = (CampaignCriterionService)_adwordsUser.GetService(AdWordsService.v201708.CampaignCriterionService);

            Selector selector = new Selector();
            selector.fields = new string[] { "CampaignId", "Id", "CriteriaType", "LocationName" };

            CampaignCriterionPage targets = campaignCriterionService.get(selector);
            List<GeoTarget> list = PopulateTargetList(targets, campaignId);

            return list;
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

        private List<GeoTarget> PopulateTargetList(CampaignCriterionPage targets, string campaignId)
        {
            var list = new List<GeoTarget>();
            List<string> targetIds = ExtractTargetIds(targets, campaignId);
            if (targetIds != null && targetIds.Count > 0)
            {
                List<KeyValuePair<string, string>> pairs = GetLocationNamesByTargetIds(targetIds);

                foreach (KeyValuePair<string, string> pair in pairs)
                {
                    list.Add(new GeoTarget()
                    {
                        Id = pair.Key,
                        Name = pair.Value
                    });
                }

                return list;
            }
            else
            {
                throw new ValidationException("Campaign not found.");
            }
        }

        private List<string> ExtractTargetIds(CampaignCriterionPage targets, string campaignId)
        {
            List<string> idList = new List<string>();
            for (int i = 0; i < targets.totalNumEntries; i++)
            {
                if (targets.entries[i].campaignId == Convert.ToInt64(campaignId) && targets.entries[i].criterion.type == CriterionType.LOCATION)
                {
                    idList.Add(targets.entries[i].criterion.id.ToString());
                }
            }

            return idList;
        }

        private List<KeyValuePair<string, string>> GetLocationNamesByTargetIds(List<string> ids)
        {
            // Adwords service that provides location names
            LocationCriterionService locationCriterionService = (LocationCriterionService)_adwordsUser.GetService(AdWordsService.v201708.LocationCriterionService);
            Selector selector = new Selector();
            selector.fields = new string[] { "Id", "LocationName", "CanonicalName", "DisplayType", "ParentLocations", "Reach" };

            // Predicates allow filtering of results
            Predicate IdPredicate = new Predicate();
            IdPredicate.field = "Id";
            IdPredicate.@operator = PredicateOperator.EQUALS;
            IdPredicate.values = ids.ToArray();
            selector.predicates = new Predicate[] { IdPredicate };

            LocationCriterion[] locationCriteria = locationCriterionService.get(selector);
            
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

            foreach (string id in ids)
            {
                foreach (LocationCriterion location in locationCriteria)
                {
                    if (location.location.id == Convert.ToInt64(id))
                    {
                        pairs.Add(new KeyValuePair<string, string>(id, location.location.locationName));
                    }
                }
            }
        
            return pairs;
        }
    }
}
