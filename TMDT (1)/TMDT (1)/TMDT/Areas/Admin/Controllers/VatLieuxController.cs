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
    public class VatLieuxController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private string pic;

        // GET: Admin/VatLieux
        public ActionResult Index()
        {
            return View(db.VatLieux.ToList());
        }

        // GET: Admin/VatLieux/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatLieu vatLieu = db.VatLieux.Find(id);
            if (vatLieu == null)
            {
                return HttpNotFound();
            }
            return View(vatLieu);
        }

        // GET: Admin/VatLieux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VatLieux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Content,Images,LoaiVatLieu")] VatLieu vatLieu)
        {
            if (ModelState.IsValid)
            {
                db.VatLieux.Add(vatLieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vatLieu);
        }

        // GET: Admin/VatLieux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatLieu vatLieu = db.VatLieux.Find(id);
            if (vatLieu == null)
            {
                return HttpNotFound();
            }
            return View(vatLieu);
        }

        // POST: Admin/VatLieux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Content,Images,LoaiVatLieu")] VatLieu vatLieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vatLieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vatLieu);
        }

        // GET: Admin/VatLieux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatLieu vatLieu = db.VatLieux.Find(id);
            if (vatLieu == null)
            {
                return HttpNotFound();
            }
            return View(vatLieu);
        }

        // POST: Admin/VatLieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VatLieu vatLieu = db.VatLieux.Find(id);
            db.VatLieux.Remove(vatLieu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> DeleteConfirmed(int? id, string category)
        {
            if (id.HasValue)
            {
                VatLieu vatLieu = await db.VatLieux.FindAsync(id.Value);
                db.VatLieux.Remove(vatLieu);
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(vatLieu));
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

            VatLieu user = db.VatLieux.Find(id);
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

            VatLieu vatLieu = db.VatLieux.Find(id);
            if (vatLieu == null)
            {
                return "Mã không tồn tại";
            }
            vatLieu.Images = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change PostID(" + id + ")-Image2: " + picture);
            return "";
        }

    }
}
