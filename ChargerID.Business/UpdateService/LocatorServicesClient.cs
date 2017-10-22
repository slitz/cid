using ChargerID.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ChargerID.Business
{
    public interface ILocatorServicesClient
    {
        KeyValuePair<string, string> SendChargingStationsGet(string url);
    }

    public class LocatorServicesClient : ILocatorServicesClient
    {
        private readonly ILogHelper _logHelper;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public LocatorServicesClient(IConfig config = null, ILogHelper logHelper = null)
        {
            _config = config ?? new Config();
            _logHelper = logHelper ?? new LogHelper();
        }

        public KeyValuePair<string, string> SendChargingStationsGet(string url)
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
                    _logHelper.WriteError(e.Message);
                    return new KeyValuePair<string, string>("0", "0");
                }
            }
        }
    }
}
