using System;
using System.Collections.Generic;
using System.Linq;
using ChargerID.DataAccess;
using ChargerID.Configuration;
using ChargerID.Business;
using ChargerID.Business.Models;

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

        private readonly ILogHelper _logHelper;
        private readonly IDataAccess _dl;
        private readonly IAdServicesClient _adServicesClient;
        private readonly ILocatorServicesClient _locatorServicesClient;
        private readonly IUpdateServiceHelper _updateServiceHelper;

        public UpdateService(IConfig config = null, ILogHelper logHelper = null, IDataAccess dl = null, IAdServicesClient adServicesClient = null, ILocatorServicesClient locatorServicesClient = null, IUpdateServiceHelper updateServiceHelper = null)
        {
            _config = config ?? new Config();
            _logHelper = logHelper ?? new LogHelper();
            _dl = dl ?? new DataAccess.DataAccess();
            _adServicesClient = adServicesClient ?? new AdServicesClient();
            _locatorServicesClient = locatorServicesClient ?? new LocatorServicesClient();
            _updateServiceHelper = updateServiceHelper ?? new UpdateServiceHelper();
        }

        public void RunUpdate()
        {
            _logHelper.WriteInfo("Running update.");

            //RefreshLocationIndicatorData();
            SetAdTargets();
        }

        private void RefreshLocationIndicatorData()
        {
            _logHelper.WriteInfo("Refreshing location indicator data.");

            IList<metropolitan_area> metroAreas = _dl.GetAllMetropolitanAreas();

            try
            {
                foreach (metropolitan_area area in metroAreas)
                {
                    List<location> locations = _dl.GetLocationsByMetropolitanAreaId(area.id);
                    foreach (location l in locations)
                    {
                        string url = String.Format(_config.Update.NrelStationsUrl, l.city, l.state);
                        KeyValuePair<string, string> stationAndPortCounts = _locatorServicesClient.SendChargingStationsGet(url);
                        _logHelper.WriteInfo("postal code: " + l.postal_code + " stations: " + stationAndPortCounts.Key + " ports: " + stationAndPortCounts.Value);
                        _dl.AddChargingStationData(l.postal_code, Convert.ToInt32(stationAndPortCounts.Key), Convert.ToInt32(stationAndPortCounts.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                _logHelper.WriteError(ex.Message);
            }
        }

        private void SetAdTargets()
        {
            _logHelper.WriteInfo("Setting advertising targets.");

            // get top n metro areas
            List<KeyValuePair<string, string>> topLocations = _updateServiceHelper.GetTopStationCountLocations(_config.Update.MaxAdwordsTargets);

            // get current targets
            List<KeyValuePair<string, string>> currentTargets = _adServicesClient.GetCurrentAdTargets();

            // compare lists to identify targets to add and targets to remove
            // TODO: fix state comparison
            List<KeyValuePair<string, string>> targetsToRemove = currentTargets.Except(topLocations).ToList();
            List<KeyValuePair<string, string>> targetsToAdd = topLocations.Except(currentTargets).ToList();

            // for each target to remove call ad services api with central city and state from metro area and update mode 1 (remove)
            if (targetsToRemove != null && targetsToRemove.Count > 0)
            {
                try
                {
                    bool targetsRemovedSuccessfully = _adServicesClient.SendAdservicesTargetsPost(targetsToRemove, UpdateMode.Remove);
                    if (!targetsRemovedSuccessfully)
                    {
                        _logHelper.WriteError("Failed to remove targets.");
                    }
                }
                catch (Exception e)
                {
                    _logHelper.WriteError(e.Message);
                }
            }

            // for each target to add call ad services api with central city and state from metro area and update mode 0 (add)
            if (targetsToAdd != null && targetsToAdd.Count > 0)
            {
                try
                {
                    bool targetsAddedSuccessfully = _adServicesClient.SendAdservicesTargetsPost(targetsToRemove, UpdateMode.Add);
                    if (!targetsAddedSuccessfully)
                    {
                        _logHelper.WriteError("Failed to add targets");
                    }
                }
                catch (Exception e)
                {
                    _logHelper.WriteError(e.Message);
                }
            }
        }
    }
}
