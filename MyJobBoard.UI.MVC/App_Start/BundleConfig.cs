using System.Web.Optimization;

namespace MyJobBoard.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Content/vendor").Include(
               "~/Content/vendor/aos/aos.js", 
               "~/Content/assests/vendor/bootstrap/js/bootstrap.bundle.min.js", 
               "~/Content/vendor/glightbox/js/glightbox.min.js", 
               "~/Content/vendor/isotope-layout/isotope.pkgd.min.js", 
               "~/Content/vendor/php-email-form/validate.js",
               "~/Content/vendor/purecounter/purecounter.js",
               "~/Content/vendor/swiper/swiper-bundle.min.js",
               "~/Content/vendor/typed.js/typed.min.js", 
               "~/Content/vendor/waypoints/noframework.waypoints.js",
               "~/Content/js/main.js"
                ));

            bundles.Add(new StyleBundle("~/Content/archive").Include(
                   "~/Content/vendor/aos/aos.css", 
                  "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                  "~/Content/vendor/bootstrap-icons/bootstrap-icons.css",
                 "~/Content/vendor/boxicons/css/boxicons.min.css",
                 "~/Content/vendor/glightbox/css/glightbox.min.css", 
                  "~/Content/vendor/swiper/swiper-bundle.min.css",
                  "~/Content/css/style.css"
                   ));
        }
    }
}
