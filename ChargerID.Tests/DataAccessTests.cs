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
        public void GetLocationByPostalCodeTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetLocation("92653");
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
        public void GetAppConfig()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetAppConfig();
            Assert.That(record[0], Is.InstanceOf<app_config>());
        }
    }
}
