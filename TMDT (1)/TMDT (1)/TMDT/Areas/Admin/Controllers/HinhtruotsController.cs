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
    public class HinhtruotsController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private string pic;

        // GET: Admin/Hinhtruots
        public ActionResult Index()
        {
            return View(db.Hinhtruots.ToList());
        }

        // GET: Admin/Hinhtruots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hinhtruot hinhtruot = db.Hinhtruots.Find(id);
            if (hinhtruot == null)
            {
                return HttpNotFound();
            }
            return View(hinhtruot);
        }

        // GET: Admin/Hinhtruots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hinhtruots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Content,Images")] Hinhtruot hinhtruot)
        {
            if (ModelState.IsValid)
            {
                db.Hinhtruots.Add(hinhtruot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hinhtruot);
        }

        // GET: Admin/Hinhtruots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hinhtruot hinhtruot = db.Hinhtruots.Find(id);
            if (hinhtruot == null)
            {
                return HttpNotFound();
            }
            return View(hinhtruot);
        }

        // POST: Admin/Hinhtruots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Content,Images")] Hinhtruot hinhtruot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhtruot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hinhtruot);
        }

        // GET: Admin/Hinhtruots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hinhtruot hinhtruot = db.Hinhtruots.Find(id);
            if (hinhtruot == null)
            {
                return HttpNotFound();
            }
            return View(hinhtruot);
        }

        // POST: Admin/Hinhtruots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hinhtruot hinhtruot = db.Hinhtruots.Find(id);
            db.Hinhtruots.Remove(hinhtruot);
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

            Hinhtruot user = db.Hinhtruots.Find(id);
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

            Hinhtruot Hinhtruot = db.Hinhtruots.Find(id);
            if (Hinhtruot == null)
            {
                return "Mã không tồn tại";
            }
            Hinhtruot.Images = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change PostID(" + id + ")-Image2: " + picture);
            return "";
        }
    }
}
