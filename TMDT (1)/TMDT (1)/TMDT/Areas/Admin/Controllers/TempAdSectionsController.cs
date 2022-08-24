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

namespace TMDT.Areas.Admin.Controllers
{
    public class TempAdSectionsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/TempAdSections
        public async Task<ActionResult> Index()
        {
            return View(await db.TempAdSections.OrderBy(x=>x.Page).ToListAsync());
        }

        // GET: Admin/TempAdSections/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempAdSection tempAdSection = await db.TempAdSections.FindAsync(id);
            if (tempAdSection == null)
            {
                return HttpNotFound();
            }
            return View(tempAdSection);
        }

        // POST: Admin/TempAdSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ImageLink,RefererLink,ImageAltInfo")] TempAdSection tempAdSection)
        {
            if (ModelState.IsValid)
            {
                db.TempAdSections.Attach(tempAdSection);
                db.Entry(tempAdSection).Property(x => x.ImageLink).IsModified = true;
                db.Entry(tempAdSection).Property(x => x.RefererLink).IsModified = true;
                db.Entry(tempAdSection).Property(x => x.ImageAltInfo).IsModified = true;
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(tempAdSection);
                return RedirectToAction("Index");
            }
            return View(tempAdSection);
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
