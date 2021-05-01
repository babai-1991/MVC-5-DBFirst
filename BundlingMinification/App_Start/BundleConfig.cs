using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BundlingMinification
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // there are 2 types of bundles 
            //1- script bundle
            //2- style bundle

            bundles.Add(new ScriptBundle("~/bundles/scriptbundler1")
                                .Include(
                                    "~/Scripts/script1.js",
                                    "~/Scripts/script2.js",
                                    "~/Scripts/script3.js",
                                    "~/Scripts/script4.js",
                                    "~/Scripts/script5.js"
                                ));
            bundles.Add(new ScriptBundle("~/bundles/scriptbundler2")
                .Include(
                    "~/Scripts/script6.js",
                    "~/Scripts/script7.js",
                    "~/Scripts/script8.js",
                    "~/Scripts/script9.js",
                    "~/Scripts/script10.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/stylebundler1")
                .Include(
                    "~/Styles/style1.js",
                    "~/Styles/style2.js",
                    "~/Styles/style3.js",
                    "~/Styles/style4.js",
                    "~/Styles/style5.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/stylebundler2")
                .Include(
                    "~/Styles/style6.js",
                    "~/Styles/style7.js",
                    "~/Styles/style8.js",
                    "~/Styles/style9.js",
                    "~/Styles/style10.js"
                ));
            // very very important , else nothing wont work as far as bundling and minification is concerned
            BundleTable.EnableOptimizations = true;
        }
    }
}