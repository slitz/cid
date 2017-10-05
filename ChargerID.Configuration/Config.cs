
namespace ChargerID.Configuration
{
    public interface IConfig
    {
        IConfigDatabase Database { get; }
        IConfigAdwords Adwords { get; }
        IConfigNrel Nrel { get; }
        IConfigUpdate Update { get; }
    }

    public class Config : IConfig
    {
        private readonly IConfigurationDataProvider _configurationDataProvider;
        private IConfigDatabase _configDatabase;
        private IConfigAdwords _configAdwords;
        private IConfigNrel _configNrel;
        private IConfigUpdate _configUpdate;

        public Config()
        {
            _configurationDataProvider = new ConfigurationDataProvider();
            Initialize();
        }

        private void Initialize()
        {
            _configDatabase = new ConfigDatabase(_configurationDataProvider);
            _configAdwords = new ConfigAdwords(_configurationDataProvider);
            _configNrel = new ConfigNrel(_configurationDataProvider);
            _configUpdate = new ConfigUpdate(_configurationDataProvider);
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

        public IConfigNrel Nrel
        {
            get
            {
                return _configNrel;
            }
        }

        public IConfigUpdate Update
        {
            get
            {
                return _configUpdate;
            }
        }
    }
}
