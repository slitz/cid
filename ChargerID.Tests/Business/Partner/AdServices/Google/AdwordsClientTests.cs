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
        public void AddCampaignGeoTargets()
        {
            AdwordsClient client = new AdwordsClient();
            List<string> list = new List<string>() { "Dallas" };
            client.AddCampaignGeoTargets("927060915", list);
        }
    }
}
