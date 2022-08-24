using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMDT;
using Common;

namespace TMDT.Areas.Admin.Controllers
{
    public class ShopManagementsController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private static string pic;
        // GET: Admin/ShopManagements
        public async Task<ActionResult> Index()
        {
            var shopManagements = db.ShopManagements.Include(s => s.Shop);
            return View(await shopManagements.ToListAsync());
        }

        // GET: Admin/ShopManagements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopManagement shopManagement = await db.ShopManagements.FindAsync(id);
            if (shopManagement == null)
            {
                return HttpNotFound();
            }
            return View(shopManagement);
        }

        // GET: Admin/ShopManagements/Create
        public ActionResult Create()
        {
            ViewBag.StoreID = new SelectList(db.Shops, "StoreID", "ShopName");
            return View();
        }

        // POST: Admin/ShopManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,StoreID,Description,Image,Url,DisplayOrder,Status,Content,UserId,UserTime,Payment,Money")] ShopManagement shopManagement)
        {
            shopManagement.UserId = shopManagement.UserId;
            shopManagement.Url = shopManagement.Url;
            shopManagement.Description = HttpUtility.HtmlDecode(shopManagement.Description);
            shopManagement.Content = HttpUtility.HtmlDecode(shopManagement.Content);
          //  shopManagement.UserTime = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                db.ShopManagements.Add(shopManagement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StoreID = new SelectList(db.Shops, "StoreID", "ShopName", shopManagement.StoreID);
            return View(shopManagement);
        }

        // GET: Admin/ShopManagements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            ShopManagement shopManagement = await db.ShopManagements.FindAsync(id);
           
            pic = shopManagement.Image;
            if (shopManagement == null)
            {
                return HttpNotFound();
            }
         
            ViewBag.StoreID = new SelectList(db.Shops, "StoreID", "ShopName", shopManagement.StoreID);
            return View(shopManagement);
        }

        // POST: Admin/ShopManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,StoreID,Description,Image,Url,DisplayOrder,Status,Content,UserId,UserTime,Payment,Money")] ShopManagement shopManagement)
        {
            shopManagement.UserId = shopManagement.UserId;
            shopManagement.Url = shopManagement.Url;
            shopManagement.Description = HttpUtility.HtmlDecode(shopManagement.Description);
            shopManagement.Content = HttpUtility.HtmlDecode(shopManagement.Content);
            if (pic != shopManagement.Image)
            {
                shopManagement.Image = pic;
            }
            if (ModelState.IsValid)
            {
                db.Entry(shopManagement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index/" + shopManagement.ID);

            }
            ViewBag.StoreID = new SelectList(db.Shops, "StoreID", "ShopName", shopManagement.StoreID);
            return View(shopManagement);
        }
        public string ChangeImage(int? id, string picture)
        {
            pic = picture;
            if (id == null)
            {
                return "Mã không tồn tại";
            }
            ShopManagement user = db.ShopManagements.Find(id);
            if (user == null)
            {
                return "Mã không tồn tại";
            }
            user.Image = picture;
            db.SaveChanges();
            return "";
        }

        // GET: Admin/ShopManagements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShopManagement shopManagement = await db.ShopManagements.FindAsync(id);
            if (shopManagement == null)
            {
                return HttpNotFound();
            }
            return View(shopManagement);
        }

        // POST: Admin/ShopManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShopManagement shopManagement = await db.ShopManagements.FindAsync(id);
            db.ShopManagements.Remove(shopManagement);
            await db.SaveChangesAsync();
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
