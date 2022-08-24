using Common;
using TMDT.Models.DAO;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class PageDatasController : Controller
    {
        private TapChiDB db = new TapChiDB();

        public async Task<ActionResult> Index()
        {
            PageData pageData = await db.PageDatas.FirstOrDefaultAsync();
            if (pageData == null)
            {
                return HttpNotFound();
            }
            return View(pageData);
        }

        // POST: Admin/Footers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Time,Address,PhoneNumber,Email,lat,lng,Status,MetaKeywordDefault,MetaDescriptionDefault")] PageData pageData)
        {
            if (ModelState.IsValid)
            {
                TempData["Saved"] = 1;
                db.Entry(pageData).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();
                    await LogActivity.LogSavedActivity(pageData);
                }
                catch (Exception ex)
                {
                    TempData["Saved"] = 0;
                }                
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
