using Common;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TMDT.Models.CustomModel;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UserGrantPermissionsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/UserGrantPermissions
        public async Task<ActionResult> Index(int id) // id: user to be granted 's id
        {
            //Lưu id của người dùng đang được cấp ra session
            ViewData["userGrantID"] = id;
            //Lấy người dùng
            UserAdministrator usergrant = await db.UserAdministrators.FindAsync(id);
            //Lưu tên ra biến
            ViewBag.usergrant = ": " + usergrant.UserName + " " + '(' + usergrant.FullName + ')';

            //Lấy tất cả các nghiệp vụ (controller) trong csdl
            IQueryable<UserBusiness> listcontrol = db.UserBusinesses.Where(x => x.Status == true);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (UserBusiness item in listcontrol)
            {
                items.Add(new SelectListItem() { Text = item.BusinessName, Value = item.BusinessId });
            }
            //Lưu ra ViewBag gắn vô select element
            ViewBag.items = items;
           
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View();
        }

        //Lấy danh sách quyền đang được cấp cho người dùng
        public JsonResult getPermissions(string id, int usertemp)
        {
            //Lấy tất cả các permission của user và của business
            List<PermissionAction> listgranted = (from g in db.UserGrantPermissions
                                                  join p in db.UserPermissions on g.PermissionId equals p.PermissionId
                                                  where g.UserId == usertemp && p.BusinessId == id
                                                  select new PermissionAction { PermissionId = p.PermissionId, PermissionName = p.PermissionName, Description = p.Description, IsGranted = true }).ToList();

            //Lấy tất cả các permission của business hiện tại
            IQueryable<PermissionAction> listpermission = from p in db.UserPermissions.Where(x => x.Status == true)
                                                          where p.BusinessId == id
                                                          select new PermissionAction { PermissionId = p.PermissionId, PermissionName = p.PermissionName, Description = p.Description, IsGranted = false };

            //Lấy tất cả id của permission đã được gán ở trên cho người dùng
            IEnumerable<int> listpermissionId = listgranted.Select(p => p.PermissionId);

            //So sánh kiểm tra permission của business mà chưa có trong listgrant thì đưa vào (IsGrant=false)
            foreach (PermissionAction item in listpermission)
            {
                if (!listpermissionId.Contains(item.PermissionId))
                {
                    listgranted.Add(item);
                }
            }
            return Json(listgranted.OrderBy(x => x.Description), JsonRequestBehavior.AllowGet);

        }

        //Cập nhật quyền cho người dùng
        public async Task<string> updatePermission(int id, int usertemp)
        {
            string msg = "";
            string log = "";
            UserGrantPermission grant = db.UserGrantPermissions.Find(id, usertemp);
            if (grant == null)
            {
                UserGrantPermission g = new UserGrantPermission()
                {
                    PermissionId = id,
                    UserId = usertemp,
                    Description = ""
                };

                db.UserGrantPermissions.Add(g);
                msg = "<div class='alert alert-success'>Quyền cấp thành công</div>";
                log = "Granted";
            }
            else
            {
                db.UserGrantPermissions.Remove(grant);
                msg = "<div class='alert alert-danger'>Quyền hủy thành công</div>";
                log = "Ungranted";
            }
            db.SaveChanges();
            await LogActivity.LogSavedActivity(log+ "UserId("+ usertemp+")-" + "PermissionID(" + id +") ");
            return msg;
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
