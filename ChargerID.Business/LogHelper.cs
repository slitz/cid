using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using System.IO;

namespace ChargerID.Business
{
    public interface ILogHelper
    {
        void WriteInfo(string msg, params object[] p);
        void WriteError(string msg, params object[] p);
    }

    public class LogHelper : ILogHelper
    {
        private readonly ILog log;

        public LogHelper()
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            log = LogManager.GetLogger(declaringType);

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFileName = String.Format("{0}\\log4net.config", path);
            var configFileInfo = new FileInfo(configFileName);
            log4net.Config.XmlConfigurator.Configure(configFileInfo);
        }

        public void WriteInfo(string msg, params object[] p)
        {
            log.InfoFormat(msg, p);
        }

        public void WriteError(string msg, params object[] p)
        {
            log.ErrorFormat(msg, p);
        }
    }
}
