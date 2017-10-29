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
        public void UpdateGeoTargetsPut()
        {
            AdwordsCampaignController controller = new AdwordsCampaignController();
            List<GeoLocation> list = new List<GeoLocation>();
            list.Add(new GeoLocation() { City = "Miami", State = "Florida" });
            //list.Add(new GeoLocation() { City = "tempe", State = "arizona" });
            UpdateGeoTargetsRequest request = new UpdateGeoTargetsRequest() 
            {
                GeoLocation = list,
                UpdateMode = UpdateMode.Add
            };
            controller.UpdateGeoTargets("927060915", request);
        }
    }
}
