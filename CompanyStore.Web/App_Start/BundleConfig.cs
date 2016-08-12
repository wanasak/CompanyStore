using System.Web;
using System.Web.Optimization;

namespace CompanyStore.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/Vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/Vendors/jquery.js", 
                "~/Scripts/Vendors/bootstrap.js", 
                "~/Scripts/Vendors/toastr.js", 
                //"~/Scripts/Vendors/jquery.raty.js", 
                "~/Scripts/Vendors/respond.src.js", 
                "~/Scripts/Vendors/angular.js", 
                "~/Scripts/Vendors/angular-route.js", 
                "~/Scripts/Vendors/angular-cookies.js", 
                "~/Scripts/Vendors/angular-validator.js", 
                "~/Scripts/Vendors/angular-base64.js",
                "~/Scripts/Vendors/angular-file-upload.js",
                "~/Scripts/Vendors/angucomplete-alt.min.js", 
                "~/Scripts/Vendors/ui-bootstrap-tpls-0.13.1.js", 
                "~/Scripts/Vendors/underscore.js", 
                "~/Scripts/Vendors/raphael.js", 
                "~/Scripts/Vendors/morris.js", 
                "~/Scripts/Vendors/jquery.fancybox.js",
                "~/Scripts/Vendors/jquery.fancybox-media.js",
                "~/Scripts/Vendors/jquery.dataTables.js",
                "~/Scripts/Vendors/angular-datatables.js",
                "~/Scripts/Vendors/angular-datatables.buttons.js",
                "~/Scripts/Vendors/dataTables.buttons.js",
                "~/Scripts/Vendors/dataTables.responsive.js",
                "~/Scripts/Vendors/buttons.print.js",
                "~/Scripts/Vendors/buttons.html5.js",
                "~/Scripts/Vendors/loading-bar.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/spa/modules/common.core.js",
                "~/Scripts/spa/modules/common.ui.js",
                "~/Scripts/spa/app.js",
                "~/Scripts/spa/layout/sideBar.directive.js",
                "~/Scripts/spa/layout/topBar.directive.js",
                "~/Scripts/spa/layout/customPager.directive.js",
                "~/Scripts/spa/services/notificationService.js",
                "~/Scripts/spa/services/membershipService.js",
                "~/Scripts/spa/services/fileUploadService.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/directives/availableDevice.directive.js",
                "~/Scripts/spa/directives/ngThumb.directive.js",
                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/account/registerCtrl.js",
                "~/Scripts/spa/device/deviceCtrl.js",
                "~/Scripts/spa/device/deviceDetailCtrl.js",
                "~/Scripts/spa/device/deviceAddCtrl.js",
                "~/Scripts/spa/employee/employeeAddCtrl.js",
                "~/Scripts/spa/employee/employeeCtrl.js",
                "~/Scripts/spa/employee/deleteEmployeeModalCtrl.js",
                "~/Scripts/spa/employee/editEmployeeModalCtrl.js",
                "~/Scripts/spa/rental/rentalCtrl.js",
                "~/Scripts/spa/rental/rentalModalCtrl.js",
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/content/css/site.css", 
                "~/content/css/bootstrap.css", 
                "~/content/css/bootstrap-theme.css", 
                "~/content/css/font-awesome.css",
                "~/content/css/morris.css",
                "~/content/css/buttons.dataTables.css",
                "~/content/css/dataTables.responsive.css",
                "~/content/css/jquery.dataTables.css", 
                "~/content/css/toastr.css", 
                "~/content/css/jquery.fancybox.css", 
                "~/content/css/loading-bar.css"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
