using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargerID.DataAccess;

namespace ChargerID.Configuration
{
    public interface IConfigurationWrapper
    {
        IList<AppConfiguration> GetAllConfigurationItems();
    }

    public class ConfigurationWrapper : IConfigurationWrapper
    {
        private readonly IDataAccess _dataAccess;
        private IDataAccess DataAccess
        {
            get { return _dataAccess ?? new DataAccess.DataAccess(); }
        }

        public ConfigurationWrapper(IDataAccess dataAccess = null)
        {
            _dataAccess = dataAccess;
        }

        public IList<AppConfiguration> GetAllConfigurationItems()
        {
            return DataAccess.GetAppConfigs().Select(cfg => new AppConfiguration(cfg)).ToList();
        }
    }
}
