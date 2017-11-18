using System.Web.Mvc;
using ChargerID.DataAccess;
using ChargerID.Configuration;
using System.Collections.Generic;
using ChargerID.UI.Models;
using ChargerID.Business.Models;
using ChargerID.Business.Partner.AdServices.Google;

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
            adminModel.SystemSettingsList = GetSystemSettings();
            adminModel.LastSystemRunDate = _config.Update.LastRunDateTime;
            return View(adminModel);
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
        private List<SystemSettings> GetSystemSettings()
        {
            List<SystemSettings> list = new List<SystemSettings>();
            list.Add(new SystemSettings { Name = "Campaign Target Count", Value = _config.Update.MaxAdwordsTargets.ToString() });
            list.Add(new SystemSettings { Name = "Enable Data Updates", Value = _config.Update.EnableLocationIndicatorDataRefresh.ToString() });
            list.Add(new SystemSettings { Name = "Enable Campaign Updates", Value = _config.Update.EnableCampaignUpdate.ToString() });
            return list;
        }
    }
}
