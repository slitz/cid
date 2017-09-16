using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ChargerID.Business.Models;
using System.Net.Http.Formatting;

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
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(PublicApiHttpResponseWithData<string>))]
        public HttpResponseMessage GetAdwordsCampaigns()
        {
            CampaignControllerGet get = new CampaignControllerGet();
            List<AdwordsCampaign> response = get.GetCampaigns();
            return Request.CreateResponse(HttpStatusCode.OK, response, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}