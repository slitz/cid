using System;
using NUnit.Framework;
using ChargerID.UpdateService;

namespace ChargerID.Tests
{
    [TestFixture]
    [Category("Integration Test")]
    public class UpdateServiceTests
    {
        [Test]
        public void RunUpdateTest()
        {
            UpdateService.UpdateService service = new UpdateService.UpdateService();
            service.RunUpdate();
        }
    }
}
