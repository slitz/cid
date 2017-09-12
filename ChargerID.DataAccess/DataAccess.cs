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
        List<app_config> GetAppConfigs();
    }

    public class DataAccess : IDataAccess
    {
        public DataAccess()
        {
        }

        public metropolitan_area GetMetropolitanAreaById(Int32 id)
        {
            metropolitan_area entity = new metropolitan_area();

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    entity = dbContext.metropolitan_area.FirstOrDefault(row => row.id == id);
                }
                catch (Exception)
                {
                    string.Format("unable to retrieve metropolitan area by id: {0}", id);
                }
            }

            return entity;
        }

        public List<metropolitan_area> GetAllMetropolitanAreas()
        {
            var list = new List<metropolitan_area>();

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    var query = from m in dbContext.metropolitan_area                               
                                select m;

                    list = query.ToList();
                }
                catch (Exception)
                {
                    string.Format("unable to retrieve metropolitan areas");
                }
            }

            return list;
        }

        public location GetLocationByPostalCode(string postalCode)
        {
            location entity = new location();

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    entity = dbContext.locations.FirstOrDefault(row => row.postal_code == postalCode);
                }
                catch (Exception)
                {
                    string.Format("unable to retrieve location by postal code: {0}", postalCode);
                }
            }

            return entity;
        }

        public List<location> GetLocationsByMetropolitanAreaId(Int32 metroAreaId)
        {
            var list = new List<location>();

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    var query = from l in dbContext.locations
                                where l.metropolitan_area_id == metroAreaId
                                select l;

                    list = query.ToList();
                }
                catch (Exception)
                {
                    string.Format("unable to retrieve locations by metropolitan area id: {0}", metroAreaId);
                }
            }

            return list;
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

        public List<app_config> GetAppConfigs()
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
