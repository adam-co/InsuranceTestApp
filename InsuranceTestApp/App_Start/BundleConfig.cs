﻿using System.Web.Optimization;

namespace InsuranceTestApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/InsuranceTestApp").Include(
                "~/Scripts/insurance.test.app.js"));

            bundles.Add(new ScriptBundle("~/bundles/jtable").Include(
                "~/Scripts/jquery-ui-1.9.2.js",
                "~/Scripts/jtable/jquery.jtable.js"));

            bundles.Add(new StyleBundle("~/Content/jtable").Include(
                "~/Content/custom-jtable.css",
                "~/Scripts/jtable/themes/jqueryui/jtable_jqueryui.css",
                "~/Content/themes/base/jquery-ui.css"));

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
        }
    }
}