using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ChargerID.Business.Models;
using System;

namespace ChargerID.AdServices.Controllers.Google
{
    /// <summary>
    /// Manages Google Adwords Campaign Objects
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = false)] // show or hide in Swagger   
    public class AdwordsCampaignController : ApiController
    {
        /// <summary>
        /// Retrieves the campaigns of the client account specified in the app_config database table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/adwords/campaigns")]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(PublicApiHttpResponseWithData<List<AdwordsCampaign>>))]
        public HttpResponseMessage GetAdwordsCampaigns()
        {
            try
            {
                CampaignControllerGet get = new CampaignControllerGet();
                List<AdwordsCampaign> retrievedCampaigns = get.GetCampaigns();
                return ResponseHelper.CreateResponse(Request, HttpStatusCode.OK, retrievedCampaigns, PublicApiCommonErrorCode.SUCCESS);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse(Request, HttpStatusCode.NotFound, e.Message, PublicApiCommonErrorCode.NOT_FOUND);
            }
        }

        /// <summary>
        /// Retrieves the location targets of the specified campaign
        /// </summary>
        /// <param name="campaignId">Campaign Id from the default client account</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/adwords/campaigns/{campaignId}/targets")]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(PublicApiHttpResponseWithData<List<AdwordsCampaign>>))]
        public HttpResponseMessage GetCampaignGeoTargets(string campaignId)
        {
            try
            {
                CampaignControllerGet get = new CampaignControllerGet();
                List<GeoTarget> retrievedLocations = get.GetCampaignGeoTargets(campaignId);
                return ResponseHelper.CreateResponse(Request, HttpStatusCode.OK, retrievedLocations, PublicApiCommonErrorCode.SUCCESS);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse(Request, HttpStatusCode.NotFound, e.Message + " " + e.InnerException + " " + e.StackTrace, PublicApiCommonErrorCode.NOT_FOUND);
            }
        }
    }
}