using System.Web.Mvc;
using ChargerID.DataAccess;
using ChargerID.Configuration;

namespace ChargerID.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDataAccess _data;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public AdminController()
            : this(null, null)
        {

        }

        public AdminController(IDataAccess data = null, IConfig config = null)
        {
            _data = data ?? new DataAccess.DataAccess();
            _config = config ?? new Config();
        }

        // GET /Admin Page
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}