using System;
using NUnit.Framework;
using ChargerID.DataAccess;

namespace ChargerID.Tests
{
    [TestFixture]
    [Category("Integration Test")]
    public class DataAccessTests
    {
        [Test]
        public void GetMetropolitanAreaByIdTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetMetropolitanAreaById(1);
            Assert.That(record, Is.InstanceOf<metropolitan_area>());
            Assert.That(record.state, Is.EqualTo("PR"));
        }

        [Test]
        public void GetAllMetropolitanAreasTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetAllMetropolitanAreas();
            Assert.That(record[0], Is.InstanceOf<metropolitan_area>());
            Assert.That(record[0].state, Is.EqualTo("PR"));
        }

        [Test]
        public void GetLocationByPostalCodeTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetLocationByPostalCode("92653");
            Assert.That(record, Is.InstanceOf<location>());
            Assert.That(record.state, Is.EqualTo("CA"));
        }

        [Test]
        public void GetLocationsByStateTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var records = dl.GetLocationsByState("CA");
            Assert.That(records[0], Is.InstanceOf<location>());
            Assert.That(records[0].state, Is.EqualTo("CA"));
        }

        [Test]
        public void GetLocationsByMetropolitanAreaIdTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var records = dl.GetLocationsByMetropolitanAreaId(1);
            Assert.That(records[0], Is.InstanceOf<location>());
            Assert.That(records[0].state, Is.EqualTo("PR"));
        }

        [Test]
        public void UpdateLocationChargerCountTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var result = dl.UpdateLocationChargerCount("92653", 2);
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetAppConfigByIdTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetAppConfig(1);
            Assert.That(record, Is.InstanceOf<app_config>());
            Assert.That(record.value, Is.EqualTo("test"));
        }

        [Test]
        public void GetAppConfigTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetAppConfig();
            Assert.That(record[0], Is.InstanceOf<app_config>());
        }
    }
}
