using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UserBusinessesController : Controller
    {
        private TapChiDB db = new TapChiDB();
        //Cập nhật Button on View . 
        //This will only add new Business(Controller name), PermissionName ( Controller-ActionName) to current DB
        public async Task<ActionResult> UpdateBusiness() 
        {            
            ReflectionController rc = new ReflectionController();
            List<Type> listControllerType = rc.GetControllers("TMDT.Areas.Admin.Controllers");
            List<string> listControllerOld = db.UserBusinesses.Select(c => c.BusinessId).ToList(); //BusinessID is string
            List<string> listPermistionOld = db.UserPermissions.Select(p => p.PermissionName).ToList(); 
            foreach (Type c in listControllerType)
            {
                if (!listControllerOld.Contains(c.Name))
                {
                    UserBusiness b = new UserBusiness() { BusinessId = c.Name, BusinessName = c.Name + " Name",Status = true};
                    db.UserBusinesses.Add(b);
                }

                List<string> listActionInController = rc.GetActions(c);
                foreach (string action in listActionInController)
                {
                    if (!listPermistionOld.Contains(c.Name + "-" + action))
                    {
                        
                        UserPermission permission = new UserPermission() { PermissionName = c.Name + "-" + action, Description = c.Name + " | " + action, BusinessId = c.Name, Status = true };
                        db.UserPermissions.Add(permission);
                    }
                }
            }
            db.SaveChanges();
            await LogActivity.LogSavedActivity("");
            TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span><span class='sr-only'></span> Cập nhật thành công</div>";
            return RedirectToAction("Index");
        }

        // GET: Admin/UserBusinesses
        public async Task<ActionResult> Index()
        {
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(await db.UserBusinesses.Where(x => x.Status == true || x.Status == null).ToListAsync());
        }       

        // GET: Admin/UserBusinesses/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserBusiness userBusiness = await db.UserBusinesses.FindAsync(id);
            if (userBusiness == null)
            {
                return HttpNotFound();
            }
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userBusiness);
        }

        // POST: Admin/UserBusinesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BusinessId,BusinessName,Status")] UserBusiness userBusiness)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userBusiness).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(userBusiness);               
                return RedirectToAction("Index");
            }
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userBusiness);
        }
       
        public async Task<ActionResult> DeleteConfirmed(string id, string category)
        {
            UserBusiness userBusiness = await db.UserBusinesses.FindAsync(id);
            db.UserBusinesses.Remove(userBusiness);
            await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(userBusiness));
            await db.SaveChangesAsync();
            return Json(new { redirectUrl = Url.Action("Index", category), isRedirect = true }, JsonRequestBehavior.AllowGet);
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
