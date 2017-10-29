using ChargerID.Business.Models;
using ChargerID.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ChargerID.Business
{
    public interface IAdServicesClient
    {
        List<KeyValuePair<string, string>> GetCurrentAdTargets();
        bool SendAdservicesTargetsPost(List<KeyValuePair<string, string>> targets, UpdateMode updateMode);
    }

    public class AdServicesClient : IAdServicesClient
    {
        private readonly ILogHelper _logHelper;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public AdServicesClient(IConfig config = null, ILogHelper logHelper = null)
        {
            _config = config ?? new Config();
            _logHelper = logHelper ?? new LogHelper();
        }

        public List<KeyValuePair<string, string>> GetCurrentAdTargets()
        {
            List<KeyValuePair<string, string>> targetsList = new List<KeyValuePair<string, string>>();
            string url = String.Format(_config.Update.AdwordsTargetsUrl, _config.Update.AdwordsCampaignId);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(url);
                    string resultString = response.Result.Content.ReadAsStringAsync().Result;
                    var json = JsonConvert.DeserializeObject<dynamic>(resultString);
                    for (int i = 0; i < json.data.Count; i++)
                    {
                        targetsList.Add(new KeyValuePair<string, string>(json.data[i].City.ToString().ToLower(), json.data[i].State.ToString().ToLower()));
                    }
                }
                catch (Exception e)
                {
                    _logHelper.WriteError(e.Message);
                    targetsList.Clear();
                }
            }

            return targetsList;
        }

        public bool SendAdservicesTargetsPost(List<KeyValuePair<string, string>> targets, UpdateMode updateMode)
        {
            bool success = false;
            string url = String.Format(_config.Update.AdwordsTargetsUrl, _config.Update.AdwordsCampaignId);

            List<GeoLocation> targetsList = new List<GeoLocation>();
            foreach (KeyValuePair<string, string> pair in targets)
            {
                targetsList.Add(new GeoLocation() { City = pair.Key, State = pair.Value });
            }

            UpdateGeoTargetsRequest request = new UpdateGeoTargetsRequest()
            {
                GeoLocation = targetsList,
                UpdateMode = updateMode
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.PostAsJsonAsync(url, request);
                    string resultString = response.Result.Content.ReadAsStringAsync().Result;
                    var json = JsonConvert.DeserializeObject<dynamic>(resultString);
                    if (json.data.Success == "true")
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    _logHelper.WriteError(e.Message);
                }
            }

            return success;
        }
    }
}
