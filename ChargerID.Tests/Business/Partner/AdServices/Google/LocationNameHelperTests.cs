using System;
using NUnit.Framework;
using Google.Api.Ads.AdWords.Lib;
using ChargerID.Business;
using ChargerID.Business.Partner.AdServices.Google;
using System.Collections.Generic;
using ChargerID.Configuration;

namespace ChargerID.Tests
{
    [TestFixture]
    [Category("Integration Test")]
    public class LocationNameHelperTests
    {
        [Test]
        public void GetLocationNamesByTargetIds()
        {
            AdwordsUserHelper adwordsUserHelper = new AdwordsUserHelper();
            AdWordsUser adwordsUser = adwordsUserHelper.SetupAdwordsUser();
            LocationNameHelper client = new LocationNameHelper(adwordsUser);
            List<string> idList = new List<string>();
            idList.Add("21137");
            var pairs = client.GetLocationNamesByTargetIds(idList);

            Assert.That(pairs, Is.Not.Null);
        }

        [Test]
        public void GetTargetIdsByLocationNames()
        {
            AdwordsUserHelper adwordsUserHelper = new AdwordsUserHelper();
            AdWordsUser adwordsUser = adwordsUserHelper.SetupAdwordsUser();
            LocationNameHelper client = new LocationNameHelper(adwordsUser);
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Dallas", "TX"));
            var pairs = client.GetTargetIdsByLocationNames(list);

            Assert.That(pairs, Is.Not.Null);
        }
    }
}
