using System.Web;
using System.Web.Optimization;

namespace OnlineShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jscore").Include(
                        "~/Assets/Client/js/jquery-1.11.3.min.js",
                        "~/Assets/Client/js/jquery-ui.js",
                        "~/Assets/Client/js/bootstrap.min.js",
                        "~/Assets/Client/js/move-top.js",
                        "~/Assets/Client/js/easing.js",
                        "~/Assets/Client/js/startstop-slider.js",
                        "~/Assets/Client/js/controller/baseController.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/controller").Include(
                       "~/Assets/Client/js/controller/baseController.js"
                       ));

            bundles.Add(new StyleBundle("~/bundles/core").Include(
                      "~/Assets/Client/css/bootstrap.css",
                      "~/Assets/Client/css/boostrap-social.css",
                      "~/Assets/Client/css/font-awesome.min.css",
                      "~/Assets/Client/css/bootstrap-theme.css",
                      "~/Assets/Client/css/jquery-ui.css",
                      "~/Assets/Client/css/style.css",
                      "~/Assets/Client/css/slider.css"
                      ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
