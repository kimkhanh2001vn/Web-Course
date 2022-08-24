using System.Web;
using System.Web.Optimization;

namespace TMDT
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Resources/bundles/css")
            .Include("~/Resources/css/jquery.bxslider.css")
            .Include("~/Areas/Admin/Resources/plugins/datepicker/datepicker3.css")
            .Include("~/Resources/css/slick.css")
            .Include("~/Resources/css/reset.css")
            .Include("~/Resources/css/style.css")
            .Include("~/Resources/css/media-queries.css")
            .Include("~/Resources/css/OtherCSS.css")
            .Include("~/Areas/Admin/Resources/ckeditor/fonts.css")
            );

            bundles.Add(new ScriptBundle("~/Resources/bundles/js")
            .Include("~/Resources/js/jquery.js")
            .Include("~/Resources/js/jquery.bxslider.min.js")
            .Include("~/Resources/js/jquery.nicescroll.js")
            .Include("~/Resources/js/jquery.infinitescroll.min.js")
            .Include("~/Areas/Admin/Resources/bootstrap/js/bootstrap.min.js")
            .Include("~/Resources/js/SubscribeModal.js")
            .Include("~/Resources/js/slick.min.js")
            .Include("~/Areas/Admin/Resources/plugins/datepicker/bootstrap-datepicker.js")
            .Include("~/Areas/Admin/Resources/plugins/datepicker/locales/bootstrap-datepicker.vi.js"));

            bundles.Add(new StyleBundle("~/Areas/Admin/Resources/bundles/css")
                .Include("~/Areas/Admin/Resources/bootstrap/css/bootstrap.min.css")
                .Include("~/Areas/Admin/Resources/css/font-awesome.min.css")
                .Include("~/Areas/Admin/Resources/plugins/datatables/dataTables.bootstrap.css")
                .Include("~/Areas/Admin/Resources/dist/css/AdminLTE.min.css")
                .Include("~/Areas/Admin/Resources/dist/css/skins/_all-skins.min.css")
                .Include("~/Areas/Admin/Resources/plugins/morris/morris.css")
                .Include("~/Areas/Admin/Resources/plugins/jvectormap/jquery-jvectormap-1.2.2.css")
                //.Include("~/Areas/Admin/Resources/plugins/datepicker/datepicker3.css")
                .Include("~/Areas/Admin/Resources/plugins/daterangepicker/daterangepicker.css")
                .Include("~/Areas/Admin/Resources/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css")
                .Include("~/Areas/Admin/Resources/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker(mod).min.css")
                .Include("~/Areas/Admin/Resources/css/style.css")
                .Include("~/Areas/Admin/Resources/ckeditor/fonts.css")
               
             );

            bundles.Add(new ScriptBundle("~/Areas/Admin/Resources/bundles/js")
                .Include("~/Areas/Admin/Resources/plugins/jQuery/jquery-2.2.3.min.js")
                .Include("~/Areas/Admin/Resources/js/sweetalert.js")
                .Include("~/Areas/Admin/Resources/Knockout/knockout-3.4.2.js")
                .Include("~/Areas/Admin/Resources/js/jquery-ui.min.js")
                .Include("~/Areas/Admin/Resources/bootstrap/js/bootstrap.min.js")
                .Include("~/Areas/Admin/Resources/js/raphael-min.js")
                .Include("~/Areas/Admin/Resources/plugins/morris/morris.min.js")
                .Include("~/Areas/Admin/Resources/plugins/sparkline/jquery.sparkline.min.js")
                .Include("~/Areas/Admin/Resources/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js")
                .Include("~/Areas/Admin/Resources/plugins/jvectormap/jquery-jvectormap-world-mill-en.js")
                .Include("~/Areas/Admin/Resources/plugins/knob/jquery.knob.js")
                .Include("~/Areas/Admin/Resources/plugins/momentjs/moment-with-locales.min.js")
                .Include("~/Areas/Admin/Resources/plugins/daterangepicker/daterangepicker.js")
                .Include("~/Areas/Admin/Resources/plugins/datepicker/bootstrap-datepicker.js")
                .Include("~/Areas/Admin/Resources/plugins/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js")
                .Include("~/Areas/Admin/Resources/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.js")
                .Include("~/Areas/Admin/Resources/plugins/datatables/jquery.dataTables.min.js")
                .Include("~/Areas/Admin/Resources/plugins/datatables/plugin/date-uk.js")
                .Include("~/Areas/Admin/Resources/plugins/datatables/plugin/datetime-moment.js")
                .Include("~/Areas/Admin/Resources/plugins/datatables/dataTables.bootstrap.min.js")
                .Include("~/Areas/Admin/Resources/plugins/slimScroll/jquery.slimscroll.min.js")
                .Include("~/Areas/Admin/Resources/plugins/fastclick/fastclick.js")
                .Include("~/Areas/Admin/Resources/dist/js/app.js")
                .Include("~/Areas/Admin/Resources/dist/js/demo.js")
                .Include("~/Areas/Admin/Resources/js/ChangesStatus/ChangeStatus.js")
                .Include("~/Areas/Admin/Resources/js/Delete/DeleteConfirm.js")
                .Include("~/Areas/Admin/Resources/js/selectImage.js")
                );

            BundleTable.EnableOptimizations = true;
        }
    }
}
