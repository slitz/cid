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
    }

    public class Config : IConfig
    {
        private readonly IConfigurationDataProvider _configurationDataProvider;
        private IConfigDatabase _configDatabase;

        public Config()
        {
            _configurationDataProvider = new ConfigurationDataProvider();
            Initialize();
        }

        private void Initialize()
        {
            _configDatabase = new ConfigDatabase(_configurationDataProvider);
        }

        public IConfigDatabase Database
        {
            get
            {
                return _configDatabase;
            }
        }
    }
}
