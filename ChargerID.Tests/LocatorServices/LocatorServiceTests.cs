using System;
using NUnit.Framework;
using ChargerID.LocatorServices.Controllers;
using ChargerID.Business.Models;

namespace ChargerID.Tests
{
    [TestFixture]
    [Category("Integration Test")]
    public class LocatorServiceTests
    {
        [Test]
        public void ChargeingStations_GetByCityAndState()
        {
            ChargingStationsGet service = new ChargingStationsGet();
            StationCounts result = service.GetStationCounts("Detroit", "Michigan");
            Assert.That(result.Stations, Is.Not.Null);
        }

        [Test]
        public void ChargeingStations_GetByEmptyCityAndState()
        {
            ChargingStationsGet service = new ChargingStationsGet();
            try
            {
                StationCounts result = service.GetStationCounts("", "");
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Is.EqualTo("City/state combination not found."));
            }
        }

        [Test]
        public void ChargeingStations_GetByEmptyCity()
        {
            ChargingStationsGet service = new ChargingStationsGet();
            StationCounts result = service.GetStationCounts("", "Michigan");
            Assert.That(result.Stations, Is.Not.Null);
        }

        [Test]
        public void ChargeingStations_GetByEmptyState()
        {
            ChargingStationsGet service = new ChargingStationsGet();
            StationCounts result = service.GetStationCounts("Detroit", "");
            Assert.That(result.Stations, Is.Not.Null);
        }

        [Test]
        public void ChargeingStations_GetByNonExistingCityAndState()
        {
            ChargingStationsGet service = new ChargingStationsGet();
            try
            {
                StationCounts result = service.GetStationCounts("abc", "def");
            }
            catch (Exception e)
            {
                Assert.That(e.Message, Is.EqualTo("City/state combination not found."));
            }
        }
    }
}
