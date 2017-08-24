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
