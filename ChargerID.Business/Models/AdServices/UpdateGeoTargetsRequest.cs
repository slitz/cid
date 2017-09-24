using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ChargerID.Business.Models
{
    [JsonObject("UpdateGeoTargetsRequest")]
    public class UpdateGeoTargetsRequest
    {
        [JsonProperty(PropertyName = "UpdateMode", Required = Required.Always)]
        public UpdateMode UpdateMode { get; set; }

        [JsonProperty(PropertyName = "GeoLocation", Required = Required.Always)]
        public List<GeoLocation> GeoLocation { get; set; }
    }

    public enum UpdateMode : int
    {
        Add = 0,
        Remove = 1
    }
}