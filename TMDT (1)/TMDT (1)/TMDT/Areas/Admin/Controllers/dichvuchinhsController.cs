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
    public class dichvuchinhsController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private string pic;

        // GET: Admin/dichvuchinhs
        public ActionResult Index()
        {
            return View(db.dichvuchinhs.ToList());
        }

        // GET: Admin/dichvuchinhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dichvuchinh dichvuchinh = db.dichvuchinhs.Find(id);
            if (dichvuchinh == null)
            {
                return HttpNotFound();
            }
            return View(dichvuchinh);
        }

        // GET: Admin/dichvuchinhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/dichvuchinhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Content,Images")] dichvuchinh dichvuchinh)
        {
            if (ModelState.IsValid)
            {
                db.dichvuchinhs.Add(dichvuchinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dichvuchinh);
        }

        // GET: Admin/dichvuchinhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dichvuchinh dichvuchinh = db.dichvuchinhs.Find(id);
            if (dichvuchinh == null)
            {
                return HttpNotFound();
            }
            return View(dichvuchinh);
        }

        // POST: Admin/dichvuchinhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Content,Images")] dichvuchinh dichvuchinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dichvuchinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dichvuchinh);
        }

        // GET: Admin/dichvuchinhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dichvuchinh dichvuchinh = db.dichvuchinhs.Find(id);
            if (dichvuchinh == null)
            {
                return HttpNotFound();
            }
            return View(dichvuchinh);
        }

        // POST: Admin/dichvuchinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dichvuchinh dichvuchinh = db.dichvuchinhs.Find(id);
            db.dichvuchinhs.Remove(dichvuchinh);
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

            dichvuchinh user = db.dichvuchinhs.Find(id);
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

            dichvuchinh dichvuchinh = db.dichvuchinhs.Find(id);
            if (dichvuchinh == null)
            {
                return "Mã không tồn tại";
            }
            dichvuchinh.Images = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change PostID(" + id + ")-Image2: " + picture);
            return "";
        }

    }
}
