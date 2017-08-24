using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ChargerID.UI.Controllers
{
    public class MapController : Controller
    {
        // GET /Map
        public ActionResult Index()
        {
            return View();
        }
    }
}