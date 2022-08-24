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
using TMDT.Models.CustomModel;
using TMDT.Models.DAO;


namespace TMDT.Areas.Admin.Controllers
{
    public class ChaptersController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/Chapters
        public ActionResult Index()
        {
            return View(db.Chapters.ToList());
        }

        // GET: Admin/Chapters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // GET: Admin/Chapters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Chapters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChapterID,CourseID,ChapterName,id")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Chapters.Add(chapter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chapter);
        }

        // GET: Admin/Chapters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: Admin/Chapters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChapterID,CourseID,ChapterName,id")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chapter);
        }

        // GET: Admin/Chapters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: Admin/Chapters/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int? id , string categỏry)
        { 
        //    Chapter chapter = db.Chapters.Find(id);
        //    db.Chapters.Remove(chapter);
        //    db.SaveChanges();

            if (id.HasValue)
            {
                 Chapter chapter = await db.Chapters.FindAsync(id.Value);
                 db.Chapters.Remove(chapter);
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(chapter));
            }
            return Json(new { redirectUrl = Url.Action("Index", categỏry), isRedirect = true }, JsonRequestBehavior.AllowGet);
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
