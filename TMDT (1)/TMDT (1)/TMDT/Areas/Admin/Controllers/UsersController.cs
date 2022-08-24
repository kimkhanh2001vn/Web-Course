using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMDT;
using Common;
using TMDT.Models.DAO;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UsersController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/Users
        //Get only users whose role > 2 (Phóng viên only)
        public async Task<ActionResult> Index()
        {
            var userAdministrators = db.UserAdministrators.Where(x=>x.UserRole.ID>2).Include(u => u.UserRole);
            return View(await userAdministrators.ToListAsync());
        }

        //// GET: Admin/Users/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserAdministrator userAdministrator = await db.UserAdministrators.FindAsync(id);
        //    if (userAdministrator == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userAdministrator);
        //}

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,UserName,Password,FullName,Email,Avatar")] UserAdministrator userAdministrator)
        {
            if (ModelState.IsValid)
            {
                userAdministrator.Password = Encryptor.HashAdminPassword(userAdministrator.UserName, userAdministrator.Password);
                userAdministrator.Allowed = true;
                userAdministrator.CreatedDate = DateTime.Now;
                userAdministrator.RoleID = 3;
                db.UserAdministrators.Add(userAdministrator);
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(userAdministrator);
                return RedirectToAction("Index");
            }
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userAdministrator);
        }

        //// GET: Admin/Users/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserAdministrator userAdministrator = await db.UserAdministrators.FindAsync(id);
        //    if (userAdministrator == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.RoleID = new SelectList(db.UserRoles, "ID", "Name", userAdministrator.RoleID);
        //    return View(userAdministrator);
        //}

        //// POST: Admin/Users/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "UserId,UserName,Password,FullName,Email,Avatar,RoleID,Allowed,CreatedDate")] UserAdministrator userAdministrator)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(userAdministrator).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RoleID = new SelectList(db.UserRoles, "ID", "Name", userAdministrator.RoleID);
        //    return View(userAdministrator);
        //}

        //// GET: Admin/Users/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserAdministrator userAdministrator = await db.UserAdministrators.FindAsync(id);
        //    if (userAdministrator == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userAdministrator);
        //}

        //// POST: Admin/Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    UserAdministrator userAdministrator = await db.UserAdministrators.FindAsync(id);
        //    db.UserAdministrators.Remove(userAdministrator);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public async Task<JsonResult> ChangeStatus(int id)
        {
            if(db.UserAdministrators.Where(x=>x.UserId == id && x.UserRole.ID < 3).Any()) //Cant change User whose RoleID < 3
            {
                return Json("Error");
            }
            bool result = new UserDAO().ChangeStatus(id);
            await LogActivity.LogSavedActivity("Change UserID(" +id+ ")-Status("+result+")");
            return Json(new
            {
                status = result
            });
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
