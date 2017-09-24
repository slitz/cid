using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargerID.Business.Models
{
    [JsonObject("StationCounts")]
    public class StationCounts
    {
        [JsonProperty(PropertyName = "Stations")]
        public string Stations { get; set; }

        [JsonProperty(PropertyName = "Ports")]
        public string Ports { get; set; }
    }
}