using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargerID.DataAccess;
using ChargerID.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace ChargerID.UpdateService
{
    public interface IUpdateService
    {
        void RunUpdate();
    }

    public class UpdateService : IUpdateService
    {
        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public UpdateService(IConfig config = null)
        {
            _config = config ?? new Config();
        }

        public void RunUpdate()
        {
            Console.WriteLine("Running update.");

            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            IList<metropolitan_area> metroAreas = dl.GetAllMetropolitanAreas();

            try
            {
                foreach (metropolitan_area area in metroAreas)
                {
                    List<location> locations = dl.GetLocationsByMetropolitanAreaId(area.id);
                    foreach (location l in locations)
                    {
                        string url = String.Format(_config.Update.NrelStationsUrl, l.city, l.state);
                        KeyValuePair<string, string> stationAndPortCounts = ExecuteChargingStationsGet(url);
                        Console.WriteLine(stationAndPortCounts.Key + " " + stationAndPortCounts.Value);
                        dl.AddChargingStationData(l.postal_code, Convert.ToInt32(stationAndPortCounts.Key), Convert.ToInt32(stationAndPortCounts.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private KeyValuePair<string,string> ExecuteChargingStationsGet(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(url);
                    string resultString = response.Result.Content.ReadAsStringAsync().Result;
                    var json = JsonConvert.DeserializeObject<dynamic>(resultString);
                    return new KeyValuePair<string, string>(json.data.Stations.ToString(), json.data.Ports.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return new KeyValuePair<string, string>("0", "0");
                }
            }
        }
    }
}
