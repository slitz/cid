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

        [Test]
        public void GetStationCounts_EmptyCityAndState()
        {
            NrelClient client = new NrelClient();
            GeoLocation location = new GeoLocation()
            {
                City = "",
                State = ""
            };

            try
            {
                StationCounts counts = client.GetStationCountsByGeoLocation(location);
                Assert.That(false);
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Is.EqualTo("Cannot perform runtime binding on a null reference"));
            }
        }

        [Test]
        public void GetStationCounts_EmptyCity()
        {
            NrelClient client = new NrelClient();
            GeoLocation location = new GeoLocation()
            {
                City = "",
                State = "Michigan"
            };

            StationCounts counts = client.GetStationCountsByGeoLocation(location);
            Assert.That(counts, Is.Not.Null);
        }

        [Test]
        public void GetStationCounts_EmptyState()
        {
            NrelClient client = new NrelClient();
            GeoLocation location = new GeoLocation()
            {
                City = "Detroit",
                State = ""
            };

            StationCounts counts = client.GetStationCountsByGeoLocation(location);
            Assert.That(counts, Is.Not.Null);
        }

        [Test]
        public void GetStationCounts_NonExistingCityAndState()
        {
            NrelClient client = new NrelClient();
            GeoLocation location = new GeoLocation()
            {
                City = "abc",
                State = "def"
            };

            try
            {
                StationCounts counts = client.GetStationCountsByGeoLocation(location);
                Assert.That(false);
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Is.EqualTo("Cannot perform runtime binding on a null reference"));
            }
        }
    }
}
