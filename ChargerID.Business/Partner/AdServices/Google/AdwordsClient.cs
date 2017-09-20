﻿using System.Collections.Generic;
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
        private AdwordsUserHelper _adwordsUserHelper;
        private readonly ILocationNameHelper _locationNameHelper;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        #region constructor

        public AdwordsClient(IConfig config = null, AdwordsUserHelper adwordsUserHelper = null, ILocationNameHelper locationNameHelper = null)
        {
            _config = config ?? new Config();
            _adwordsUserHelper = adwordsUserHelper ?? new AdwordsUserHelper();
            _adwordsUser = _adwordsUserHelper.SetupAdwordsUser();
            _locationNameHelper = locationNameHelper ?? new LocationNameHelper(_adwordsUser);
        }

        #endregion

        #region public methods

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

        public bool AddCampaignGeoTargets(string campaignId, List<string> locationNames)
        {
            bool isSuccess = true;

            CampaignCriterionService campaignCriterionService = (CampaignCriterionService)_adwordsUser.GetService(AdWordsService.v201708.CampaignCriterionService);

            List<KeyValuePair<string, string>> pairs = _locationNameHelper.GetTargetIdsByLocationNames(locationNames);

            List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();
            foreach (KeyValuePair<string, string> pair in pairs)
            {
                Location location = new Location() { id = Convert.ToInt64(pair.Value) };
                CampaignCriterionOperation operation = new CampaignCriterionOperation();
                CampaignCriterion campaignCriterion = new CampaignCriterion();
                campaignCriterion.campaignId = Convert.ToInt64(campaignId);
                campaignCriterion.criterion = location;
                campaignCriterion.CampaignCriterionType = "Location";
                operation.operand = campaignCriterion;
                operation.OperationType = "ADD";
                operation.@operator = Operator.ADD;
                operations.Add(operation);
            }

            try
            {
                CampaignCriterionReturnValue result = campaignCriterionService.mutate(operations.ToArray());
            }
            catch (Exception ex)
            {
                var exception = ex.Message;
                isSuccess = false;
            }

            return isSuccess;
        }

        #endregion

        #region private methods

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
                List<KeyValuePair<string, string>> pairs = _locationNameHelper.GetLocationNamesByTargetIds(targetIds);

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

        #endregion
    }
}