using System.Web.Optimization;

namespace ChargerID.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/scripts/jquery-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                //.Include("~/Content/site.css")
                .Include("~/Content/bootstrap.css"));
                //.Include("~/Content/font-awesome.css")
                //.Include("~/Content/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
        }
    }
}