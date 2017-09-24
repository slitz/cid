using System;
using NUnit.Framework;
using ChargerID.Business;
using ChargerID.Business.Partner.AdServices.Google;
using System.Collections.Generic;

namespace ChargerID.Tests
{
    [TestFixture]
    [Category("Integration Test")]
    public class AdwordsClientTests
    {
        [Test]
        public void GetAdwordsCampaigns()
        {
            AdwordsClient client = new AdwordsClient();
            client.GetCampaigns();
        }

        [Test]
        public void GetCampaignGeoTargets()
        {
            AdwordsClient client = new AdwordsClient();
            client.GetCampaignGeoTargets("931755099");
        }

        //[Test]
        //public void AddCampaignGeoTargets()
        //{
        //    AdwordsClient client = new AdwordsClient();
        //    List<KeyValuePair<string,string>> list = new List<KeyValuePair<string,string>>();
        //    list.Add(new KeyValuePair<string, string>("Dallas", "Texas"));
        //    client.UpdateCampaignGeoTargets("927060915", list, Google.Api.Ads.AdWords.v201708.Operator.ADD);
        //}

        //[Test]
        //public void RemoveCampaignGeoTargets()
        //{
        //    AdwordsClient client = new AdwordsClient();
        //    List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
        //    list.Add(new KeyValuePair<string, string>("Dallas", "Texas"));
        //    client.UpdateCampaignGeoTargets("927060915", list, Google.Api.Ads.AdWords.v201708.Operator.REMOVE);
        //}
    }
}
