using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargerID.Configuration;

namespace ChargerID.Business.Partner.AdServices.Google
{
    public interface IAdwordsClient
    {
    }

    public class AdwordsClient : IAdwordsClient
    {
        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public AdwordsClient(IConfig config = null)
        {
            _config = config ?? new Config();
            
        }
    }
}
