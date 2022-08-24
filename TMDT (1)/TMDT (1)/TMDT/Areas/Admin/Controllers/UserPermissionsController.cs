using Common;
using Newtonsoft.Json;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class UserPermissionsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/UserPermissions
        public async Task<ActionResult> Index(string id) //This id actually means category, but for easy parsing we use id
        {
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            IQueryable<UserPermission> permissions = db.UserPermissions.Where(u => u.BusinessId == id && u.Status.Value == true);
            return View(await permissions.ToListAsync());
        }       

        // GET: Admin/UserPermissions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserPermission userPermission = await db.UserPermissions.FindAsync(id);
            if (userPermission == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = new SelectList(db.UserBusinesses, "BusinessId", "BusinessName", userPermission.BusinessId);
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            return View(userPermission);
        }

        // POST: Admin/UserPermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PermissionId,PermissionName,Description,BusinessId,Status")] UserPermission userPermission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPermission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "UserPermissions", new { id = userPermission.BusinessId });
            }

            ViewBag.BusinessId = new SelectList(db.UserBusinesses, "BusinessId", "BusinessName", userPermission.BusinessId);
            ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
            await LogActivity.LogSavedActivity(userPermission);
            return View(userPermission);
        }
       
        public async Task<ActionResult> DeleteConfirmed(int id,string category)
        {
            UserPermission userPermission = await db.UserPermissions.FindAsync(id);
            string permissionCategory = userPermission.BusinessId;
            db.UserPermissions.Remove(userPermission);
            await db.SaveChangesAsync();
            await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(userPermission));
            return Json(new { redirectUrl = Url.Action("Index", category, new { id = permissionCategory }), isRedirect = true }, JsonRequestBehavior.AllowGet);
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