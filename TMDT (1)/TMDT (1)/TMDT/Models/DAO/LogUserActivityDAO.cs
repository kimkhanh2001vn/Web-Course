using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TMDT.Models.CustomModel;

namespace TMDT.Models.DAO
{
    public class LogUserActivityDAO
    {
        TapChiDB db = new TapChiDB();
        public async Task LogSavedActivity(string content)
        {
            UserInfo currentUser = (UserInfo)HttpContext.Current.Session["UserInfo"];
            LogUserActivity log = new LogUserActivity {
                Action = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString() + "-" + HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString(),
                Content = content,
                UserID = currentUser.UserId,
                UserName = currentUser.UserName,
                CreatedDate = DateTime.Now                
            };
            db.LogUserActivities.Add(log);
            await db.SaveChangesAsync();
            
        }
    }
}