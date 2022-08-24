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
    public class duangandaysController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private string pic;

        // GET: Admin/duangandays
        public ActionResult Index()
        {
            return View(db.duangandays.ToList());
        }

        // GET: Admin/duangandays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            duanganday duanganday = db.duangandays.Find(id);
            if (duanganday == null)
            {
                return HttpNotFound();
            }
            return View(duanganday);
        }

        // GET: Admin/duangandays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/duangandays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Title,Name,Images,Major")] duanganday duanganday)
        {
            if (ModelState.IsValid)
            {
                db.duangandays.Add(duanganday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(duanganday);
        }

        // GET: Admin/duangandays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            duanganday duanganday = db.duangandays.Find(id);
            if (duanganday == null)
            {
                return HttpNotFound();
            }
            return View(duanganday);
        }

        // POST: Admin/duangandays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Title,Name,Images,Major")] duanganday duanganday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(duanganday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(duanganday);
        }

        // GET: Admin/duangandays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            duanganday duanganday = db.duangandays.Find(id);
            if (duanganday == null)
            {
                return HttpNotFound();
            }
            return View(duanganday);
        }

        // POST: Admin/duangandays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            duanganday duanganday = db.duangandays.Find(id);
            db.duangandays.Remove(duanganday);
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

            duanganday user = db.duangandays.Find(id);
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

            duanganday duanganday = db.duangandays.Find(id);
            if (duanganday == null)
            {
                return "Mã không tồn tại";
            }
            duanganday.Images = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change PostID(" + id + ")-Image2: " + picture);
            return "";
        }

    }
}
