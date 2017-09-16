using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ChargerID.Business.Models
{
    public class AdwordsCampaign
    {
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "CampaignName")]
        public string CampaignName { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public Google.Api.Ads.AdWords.v201708.CampaignStatus Status { get; set; }
    }
}