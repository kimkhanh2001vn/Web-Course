using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TMDT;

namespace TMDT.Areas.Admin.Controllers
{
    public class taisaochontoisController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/taisaochontois
        public ActionResult Index()
        {
            return View(db.taisaochontois.ToList());
        }

        // GET: Admin/taisaochontois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taisaochontoi taisaochontoi = db.taisaochontois.Find(id);
            if (taisaochontoi == null)
            {
                return HttpNotFound();
            }
            return View(taisaochontoi);
        }

        // GET: Admin/taisaochontois/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/taisaochontois/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Title,Name,Introduce")] taisaochontoi taisaochontoi)
        {
            if (ModelState.IsValid)
            {
                db.taisaochontois.Add(taisaochontoi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taisaochontoi);
        }

        // GET: Admin/taisaochontois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taisaochontoi taisaochontoi = db.taisaochontois.Find(id);
            if (taisaochontoi == null)
            {
                return HttpNotFound();
            }
            return View(taisaochontoi);
        }

        // POST: Admin/taisaochontois/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Title,Name,Introduce")] taisaochontoi taisaochontoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taisaochontoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taisaochontoi);
        }

        // GET: Admin/taisaochontois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taisaochontoi taisaochontoi = db.taisaochontois.Find(id);
            if (taisaochontoi == null)
            {
                return HttpNotFound();
            }
            return View(taisaochontoi);
        }

        // POST: Admin/taisaochontois/Delete/5
        [HttpPost]
        
        public async Task<ActionResult> DeleteConfirmed(int? id, string category)
        {
            if (id.HasValue)
            {
                taisaochontoi taisaochontoi = await db.taisaochontois.FindAsync(id.Value);
                db.taisaochontois.Remove(taisaochontoi);
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(taisaochontoi));
            }

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
