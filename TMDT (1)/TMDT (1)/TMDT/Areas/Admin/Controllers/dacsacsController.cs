using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMDT;

namespace TMDT.Areas.Admin.Controllers
{
    public class dacsacsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/dacsacs
        public ActionResult Index()
        {
            return View(db.dacsacs.ToList());
        }

        // GET: Admin/dacsacs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dacsac dacsac = db.dacsacs.Find(id);
            if (dacsac == null)
            {
                return HttpNotFound();
            }
            return View(dacsac);
        }

        // GET: Admin/dacsacs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/dacsacs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,content,Name")] dacsac dacsac)
        {
            if (ModelState.IsValid)
            {
                db.dacsacs.Add(dacsac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dacsac);
        }

        // GET: Admin/dacsacs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dacsac dacsac = db.dacsacs.Find(id);
            if (dacsac == null)
            {
                return HttpNotFound();
            }
            return View(dacsac);
        }

        // POST: Admin/dacsacs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,content,Name")] dacsac dacsac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dacsac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dacsac);
        }

        // GET: Admin/dacsacs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dacsac dacsac = db.dacsacs.Find(id);
            if (dacsac == null)
            {
                return HttpNotFound();
            }
            return View(dacsac);
        }

        // POST: Admin/dacsacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dacsac dacsac = db.dacsacs.Find(id);
            db.dacsacs.Remove(dacsac);
            db.SaveChanges();
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
