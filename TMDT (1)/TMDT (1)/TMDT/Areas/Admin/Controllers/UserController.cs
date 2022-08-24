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
using TMDT.Models.CustomModel;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UserController : Controller
    {
        private TapChiDB db = new TapChiDB();
       
        // GET: Admin/User/Edit/5
        public async Task<ActionResult> Edit()
        {
            int id = ((UserInfo)Session["UserInfo"]).UserId;
            UserAdministrator userAdministrator = await db.UserAdministrators.FindAsync(id);
            if (userAdministrator == null)
            {
                return HttpNotFound();
            }
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userAdministrator);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserName,Password,FullName,Email,Avatar")] UserAdministrator userAdministrator)
        {            
            if (userAdministrator.Password == null)
            {
                userAdministrator.Password = "";
                ModelState.Remove("Password");
            }
                userAdministrator.UserId = ((UserInfo)Session["UserInfo"]).UserId;
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.UserAdministrators.Attach(userAdministrator);
                db.Entry(userAdministrator).Property(x => x.FullName).IsModified = true;
                db.Entry(userAdministrator).Property(x => x.Email).IsModified = true;
                db.Entry(userAdministrator).Property(x => x.Avatar).IsModified = true;              
                if (!string.IsNullOrEmpty(userAdministrator.Password))
                {
                    userAdministrator.Password = Encryptor.HashAdminPassword(userAdministrator.UserName, userAdministrator.Password);
                    db.Entry(userAdministrator).Property(x => x.Password).IsModified = true;
                }
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(userAdministrator);
                TempData["Saved"] = 1; //This will notify success message , this tempdata is putin layout
                return RedirectToAction("Edit");
            }
            ViewBag.RoleID = new SelectList(await db.UserRoles.ToListAsync(), "ID", "Name", userAdministrator.RoleID);
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userAdministrator);
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
