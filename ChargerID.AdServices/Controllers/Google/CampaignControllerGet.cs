using System;
using System.Collections.Generic;
using ChargerID.Business.Partner.AdServices.Google;
using ChargerID.Business.Models;
using ChargerID.Business.Exceptions;

namespace ChargerID.AdServices.Controllers.Google
{  
    public class CampaignControllerGet
    {
        private readonly IAdwordsClient _adwordsClient;

        public CampaignControllerGet(IAdwordsClient adwordsClient = null)
        {
            _adwordsClient = adwordsClient ?? new AdwordsClient();
        }

        public List<AdwordsCampaign> GetCampaigns()
        {
            List<AdwordsCampaign> campaigns = null;

            try
            {
                campaigns = _adwordsClient.GetCampaigns();
            }
            catch (Exception)
            {

            }

            return campaigns;
        }


        public List<GeoTarget> GetCampaignGeoTargets(string campaignId)
        {
            if (string.IsNullOrWhiteSpace(campaignId))
            {
                throw new ValidationException("CampaignId is required.");
            }

            List<GeoTarget> targets = null;

            try
            {
                targets = _adwordsClient.GetCampaignGeoTargets(campaignId);
            }
            catch (Exception ex)
            {
                throw new ValidationException(ex.Message);
            }

            return targets;
        }
    }
}