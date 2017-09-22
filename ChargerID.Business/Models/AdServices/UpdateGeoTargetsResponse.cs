using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargerID.Business.Models
{
    [JsonObject("UpdateGeoTargetsRequest")]
    public class UpdateGeoTargetsResponse
    {
        [JsonProperty(PropertyName = "Success", Required = Required.Always)]
        public bool Success { get; set; }
    }
}