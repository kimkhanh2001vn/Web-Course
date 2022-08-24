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
    public class FooterContentsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/FooterContents
        public async Task<ActionResult> Index()
        {
            return View(await db.FooterContents.ToListAsync());
        }             

        // GET: Admin/FooterContents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FooterContent footerContent = await db.FooterContents.FindAsync(id);
            if (footerContent == null)
            {
                return HttpNotFound();
            }
            return View(footerContent);
        }

        // POST: Admin/FooterContents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Image,Link,Status")] FooterContent footerContent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(footerContent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(footerContent);
                return RedirectToAction("Index");
            }
            return View(footerContent);
        }

        public async Task<JsonResult> ChangeStatus(int id)
        {
            var result = new ChangesDAO().FooterContentStatus(id);
            await LogActivity.LogSavedActivity("Change FooterContentsID(" + id + ")-Status(" + result + ")");
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
