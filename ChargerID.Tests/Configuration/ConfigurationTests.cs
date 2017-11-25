using System;
using NUnit.Framework;
using ChargerID.Configuration;

namespace ChargerID.Tests
{
    [TestFixture]
    [Category("Integration Test")]
    public class ConfigurationTests
    {
        [Test]
        public void ConfigurationDataProvider_GetStringValue()
        {
            ConfigurationDataProvider config = new ConfigurationDataProvider();
            string result = config.GetStringValue("test/@test", "test/@test");
            Assert.That(result, Is.TypeOf<string>());
        }

        [Test]
        public void ConfigurationDataProvider_GetIntValue()
        {
            ConfigurationDataProvider config = new ConfigurationDataProvider();
            int result = config.GetIntValue("update/@maxAdwordsTargets", 0);
            Assert.That(result, Is.TypeOf<int>());
        }

        [Test]
        public void ConfigurationDataProvider_GetLongValue()
        {
            ConfigurationDataProvider config = new ConfigurationDataProvider();
            long result = config.GetLongValue("update/@maxAdwordsTargets", 0);
            Assert.That(result, Is.TypeOf<long>());
        }

        [Test]
        public void ConfigurationDataProvider_GetDateTimeValue()
        {
            ConfigurationDataProvider config = new ConfigurationDataProvider();
            DateTime result = config.GetDateTimeValue("update/@lastRunDateTime", DateTime.Now);
            Assert.That(result, Is.TypeOf<DateTime>());
        }

        [Test]
        public void ConfigurationDataProvider_GetBoolValue()
        {
            ConfigurationDataProvider config = new ConfigurationDataProvider();
            bool result = config.GetBoolValue("update/@manualSchedule", true);
            Assert.That(result, Is.TypeOf<bool>());
        }
    }
}
