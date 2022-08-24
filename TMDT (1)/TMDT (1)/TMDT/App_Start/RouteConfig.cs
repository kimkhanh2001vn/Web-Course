using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TMDT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "DaoTao",
      url: "Dao-Tao/{title}-{id}",
      defaults: new { controller = "Home", action = "DaoTao", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Trang Chi Tiet",
      url: "Trang-Chu/Chi-Tiet/{Alias}-{id}",
      defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Khoa Hoc Mien Phi",
      url: "Trang-Chu/Khoa-Hoc-Mien-Phi/{Alias}-{id}",
      defaults: new { controller = "Home", action = "UserDetails",  id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Dao Tao",
      url: "Dao-Tao",
      defaults: new { controller = "Home", action = "DaoTao", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Trang-Chu/Co-Van",
      url: "Trang-Chu/Co-Van",
      defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Trang-Chu/Thue-Co-So",
      url: "Trang-Chu/Thue-Co-So",
      defaults: new { controller = "Home", action = "thue", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Trang-Chu/ve-chung-toi",
      url: "Trang-Chu/ve-chung-toi",
      defaults: new { controller = "Home", action = "AboutUs", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Trang-Chu/Tu-Van",
      url: "Trang-Chu/Tu-Van",
      defaults: new { controller = "Home", action = "TuVan", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Trang-Chu/Thue-Can-Ho",
      url: "Trang-Chu/Thue-Can-Ho",
      defaults: new { controller = "Home", action = "thuecanho", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Trang-Chu/Vat-Lieu-Xay-Dung",
      url: "Trang-Chu/Vat-Lieu-Xay-Dung",
      defaults: new { controller = "Home", action = "VatLieu", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
      name: "Trang-Chu/Vat-Lieu-Da",
      url: "Trang-Chu/Vat-Lieu-Da",
      defaults: new { controller = "Home", action = "VatLieuda", id = UrlParameter.Optional },
          new string[] { "TMDT.Controllers" }
  );
            routes.MapRoute(
                     name: "Trang Chu",
                     url: "Trang-Chu",
                     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                         new string[] { "TMDT.Controllers" }
                 );
        
        routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    new string[] { "TMDT.Controllers" }
            );
        }
    }
}
