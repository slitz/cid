using ChargerID.Business.Models;
using ChargerID.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace ChargerID.Business
{
    public interface INrelClient
    {
        StationCounts GetStationCountsByGeoLocation(GeoLocation location);
    }

    public class NrelClient : INrelClient
    {
        private const string _getStationCountsUrlFormat = "{0}?fuel_type={1}&radius={2}&api_key={3}&format=JSON&location={4},{5}";

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public NrelClient(IConfig config = null)
        {
            _config = config ?? new Config();
        }

        public StationCounts GetStationCountsByGeoLocation(GeoLocation location)
        {
            StationCounts result = new StationCounts();
            string url = String.Format(_getStationCountsUrlFormat, _config.Nrel.Url, _config.Nrel.FuelType, _config.Nrel.Radius, _config.Nrel.ApiKey,
                location.City, location.State);

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url);
                string resultString = response.Result.Content.ReadAsStringAsync().Result;
                var json = JsonConvert.DeserializeObject<dynamic>(resultString);
                result.Stations = json.station_counts.fuels.ELEC.stations.total;
                result.Ports = json.station_counts.fuels.ELEC.total;
            }

            return result;
        }
    }
}
