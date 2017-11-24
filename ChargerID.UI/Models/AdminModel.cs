using ChargerID.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargerID.UI.Models
{
    public class AdminModel
    {
        public List<GeoTarget> CurrentTargetsList { get; set; }
        public List<SystemSettings> SystemSettingsList { get; set; }
        public int MaxAdwordsTargets { get; set; }
        public string EnableDataUpdateValue { get; set; }
        public string EnableCampaignUpdateValue { get; set; }
        public List<System.Web.Mvc.SelectListItem> CampaignTargetCountSelectionItems { get; set; }
        public List<System.Web.Mvc.SelectListItem> EnableDataUpdateSelectionItems { get; set; }
        public List<System.Web.Mvc.SelectListItem> EnableCampaignUpdateSelectionItems { get; set; }
        public string NewMaxAdwordsTargets { get; set; }
        public string NewEnableDataUpdateValue { get; set; }
        public string NewEnableCampaignUpdateValue { get; set; }
        public DateTime LastSystemRunDate { get; set; }
        public DateTime NextSystemRunDate { get; set; }
        public bool ManualSchedule { get; set; }
        public List<System.Web.Mvc.SelectListItem> ManualScheduleSelectionItems { get; set; }
        public string ManualScheduleRunDate { get; set; }
    }

    public class SystemSettings
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}