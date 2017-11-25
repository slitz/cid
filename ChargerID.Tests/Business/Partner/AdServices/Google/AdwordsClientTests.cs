using System;
using NUnit.Framework;
using ChargerID.Business;
using ChargerID.Business.Partner.AdServices.Google;
using System.Collections.Generic;
using ChargerID.Business.Models;

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
            List<AdwordsCampaign> result = client.GetCampaigns();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetCampaignGeoTargets()
        {
            AdwordsClient client = new AdwordsClient();
            List<GeoTarget> result = client.GetCampaignGeoTargets("931755099");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetCampaignGeoTargets_EmptyCampaignId()
        {
            AdwordsClient client = new AdwordsClient();
            try
            {
                client.GetCampaignGeoTargets("");
                Assert.That(false);
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Is.EqualTo("Input string was not in a correct format."));
            }
        }

        [Test]
        public void GetCampaignGeoTargets_NonExistingCampaignId()
        {
            AdwordsClient client = new AdwordsClient();
            try
            {
                client.GetCampaignGeoTargets("123456789");
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Is.EqualTo("Campaign not found."));
            }
        }
    }
}
