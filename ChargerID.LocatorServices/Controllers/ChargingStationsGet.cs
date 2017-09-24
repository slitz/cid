using System;
using ChargerID.Business;
using ChargerID.Business.Models;
using ChargerID.Business.Exceptions;
#pragma warning disable 1591 //disable warning to add XML documentation

namespace ChargerID.LocatorServices.Controllers
{  
    public class ChargingStationsGet
    {
        private readonly INrelClient _nrelClient;

        public ChargingStationsGet(INrelClient nrelClient = null)
        {
            _nrelClient = nrelClient ?? new NrelClient();
        }

        public StationCounts GetStationCounts(string city, string state)
        {
            StationCounts counts = null;

            GeoLocation location = new GeoLocation() { City = city, State = state };

            try
            {
                counts = _nrelClient.GetStationCountsByGeoLocation(location);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot perform runtime binding on a null reference"))
                {
                    throw new ValidationException("City/state combination not found.");
                }
                else
                {
                    throw new ValidationException(ex.Message);
                }
            }

            return counts;
        }
    }
}