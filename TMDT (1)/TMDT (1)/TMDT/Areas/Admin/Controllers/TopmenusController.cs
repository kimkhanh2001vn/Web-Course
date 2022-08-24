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
    public class TopmenusController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/Topmenus
        public ActionResult Index()
        {
            return View(db.Topmenus.ToList());
        }

        // GET: Admin/Topmenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topmenu topmenu = db.Topmenus.Find(id);
            if (topmenu == null)
            {
                return HttpNotFound();
            }
            return View(topmenu);
        }

        // GET: Admin/Topmenus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Topmenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tenduong,diachi,Email,giolamviec,hotline")] Topmenu topmenu)
        {
            if (ModelState.IsValid)
            {
                db.Topmenus.Add(topmenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topmenu);
        }

        // GET: Admin/Topmenus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topmenu topmenu = db.Topmenus.Find(id);
            if (topmenu == null)
            {
                return HttpNotFound();
            }
            return View(topmenu);
        }

        // POST: Admin/Topmenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tenduong,diachi,Email,giolamviec,hotline")] Topmenu topmenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topmenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topmenu);
        }

        // GET: Admin/Topmenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topmenu topmenu = db.Topmenus.Find(id);
            if (topmenu == null)
            {
                return HttpNotFound();
            }
            return View(topmenu);
        }

        // POST: Admin/Topmenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topmenu topmenu = db.Topmenus.Find(id);
            db.Topmenus.Remove(topmenu);
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
