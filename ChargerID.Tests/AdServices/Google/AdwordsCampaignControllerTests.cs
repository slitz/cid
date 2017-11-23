using System;
using NUnit.Framework;
using System.Web;
using ChargerID.Business;
using ChargerID.Business.Partner.AdServices.Google;
using ChargerID.AdServices.Controllers.Google;
using ChargerID.Business.Models;
using System.Collections.Generic;

namespace ChargerID.Tests
{
    [TestFixture]
    [Category("Integration Test")]
    public class AdwordsCampaignControllerTests
    {
        [Test]
        public void UpdateGeoTargetsPut_AddTarget()
        {
            CampaignControllerPut controller = new CampaignControllerPut();
            List<GeoLocation> list = new List<GeoLocation>();
            list.Add(new GeoLocation() { City = "Miami", State = "Florida" });
            UpdateGeoTargetsRequest request = new UpdateGeoTargetsRequest() 
            {
                GeoLocation = list,
                UpdateMode = UpdateMode.Add
            };
            
            UpdateGeoTargetsResponse response = controller.UpdateCampaignGeoTargets("927060915", request);

            Assert.That(response.Success, Is.True);
        }

        [Test]
        public void UpdateGeoTargetsPut_RemoveTarget()
        {
            CampaignControllerPut controller = new CampaignControllerPut();
            List<GeoLocation> list = new List<GeoLocation>();
            list.Add(new GeoLocation() { City = "Miami", State = "Florida" });
            UpdateGeoTargetsRequest request = new UpdateGeoTargetsRequest()
            {
                GeoLocation = list,
                UpdateMode = UpdateMode.Remove
            };

            UpdateGeoTargetsResponse response = controller.UpdateCampaignGeoTargets("927060915", request);

            Assert.That(response.Success, Is.True);
        }
    }
}
