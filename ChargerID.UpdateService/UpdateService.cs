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
            _logHelper.WriteInfo("UpdateService started.");

            DateTime currentTime = DateTime.Now;

            _logHelper.WriteInfo("Checking configs...");
            DateTime nextRunDateTime = _config.Update.NextRunDateTime;
            bool manualSchedule = _config.Update.ManualSchedule;

            _logHelper.WriteInfo("NextRunDateTime: " + nextRunDateTime);
            _logHelper.WriteInfo("Manual schedule: " + manualSchedule);

            if (currentTime >= nextRunDateTime || manualSchedule)
            {

                _dl.UpdateAppConfig("update/@lastRunDateTime", currentTime.ToString());

                if (_config.Update.EnableLocationIndicatorDataRefresh)
                {
                    RefreshLocationIndicatorData();
                }

                if (_config.Update.EnableCampaignUpdate)
                {
                    SetAdTargets();
                }

                UpdateAppConfiguration(currentTime, nextRunDateTime, manualSchedule);
            }

            _logHelper.WriteInfo("UpdateService stopped.");
        }

        private void RefreshLocationIndicatorData()
        {
            _logHelper.WriteInfo("Location indicator data refresh started.");

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

            _logHelper.WriteInfo("Location indicator data refresh completed.");
        }

        private void SetAdTargets()
        {
            _logHelper.WriteInfo("Set advertising targets started.");

            // get top n metro areas (n is set in the app_config table)
            List<KeyValuePair<string, string>> topLocations = _updateServiceHelper.GetTopStationCountLocations(_config.Update.MaxAdwordsTargets);

            // get current targets
            List<KeyValuePair<string, string>> currentTargets = _adServicesClient.GetCurrentAdTargets();

            // compare lists to identify targets to add and targets to remove
            List<KeyValuePair<string, string>> targetsToRemove = currentTargets.Except(topLocations).ToList();
            List<KeyValuePair<string, string>> targetsToAdd = topLocations.Except(currentTargets).ToList();

            // call ad services api with list of central cities/states from each metro area to remove and update mode 1 (remove)
            if (targetsToRemove == null || targetsToRemove.Count <= 0)
            {
                _logHelper.WriteInfo("No targets to remove.");
            }
            else 
            {
                _logHelper.WriteInfo("Removing " + targetsToRemove.Count.ToString() + " targets.");
                try
                {
                    bool targetsRemovedSuccessfully = _adServicesClient.SendAdservicesTargetsPost(targetsToRemove, UpdateMode.Remove);
                    if (!targetsRemovedSuccessfully)
                    {
                        _logHelper.WriteError("Failed to remove targets.");
                    }
                    else
                    {
                        _logHelper.WriteInfo("Targets removed successfully.");
                    }
                }
                catch (Exception e)
                {
                    _logHelper.WriteError(e.Message);
                }
            }

            // call ad services api with list of central cities/states from each metro area to add and update mode 0 (add)
            if (targetsToAdd == null || targetsToAdd.Count <= 0)
            {
                _logHelper.WriteInfo("No targets to add.");
            }
            else
            {
                _logHelper.WriteInfo("Adding " + targetsToAdd.Count.ToString() + " targets.");
                try
                {
                    bool targetsAddedSuccessfully = _adServicesClient.SendAdservicesTargetsPost(targetsToAdd, UpdateMode.Add);
                    if (!targetsAddedSuccessfully)
                    {
                        _logHelper.WriteError("Failed to add targets");
                    }
                    else
                    {
                        _logHelper.WriteInfo("Targets added successfully.");
                    }
                }
                catch (Exception e)
                {
                    _logHelper.WriteError(e.Message);
                }
            }

            _logHelper.WriteInfo("Set advertising targets completed.");
        }

        // Updates next run time and manual schedule flag in the app_config DB table
        private void UpdateAppConfiguration(DateTime currentTime, DateTime nextRunDateTime, bool manualRun)
        {
            // if nextRunDateTime is less than the current time then the current run was the regularly scheduled run and the nextRunDateTime
            // should be updated to reflect the next regularly scheduled run
            if (nextRunDateTime <= currentTime)
            {
                _dl.UpdateAppConfig("update/@nextRunDateTime", currentTime.AddDays(_config.Update.RunIntervalDays).ToString());
            }

            // if manual run flag is true set it to false; this will ensure that the next run time displayed on the UI correctly reflects the 
            // next run time
            if (manualRun)
            {
                _dl.UpdateAppConfig("update/@manualSchedule", "false");
            }
        }
    }
}
