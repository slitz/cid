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
        public void GetLocationNames()
        {
            AdwordsUserHelper adwordsUserHelper = new AdwordsUserHelper();
            AdWordsUser adwordsUser = adwordsUserHelper.SetupAdwordsUser();
            LocationNameHelper client = new LocationNameHelper(adwordsUser);
            List<string> idList = new List<string>();
            idList.Add("21137");
            var pairs = client.GetLocationNamesByTargetIds(idList);
        }

        //[Test]
        //public void GetAdwordsCampaignGeoTargets()
        //{
        //    AdwordsClient client = new AdwordsClient();
        //    client.GetCampaignGeoTargets();
        //}
    }
}
