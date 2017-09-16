using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargerID.Configuration
{
    public interface IConfig
    {
        IConfigDatabase Database { get; }
        IConfigAdwords Adwords { get; }
    }

    public class Config : IConfig
    {
        private readonly IConfigurationDataProvider _configurationDataProvider;
        private IConfigDatabase _configDatabase;
        private IConfigAdwords _configAdwords;

        public Config()
        {
            _configurationDataProvider = new ConfigurationDataProvider();
            Initialize();
        }

        private void Initialize()
        {
            _configDatabase = new ConfigDatabase(_configurationDataProvider);
            _configAdwords = new ConfigAdwords(_configurationDataProvider);
        }

        public IConfigDatabase Database
        {
            get
            {
                return _configDatabase;
            }
        }

        public IConfigAdwords Adwords
        {
            get
            {
                return _configAdwords;
            }
        }
    }
}
