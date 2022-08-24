using Common;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TMDT.Models.DAO;
namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UserAdministratorsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/UserAdministrators
        public async Task<ActionResult> Index() {
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(await db.UserAdministrators.Include(x=>x.UserRole).ToListAsync());
        }

        // GET: Admin/UserAdministrators/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserAdministrator userAdministrator = await db.UserAdministrators.Where(x => x.UserId == id.Value).Include(x => x.UserRole).FirstOrDefaultAsync();
            if (userAdministrator == null)
            {
                return HttpNotFound();
            }
            return View(userAdministrator);
        }

        // GET: Admin/UserAdministrators/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.RoleID = new SelectList(await db.UserRoles.ToListAsync(),"ID","Name");
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View();
        }

        // POST: Admin/UserAdministrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,UserName,Password,FullName,Email,Avatar,RoleID,Allowed,CreatedDate")] UserAdministrator userAdministrator)
        {           
            if (ModelState.IsValid)
            {
                UserAdministrator user = db.UserAdministrators.SingleOrDefault(x => x.UserName == userAdministrator.UserName);
                UserAdministrator email = db.UserAdministrators.SingleOrDefault(x => x.Email == userAdministrator.Email);
                string passMD5 = Encryptor.HashAdminPassword(userAdministrator.UserName, userAdministrator.Password);
                userAdministrator.Password = passMD5;
                userAdministrator.CreatedDate = DateTime.Now;
                if (user == null)
                {
                    if (email == null)
                    {
                        db.UserAdministrators.Add(userAdministrator);                        
                        await db.SaveChangesAsync();
                        await LogActivity.LogSavedActivity(userAdministrator);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Email đã có người dùng, nhập Email khác";
                    }
                }
                else
                {
                    ViewBag.error = "Tài khoản đã tồn tại";
                }
            }
            ViewBag.RoleID = new SelectList(await db.UserRoles.ToListAsync(), "ID", "Name", userAdministrator.RoleID);
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userAdministrator);
        }
        // GET: Admin/UserAdministrators/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserAdministrator userAdministrator = await db.UserAdministrators.FindAsync(id);
            if (userAdministrator == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(await db.UserRoles.ToListAsync(), "ID", "Name",userAdministrator.RoleID);
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userAdministrator);
        }

        // POST: Admin/UserAdministrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,UserName,Password,FullName,Email,Avatar,RoleID,Allowed,CreatedDate")] UserAdministrator userAdministrator)
        {
            if (userAdministrator.Password == null)
            {
                userAdministrator.Password = "";
                ModelState.Remove("Password");
            }
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.UserAdministrators.Attach(userAdministrator);                
                db.Entry(userAdministrator).Property(x => x.FullName).IsModified = true;
                db.Entry(userAdministrator).Property(x => x.Email).IsModified = true;
                db.Entry(userAdministrator).Property(x => x.Avatar).IsModified = true;
                db.Entry(userAdministrator).Property(x => x.RoleID).IsModified = true;
                db.Entry(userAdministrator).Property(x => x.Allowed).IsModified = true;                
                if (!string.IsNullOrEmpty(userAdministrator.Password))
                {
                    userAdministrator.Password = Encryptor.HashAdminPassword(userAdministrator.UserName, userAdministrator.Password); 
                    db.Entry(userAdministrator).Property(x => x.Password).IsModified = true;                    
                }
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(userAdministrator);
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(await db.UserRoles.ToListAsync(), "ID", "Name", userAdministrator.RoleID);
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userAdministrator);
        }

        public async Task<ActionResult> DeleteConfirmed(int id, string category)
        {
            UserAdministrator userAdministrator = await db.UserAdministrators.FindAsync(id);
            db.UserAdministrators.Remove(userAdministrator);
            await db.SaveChangesAsync();
            await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(userAdministrator));
            return Json(new { redirectUrl = Url.Action("Index", category), isRedirect = true }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ChangeStatus(int id)
        {
            bool result = new UserDAO().ChangeStatus(id);
            await LogActivity.LogSavedActivity("Change UserID(" + id + ")-Status: " + result);
            return Json(new
            {
                status = result
            });
        }

        public async Task<string> ChangeImage(int? id, string picture)
        {            
            if (id == null)
            {
                return "Mã không tồn tại";
            }

            UserAdministrator user = db.UserAdministrators.Find(id);
            if (user == null)
            {
                return "Mã không tồn tại";
            }
            user.Avatar = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change UserID(" + id + ")-Image: " + picture);
            return "";
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
