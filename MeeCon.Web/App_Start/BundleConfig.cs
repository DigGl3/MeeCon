using System.Web;
using System.Web.Optimization;

namespace MeeCon.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/wwwroot/lib/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/wwwroot/lib/bootstrap/dist/js/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/wwwroot/js/site.js",
                "~/wwwroot/js/uikit.min.js",
                "~/wwwroot/js/simplebar.js",
                "~/wwwroot/js/script.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/wwwroot/css/style.css",
                "~/wwwroot/css/site.css",
                "~/wwwroot/css/tailwind.css",
                "~/wwwroot/css/_Layoutcs.css"));

            // Enable bundling and minification
            BundleTable.EnableOptimizations = true;
        }
    }
} 