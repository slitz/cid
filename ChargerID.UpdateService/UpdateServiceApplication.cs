using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargerID.UpdateService
{
    public class UpdateServiceApplication
    {
        private static IUpdateService _updateService;

        public static IUpdateService UpdateService
        {
            get { return _updateService ?? (_updateService = new UpdateService()); }
            set { _updateService = value; }
        }

        public static int Main(string[] args)
        {
            try
            {
                UpdateService.RunUpdate();
                return 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return -1;
            }
        }
    }
}
