using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TMDT
{
    public class MvcApplication : System.Web.HttpApplication
    {
        int ThisInstanceID = new Random().Next(0, 1000000);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

      

            //Log Application 
            LogApp("Application_Start");    
        }

        protected void Application_BeginRequest()
        {
            LogApp("Application_BeginRequest");
        }

        protected void Application_EndRequest()
        {
            LogApp("Application_EndRequest");
        }


        protected void Application_End()
        {
            LogApp("Application_End");
        }

        private void LogApp(string methodString)
        {
            TapChiDB db = new TapChiDB();
            db.LogTestApplications.Add(new LogTestApplication
            {
                CreatedDate = DateTime.Now,
                InstanceID = ThisInstanceID,
                Description = methodString
            });
            db.SaveChanges();
        }
    }
}
