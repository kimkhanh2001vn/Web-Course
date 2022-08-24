using Common;
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
    public class TinTucNewsController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private string pic;

        // GET: Admin/TinTucNews
        public ActionResult Index()
        {
            return View(db.TinTucNews.ToList());
        }

        // GET: Admin/TinTucNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucNew tinTucNew = db.TinTucNews.Find(id);
            if (tinTucNew == null)
            {
                return HttpNotFound();
            }
            return View(tinTucNew);
        }

        // GET: Admin/TinTucNews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TinTucNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Images,Title,Content")] TinTucNew tinTucNew)
        {
            if (ModelState.IsValid)
            {
                db.TinTucNews.Add(tinTucNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tinTucNew);
        }

        // GET: Admin/TinTucNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucNew tinTucNew = db.TinTucNews.Find(id);
            if (tinTucNew == null)
            {
                return HttpNotFound();
            }
            return View(tinTucNew);
        }

        // POST: Admin/TinTucNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Images,Title,Content")] TinTucNew tinTucNew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTucNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tinTucNew);
        }

        // GET: Admin/TinTucNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTucNew tinTucNew = db.TinTucNews.Find(id);
            if (tinTucNew == null)
            {
                return HttpNotFound();
            }
            return View(tinTucNew);
        }

        // POST: Admin/TinTucNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTucNew tinTucNew = db.TinTucNews.Find(id);
            db.TinTucNews.Remove(tinTucNew);
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
        public async Task<string> ChangeImage(int? id, string picture)
        {
            pic = picture;
            if (id == null)
            {
                return "Mã không tồn tại";
            }

            TinTucNew user = db.TinTucNews.Find(id);
            if (user == null)
            {
                return "Mã không tồn tại";
            }
            user.Images = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change PostID(" + id + ")-Image: " + picture);
            return "";
        }
        public async Task<string> ChangeImage2(int? id, string picture)
        {
            pic = picture;
            if (id == null)
            {
                return "Mã không tồn tại";
            }

            TinTucNew tinTucNew = db.TinTucNews.Find(id);
            if (tinTucNew == null)
            {
                return "Mã không tồn tại";
            }
            tinTucNew.Images = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change PostID(" + id + ")-Image2: " + picture);
            return "";
        }


    }
}
