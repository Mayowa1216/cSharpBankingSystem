using System.Web;
using System.Web.Optimization;

namespace OnlineBankingSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                   "~/Scripts/vendors/jquery.js",
                   "~/Scripts/vendors/bootstrap.bundle.min.js",
                   "~/Scripts/vendors/fastclick.js",
                   "~/Scripts/vendors/nprogress.js",
                   "~/Scripts/vendors/Chart.js",
                   "~/Scripts/vendors/guage.min.js",
                   "~/Scripts/vendors/icheck.min.js",
                   "~/Scripts/vendors/bootstrap-progressbar.js",
                   "~/Scripts/vendors/skycons.js",
                   "~/Scripts/vendors/jquery.flot.js",
                   "~/Scripts/vendors/jquery.flot.pie.js",
                   "~/Scripts/vendors/jquery.flot.time.js",
                   "~/Scripts/vendors/jquery.flot.stack.js",
                   "~/Scripts/vendors/jquery.flot.resize.js",
                   "~/Scripts/vendors/jquery.flot.orderBars.js",
                   "~/Scripts/vendors/jquery.flot.spline.js",
                   "~/Scripts/vendors/curvedLines.js",
                   "~/Scripts/vendors/date.js",
                   "~/Scripts/vendors/jquery.vmap.js",
                   "~/Scripts/vendors/jquery.vmap.sampledata.js",
                   "~/Scripts/vendors/jquery.vmap.world.js",
                   "~/Scripts/vendors/moment.js",
                   "~/Scripts/vendors/custom.js"
                ));

           bundles.Add(new StyleBundle("~/Content/vendors").Include(
                  "~/Content/vendors/bootstrap/dist/css/bootstrap.min.css",
                  "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                  "~/Content/vendors/nprogress/nprogress.css",
                  "~/Content/vendors/iCheck/skins/flat/green.css",
                  "~/Content/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                  "~/Content/vendors/jqvmap/dist/jqvmap.min.css",
                  "~/Content/vendors/bootstrap-daterangepicker/daterangepicker.css"
                ));

            bundles.Add(new StyleBundle("~/Content/build").Include(
                "~/Content/build/css/custom.min.css"));

           
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                        "~/Content/material-design-iconic-font.css",
                      "~/Content/site.css"
                    
                      ));
        }
    }
}
