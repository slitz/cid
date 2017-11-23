using System.Web.Mvc;
using ChargerID.DataAccess;
using ChargerID.Configuration;
using System.Collections.Generic;
using ChargerID.UI.Models;
using ChargerID.Business.Models;
using ChargerID.Business.Partner.AdServices.Google;
using System;

namespace ChargerID.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDataAccess _data;
        private readonly IAdwordsClient _adwordsClient;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public AdminController()
            : this(null, null)
        {

        }

        public AdminController(IDataAccess data = null, IAdwordsClient adwordsClient = null, IConfig config = null)
        {
            _data = data ?? new DataAccess.DataAccess();
            _adwordsClient = adwordsClient ?? new AdwordsClient();
            _config = config ?? new Config();
        }

        // GET /Admin Page
        [Authorize]
        public ActionResult Index()
        {
            var adminModel = new AdminModel();
            adminModel.CurrentTargetsList = GetCurrentTargets();
            adminModel.MaxAdwordsTargets = _config.Update.MaxAdwordsTargets;
            adminModel.EnableDataUpdateValue = _config.Update.EnableLocationIndicatorDataRefresh.ToString();
            adminModel.EnableCampaignUpdateValue = _config.Update.EnableCampaignUpdate.ToString();
            adminModel.SystemSettingsList = GetSystemSettings(adminModel);
            adminModel.CampaignTargetCountSelectionItems = GetCampaignTargetCountSelectionItems(adminModel.MaxAdwordsTargets);
            adminModel.EnableDataUpdateSelectionItems = GetBooleanSelectionItems(adminModel.EnableDataUpdateValue);
            adminModel.EnableCampaignUpdateSelectionItems = GetBooleanSelectionItems(adminModel.EnableCampaignUpdateValue);
            adminModel.LastSystemRunDate = _config.Update.LastRunDateTime;
            adminModel.NextSystemRunDate = _config.Update.NextRunDateTime;
            return View(adminModel);
        }

        // Update system settings
        [HttpPost]
        [Authorize]
        public ActionResult Update(AdminModel model)
        {
            try
            {
                bool updateMaxAdwordsTargetsResult = _data.UpdateAppConfig("update/@maxAdwordsTargets", model.NewMaxAdwordsTargets);
                bool updateEnableUpdateDataValueResult = _data.UpdateAppConfig("update/@enableLocationIndicatorDataRefresh", model.NewEnableDataUpdateValue);
                bool updateEnableCampaignUpdateValueResult = _data.UpdateAppConfig("update/@enableCampaignUpdate", model.NewEnableCampaignUpdateValue);

                if (updateMaxAdwordsTargetsResult && updateEnableUpdateDataValueResult && updateEnableCampaignUpdateValueResult)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {

            }
            return View();
        }

        /// <summary>
        /// Retrieves the current Adwords campaign location targets
        /// </summary>
        /// <returns></returns>
        private List<GeoTarget> GetCurrentTargets()
        {
            List<GeoTarget> list = new List<GeoTarget>();
            list = _adwordsClient.GetCampaignGeoTargets(_config.Update.AdwordsCampaignId.ToString());
            return list;
        }

        /// <summary>
        /// Retrieves select system settings from the database
        /// </summary>
        /// <returns></returns>
        private List<SystemSettings> GetSystemSettings(AdminModel model)
        {
            List<SystemSettings> list = new List<SystemSettings>();
            list.Add(new SystemSettings { Name = "Campaign Target Count", Value = model.MaxAdwordsTargets.ToString() });
            list.Add(new SystemSettings { Name = "Enable Data Updates", Value = _config.Update.EnableLocationIndicatorDataRefresh.ToString() });
            list.Add(new SystemSettings { Name = "Enable Campaign Updates", Value = _config.Update.EnableCampaignUpdate.ToString() });
            return list;
        }

        private List<SelectListItem> GetCampaignTargetCountSelectionItems(int count)
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text = "0", Value = "0", Selected = count == 0 },
                new SelectListItem{ Text = "1", Value = "1", Selected = count == 1 },
                new SelectListItem{ Text = "2", Value = "2", Selected = count == 2 },
                new SelectListItem{ Text = "3", Value = "3", Selected = count == 3 },
                new SelectListItem{ Text = "4", Value = "4", Selected = count == 4 },
                new SelectListItem{ Text = "5", Value = "5", Selected = count == 5 },
                new SelectListItem{ Text = "6", Value = "6", Selected = count == 6 },
                new SelectListItem{ Text = "7", Value = "7", Selected = count == 7 },
                new SelectListItem{ Text = "8", Value = "8", Selected = count == 8 },
                new SelectListItem{ Text = "9", Value = "9", Selected = count == 9 },
                new SelectListItem{ Text = "10", Value = "10", Selected = count == 10 },
                new SelectListItem{ Text = "11", Value = "11", Selected = count == 11 },
                new SelectListItem{ Text = "12", Value = "12", Selected = count == 12 },
                new SelectListItem{ Text = "13", Value = "13", Selected = count == 13 },
                new SelectListItem{ Text = "14", Value = "14", Selected = count == 14 },
                new SelectListItem{ Text = "15", Value = "15", Selected = count == 15 },
                new SelectListItem{ Text = "20", Value = "20", Selected = count == 20 },
                new SelectListItem{ Text = "25", Value = "25", Selected = count == 25 }   
            };
        }

        private List<SelectListItem> GetBooleanSelectionItems(string value)
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text = "true", Value = "true", Selected = value == "True" },
                new SelectListItem{ Text = "false", Value = "false", Selected = value == "False" }   
            };
        }
    }
}
