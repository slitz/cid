using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ChargerID.Business.Models;
using System;

namespace ChargerID.LocatorServices.Controllers
{
    /// <summary>
    /// Manages Charging Station Objects
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = false)] // show or hide in Swagger   
    public class ChargingStationsController : ApiController
    {
        /// <summary>
        /// Retrieves the charging station and port counts for a configurable radius around the specified city and state
        /// </summary>
        /// <param name="city">City name</param>
        /// <param name="state">State name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("v1/nrel/stations/{city}/{state}")]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(PublicApiHttpResponseWithData<StationCounts>))]
        public HttpResponseMessage GetStationCounts(string city, string state)
        {
            try
            {
                ChargingStationsGet get = new ChargingStationsGet();
                StationCounts retrievedCounts = get.GetStationCounts(city, state);
                return ResponseHelper.CreateResponse(Request, HttpStatusCode.OK, retrievedCounts, PublicApiCommonErrorCode.SUCCESS);
            }
            catch (Exception e)
            {
                return ResponseHelper.CreateResponse(Request, HttpStatusCode.NotFound, e.Message, PublicApiCommonErrorCode.NOT_FOUND);
            }
        }
    }
}