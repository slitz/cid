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
        public void GetAppConfigByIdTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetAppConfig(1);
            Assert.That(record, Is.InstanceOf<app_config>());
            StringAssert.Contains("test", record.value);
        }

        [Test]
        public void GetAppConfigTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetAppConfigs();
            Assert.That(record[0], Is.InstanceOf<app_config>());
        }

        [Test]
        public void AddChargingStationDataTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var result = dl.AddChargingStationData("85254", 3, 5);
            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void GetChargingStationDataByPostalCodeTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var result = dl.GetChargingStationDataByPostalCode("85254");
            Assert.That(result[0], Is.InstanceOf<charging_station_data>());
        }

        [Test]
        public void GetStateByStateNameTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var result = dl.GetStateByStateName("Michigan");
            Assert.That(result.state_cd, Is.EqualTo("MI"));
        }

        [Test]
        public void GetStateByStateCodeTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var result = dl.GetStateByStateCode("MI");
            Assert.That(result.state_name, Is.EqualTo("Michigan"));
        }

        [Test]
        public void UpdateAppConfigByNameTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var result = dl.UpdateAppConfig(@"test/@test", "test" + DateTime.Now.ToFileTimeUtc().ToString());
            Assert.That(result, Is.EqualTo(true));
        }
    }
}
