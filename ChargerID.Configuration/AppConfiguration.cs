using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargerID.DataAccess;

namespace ChargerID.Configuration
{
    public class AppConfiguration
    {
        public AppConfiguration(app_config appConfiguration = null)
        {
            Initialize(appConfiguration);
        }

        public decimal Id { get; private set; }
        public string Name { get; private set; }
        public string Value { get; set; }
        public DateTime Date { get; private set; }

        private void Initialize(app_config appConfiguration)
        {
            if (appConfiguration != null)
            {
                Id = appConfiguration.id;
                Name = appConfiguration.name;
                Value = appConfiguration.value;

                if (appConfiguration.date != null)
                {
                    Date = (DateTime)appConfiguration.date;
                }
            }
        }
    }
}
