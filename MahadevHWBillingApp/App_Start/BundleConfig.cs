﻿using System.Web;
using System.Web.Optimization;

namespace MahadevHWBillingApp
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
                "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/customValidation.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/jquery.datetextentry.js",
                "~/Scripts/vue.min.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap4.min.js",
                "~/Scripts/select2.min.js",
                "~/Scripts/customValidation.js",
                 "~/Scripts/customSelect.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/font-awesome.css",
                "~/Content/jquery.datetextentry.css",
                "~/Content/dataTables.bootstrap4.min.css",
                "~/Content/select2.min.css"));

           // BundleTable.EnableOptimizations = true;
        }
    }
}
