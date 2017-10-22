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
        List<metropolitan_area> GetAllMetropolitanAreas();
        location GetLocationByPostalCode(string postalCode);
        List<location> GetLocationsByMetropolitanAreaId(Int32 metroAreaId);
        List<charging_station_data> GetChargingStationDataByPostalCode(string postal_code);
        long AddChargingStationData(string postalCode, int chargingStationCount, int portCount);
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

        public long AddChargingStationData(string postalCode, int chargingStationCount, int portCount)
        {
            long result = 0;

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    charging_station_data entity = new charging_station_data()
                    {
                        postal_code = postalCode,
                        station_count = chargingStationCount,
                        port_count = portCount,
                        date = DateTime.Now
                    };

                    dbContext.charging_station_data.Add(entity);

                    if (dbContext.SaveChanges() > 0)
                        result = entity.id;
                }
                catch (Exception ex)
                {
                    result = 0;
                    string.Format("Unable to add charging station data: {0}.", ex);
                }
            }

            return result;
        }

        public List<charging_station_data> GetChargingStationDataByPostalCode(string postal_code)
        {
            var list = new List<charging_station_data>();

            using (CIDEntities dbContext = new CIDEntities())
            {
                try
                {
                    var query = from c in dbContext.charging_station_data
                                where c.postal_code == postal_code
                                select c;

                    list = query.ToList();
                }
                catch (Exception)
                {
                    string.Format("unable to retrieve charging station data by postal code: {0}", postal_code);
                }
            }

            return list;
        }
    }
}
