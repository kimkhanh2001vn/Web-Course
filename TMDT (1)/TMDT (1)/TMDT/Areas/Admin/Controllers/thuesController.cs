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
    public class thuesController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private string pic;

        // GET: Admin/thues
        public ActionResult Index()
        {
            return View(db.thues.ToList());
        }

        // GET: Admin/thues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thue thue = db.thues.Find(id);
            if (thue == null)
            {
                return HttpNotFound();
            }
            return View(thue);
        }

        // GET: Admin/thues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/thues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Content,Image,LoaiThue")] thue thue)
        {
            if (ModelState.IsValid)
            {
                db.thues.Add(thue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thue);
        }

        // GET: Admin/thues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thue thue = db.thues.Find(id);
            if (thue == null)
            {
                return HttpNotFound();
            }
            return View(thue);
        }

        // POST: Admin/thues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Content,Image,LoaiThue")] thue thue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thue);
        }

        // GET: Admin/thues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thue thue = db.thues.Find(id);
            if (thue == null)
            {
                return HttpNotFound();
            }
            return View(thue);
        }

        // POST: Admin/thues/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int? id, string category)
        {
            if (id.HasValue)
            {
                thue thue = await db.thues.FindAsync(id.Value);
                db.thues.Remove(thue);
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(thue));
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

        public async Task<string> ChangeImage(int? id, string picture)
        {
            pic = picture;
            if (id == null)
            {
                return "Mã không tồn tại";
            }

            thue user = db.thues.Find(id);
            if (user == null)
            {
                return "Mã không tồn tại";
            }
            user.Image = picture;
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

            thue Thue = db.thues.Find(id);
            if (Thue == null)
            {
                return "Mã không tồn tại";
            }
            Thue.Image = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change PostID(" + id + ")-Image2: " + picture);
            return "";
        }

    }
}
