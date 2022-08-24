using System.Web.Mvc;

namespace TMDT.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "TMDT.Areas.Admin.Controllers" }
            );
        }
    }
}