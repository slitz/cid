using ChargerID.Configuration;
using ChargerID.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace ChargerID.Business
{
    public interface IUpdateServiceHelper
    {
        List<KeyValuePair<string, string>> GetTopStationCountLocations(int count);
    }

    public class UpdateServiceHelper : IUpdateServiceHelper
    {
        private readonly ILogHelper _logHelper;

        private readonly IDataAccess _dl;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public UpdateServiceHelper(IConfig config = null, ILogHelper logHelper = null, IDataAccess dl = null)
        {
            _config = config ?? new Config();
            _logHelper = logHelper ?? new LogHelper();
            _dl = dl ?? new DataAccess.DataAccess();
        }

        public List<KeyValuePair<string, string>> GetTopStationCountLocations(int count)
        {
            List<KeyValuePair<string, string>> locationList = new List<KeyValuePair<string, string>>();
            List<charging_station_data> dataList = new List<charging_station_data>();
            foreach (metropolitan_area metro in _dl.GetAllMetropolitanAreas())
            {
                location location = _dl.GetLocationsByMetropolitanAreaId(metro.id).FirstOrDefault();
                charging_station_data chargingStationData = _dl.GetChargingStationDataByPostalCode(location.postal_code).LastOrDefault();
                dataList.Add(chargingStationData);
            }

            foreach (charging_station_data item in dataList.OrderByDescending(x => x.station_count).Take(count))
            {
                location location = _dl.GetLocationByPostalCode(item.postal_code);
                state state = _dl.GetStateByStateCode(location.state);
                locationList.Add(new KeyValuePair<string, string>(location.city.ToLower(), state.state_name.ToLower()));
            }

            return locationList;
        }
    }
}
