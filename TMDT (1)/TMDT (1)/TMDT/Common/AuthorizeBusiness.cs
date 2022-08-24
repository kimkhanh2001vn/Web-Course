using System;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TMDT;
using TMDT.Models.CustomModel;

namespace Common
{
    public class AuthorizeBusiness : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TapChiDB db = new TapChiDB();            
            if (HttpContext.Current.Session["UserInfo"] == null)
            {
                string userName = UserCookiesManager.GetUserNameFromRequestCookie();
                if (string.IsNullOrEmpty(userName))
                {                    
                    filterContext.Result = new RedirectResult("/Admin/Home/Login");
                    return;
                }
                else
                {
                    UserInfo userInfo = db.UserAdministrators
                                .Where(x => x.UserName == userName && x.Allowed == true)
                                .Include(x=>x.RoleID)
                                .Select(x => new UserInfo
                                {
                                    UserId = x.UserId,
                                    UserName = x.UserName,
                                    FullName = x.FullName,
                                    Email = x.Email,
                                    Avatar = x.Avatar,  
                                    RoleID = x.RoleID,
                                    RoleName = x.UserName,
                                    CreatedDate = x.CreatedDate
                                }).FirstOrDefault();
                    HttpContext.Current.Session["UserInfo"] = userInfo;
                }
            }                   

            int userId = int.Parse(((UserInfo)HttpContext.Current.Session["UserInfo"]).UserId.ToString());
            //Lấy thông tin user
            UserAdministrator admin = db.UserAdministrators
                .Where(a => a.UserId == userId && a.RoleID == 1)
                .FirstOrDefault();

            //Nếu là admin  thì mặc nhiên được vào và không cần kiểm tra
            if (admin != null)
            {
                return;
            }

            //Lấy ra tên các permission được gán cho người dùng
            IQueryable<string> listpermission = from p in db.UserPermissions
                                                join g in db.UserGrantPermissions on p.PermissionId equals g.PermissionId
                                                where g.UserId == userId
                                                select p.PermissionName;
            //Lấy tên action
            string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
                + "Controller-" +
                filterContext.ActionDescriptor.ActionName;

            //Kiểm tra xem các permision có chứa tên action mà người dùng kich hoạt hay không?
            //Nếu không thì nhẩy tới trang thông báo
            if (!listpermission.Contains(actionName))
            {
                filterContext.Result = new RedirectResult("/Admin/Home/NotificationAuthorize");
                return;
            }
        }       
    }
}