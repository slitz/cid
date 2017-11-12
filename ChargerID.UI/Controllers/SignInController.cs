using System.Web.Mvc;
using ChargerID.DataAccess;
using ChargerID.Configuration;
using ChargerID.UI.Models;
using System.Web.Security;

namespace ChargerID.UI.Controllers
{
    public class SignInController : Controller
    {
        private readonly IDataAccess _data;

        private readonly IConfig _config;
        protected IConfig Config
        {
            get { return _config; }
        }

        public SignInController()
            : this(null, null)
        {

        }

        public SignInController(IDataAccess data = null, IConfig config = null)
        {
            _data = data ?? new DataAccess.DataAccess();
            _config = config ?? new Config();
        }

        // GET /Sign In Page
        [AllowAnonymous]
        public ActionResult Index()
        {
            var userModel = new UserModel();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Index(UserModel user, string returnUrl = "/Admin")
        {
            if (user != null)
            {
                string username = user.Username;
                string password = user.Password;

                if (username == _config.Ui.Username && password == _config.Ui.Password)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "SignIn");
                    }
                }
                else
                {
                    user.Error = "The username or password is incorrect.";
                }
            }

            return View(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Dashboard");
        }  
    }
}