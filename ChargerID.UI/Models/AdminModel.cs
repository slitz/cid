﻿using ChargerID.Business.Models;
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
        public DateTime LastSystemRunDate { get; set; }
    }

    public class SystemSettings
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}