using Newtonsoft.Json;

namespace ChargerID.Business.Models
{
    public class GeoTarget
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "City")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "State")]
        public string State { get; set; }
    }
}