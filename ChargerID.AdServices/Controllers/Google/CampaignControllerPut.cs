using System;
using System.Collections.Generic;
using ChargerID.Business.Partner.AdServices.Google;
using ChargerID.Business.Models;
using ChargerID.Business.Exceptions;

namespace ChargerID.AdServices.Controllers.Google
{  
    public class CampaignControllerPut
    {
        private readonly IAdwordsClient _adwordsClient;

        public CampaignControllerPut(IAdwordsClient adwordsClient = null)
        {
            _adwordsClient = adwordsClient ?? new AdwordsClient();
        }

        public UpdateGeoTargetsResponse UpdateCampaignGeoTargets(string campaignId, UpdateGeoTargetsRequest updateGeoTargetsRequest)
        {
            if (string.IsNullOrWhiteSpace(campaignId))
            {
                throw new ValidationException("CampaignId is required.");
            }

            try
            {
                return _adwordsClient.UpdateCampaignGeoTargets(campaignId, updateGeoTargetsRequest);
            }
            catch (Exception ex)
            {
                throw new ValidationException(ex.Message);
            }
        }
    }
}