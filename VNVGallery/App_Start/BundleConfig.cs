using System.Web;
using System.Web.Optimization;

namespace VnVGallery
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                "~/Scripts/lightbox.js"));
            bundles.Add(new ScriptBundle("~/bundles/lazyload").Include(
                "~/Scripts/lazyload/lazyload.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/gallery.css",
                      "~/Content/html5.css",
                      "~/Content/jquery.lightbox-0.5.css",
                      "~/Content/jquery-ui-1.8.9.custom.css",
                      "~/Content/lightbox.css",
                      "~/Content/lytebox.css",
                      "~/Content/screen.css"));
        }
    }
}
