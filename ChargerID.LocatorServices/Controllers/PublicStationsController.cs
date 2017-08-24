using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChargerID.LocatorServices.Exceptions;

namespace ChargerID.LocatorServices.Controllers
{
    public class PublicStationsController : ApiController
    {
        private readonly IPublicStations _publicStations;

        public PublicStationsController()
		{
            _publicStations = new PublicStations();            
		}

        public HttpResponseMessage Get(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new MissingParametersException(new List<string>() { "Postal Code" });

            PublicStationsResponse response = new PublicStationsResponse()
            {
                Results = _publicStations.GetPublicStationsByPostalCode(postalCode)
            };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}