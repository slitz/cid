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
    public class NrelClientTests
    {
        [Test]
        public void GetStationCounts()
        {
            NrelClient client = new NrelClient();
            GeoLocation location = new GeoLocation()
            {
                City = "Detroit",
                State = "Michigan"
            };
            StationCounts counts = client.GetStationCountsByGeoLocation(location);

            Assert.That(counts.Ports, Is.Not.Empty);
            Assert.That(counts.Stations, Is.Not.Empty);
        }
    }
}
