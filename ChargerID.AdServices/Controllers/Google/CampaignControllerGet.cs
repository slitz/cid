using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Threading.Tasks;
using ChargerID.Business.Partner.AdServices.Google;
using ChargerID.Business.Models;

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
    }
}