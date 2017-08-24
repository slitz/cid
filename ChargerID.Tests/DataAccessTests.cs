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
        public void GetAppConfigByIdTest()
        {
            DataAccess.DataAccess dl = new DataAccess.DataAccess();
            var record = dl.GetAppConfig(1);
            Assert.That(record, Is.InstanceOf<app_config>());
            Assert.That(record.value, Is.EqualTo("test"));
        }
    }
}
