using System;
using NUnit.Framework;
using ChargerID.Business;
using ChargerID.Business.Partner.AdServices.Google;

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
        public void GetAdwordsCampaignGeoTargets()
        {
            AdwordsClient client = new AdwordsClient();
            client.GetCampaignGeoTargets();
        }
    }
}
