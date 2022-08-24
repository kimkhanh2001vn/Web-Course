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
    public class ChapterDetailsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/ChapterDetails
        public ActionResult Index()
        {
            return View(db.ChapterDetails.ToList());
        }

        // GET: Admin/ChapterDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChapterDetail chapterDetail = db.ChapterDetails.Find(id);
            if (chapterDetail == null)
            {
                return HttpNotFound();
            }
            return View(chapterDetail);
        }

        // GET: Admin/ChapterDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChapterDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChapterDetailsID,ChapterID,ChapterDetailsName")] ChapterDetail chapterDetail)
        {
            if (ModelState.IsValid)
            {
                db.ChapterDetails.Add(chapterDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chapterDetail);
        }

        // GET: Admin/ChapterDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChapterDetail chapterDetail = db.ChapterDetails.Find(id);
            if (chapterDetail == null)
            {
                return HttpNotFound();
            }
            return View(chapterDetail);
        }

        // POST: Admin/ChapterDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChapterDetailsID,ChapterID,ChapterDetailsName")] ChapterDetail chapterDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapterDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chapterDetail);
        }

        // GET: Admin/ChapterDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChapterDetail chapterDetail = db.ChapterDetails.Find(id);
            if (chapterDetail == null)
            {
                return HttpNotFound();
            }
            return View(chapterDetail);
        }

        // POST: Admin/ChapterDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChapterDetail chapterDetail = db.ChapterDetails.Find(id);
            db.ChapterDetails.Remove(chapterDetail);
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
