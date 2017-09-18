using System.Collections.Generic;
using ChargerID.Configuration;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201708;
using ChargerID.Business.Models;
using System;
using ChargerID.Business.Exceptions;

namespace ChargerID.Business.Partner.AdServices.Google
{
    public class AdwordsUserHelper
    {
        public AdWordsUser _adwordsUser;
        private AdWordsAppConfig _adwordsConfig;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public AdwordsUserHelper(IConfig config = null)
        {
            _config = config ?? new Config();
            _adwordsUser = new AdWordsUser();
            _adwordsConfig = (AdWordsAppConfig)_adwordsUser.Config;
        }

        public AdWordsUser SetupAdwordsUser()
        {
            _adwordsConfig.UserAgent = _config.Adwords.UserAgent;
            _adwordsConfig.DeveloperToken = _config.Adwords.DeveloperToken;
            _adwordsConfig.ClientCustomerId = _config.Adwords.ClientAccount;
            _adwordsConfig.OAuth2ClientId = _config.Adwords.ClientId;
            _adwordsConfig.OAuth2ClientSecret = _config.Adwords.ClientSecret;
            _adwordsConfig.OAuth2RefreshToken = _config.Adwords.RefreshToken;

            return _adwordsUser;
        }
    }
}
