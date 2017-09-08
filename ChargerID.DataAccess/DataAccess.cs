using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ChargerID.DataAccess
{
    public interface IDataAccess
    {
    }

    public class DataAccess : IDataAccess
    {
        public DataAccess()
        {
        }

        public location GetLocation(string postalCode)
        {
            using (CIDEntities dbContext = new CIDEntities())
            {
                location entity = dbContext.locations.FirstOrDefault(row => row.postal_code == postalCode);
                return entity;
            }
        }

        public List<location> GetLocationsByState(string state)
        {
            var list = new List<location>();

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    var query = from l in dbContext.locations
                                where l.state == state
                                select l;

                    list = query.ToList();
                }
                catch (Exception)
                {
                    string.Format("unable to retrieve locations by state name: {0}", state);
                }
            }

            return list;
        }

        public bool UpdateLocationChargerCount(string postalCode, Int32 chargerCount)
        {
            bool result = true;

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    location entity = dbContext.locations.Single(row => row.postal_code == postalCode);
                    entity.charger_count = chargerCount;
                    dbContext.SaveChanges();
                }
                catch (Exception)
                {
                    result = false;
                    string.Format("failed to update charger_count for: {0}", postalCode);
                }    
            }

            return result;
        }

        public List<app_config> GetAppConfig()
        {
            var list = new List<app_config>();

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    list = dbContext.app_config.ToList();
                }
                catch
                {
                    string.Format("unable to retrieve app_config records");
                }
            }

            return list;
        }

        public app_config GetAppConfig(long id)
        {
            using (CIDEntities dbContext = new CIDEntities())
            {
                app_config entity = dbContext.app_config.FirstOrDefault(row => row.id == id);
                return entity;
            }
        }
    }
}
