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

namespace TMDT.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/OrderDetails
        public async Task<ActionResult> Index()
        {
              var orderDetails = db.OrderDetails.Include(o => o.Order).Include(o => o.Post);
          //  var orderDetails = db.OrderDetails.Include(o => o.Color).Include(o => o.Order).Include(o => o.Product).Include(o => o.Size);

            //var orderDetails = from OrderDetails in db.Orders
            //    .Include(s => s.ID)
            //                   from Post in db.OrderDetails
            //    .Include(s => s.ProductID)
            //                   select OrderDetails;
            return View(await orderDetails.ToListAsync());
        }

        // GET: Admin/OrderDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName");
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,ProductID,Quantity,Price,ColorId,SizeId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,ProductID,Quantity,Price,ColorId,SizeId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = await db.OrderDetails.FindAsync(id);
            db.OrderDetails.Remove(orderDetail);
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
