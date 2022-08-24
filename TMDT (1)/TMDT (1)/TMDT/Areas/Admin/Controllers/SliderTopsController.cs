using Common;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TMDT.Models.DAO;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class SliderTopsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/AdTopBanners
        public async Task<ActionResult> Index()
        {
            return View(await db.SliderTops.ToListAsync());
        }

        // GET: Admin/AdTopBanners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdTopBanners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Image,Link,Status")] SliderTop sliderTop)
        {
            if (ModelState.IsValid)
            {
                db.SliderTops.Add(sliderTop);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sliderTop);
        }

        // GET: Admin/AdTopBanners/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SliderTop sliderTop = await db.SliderTops.FindAsync(id);
            if (sliderTop == null)
            {
                return HttpNotFound();
            }
            return View(sliderTop);
        }

        // POST: Admin/AdTopBanners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Image,Link,Status")] SliderTop sliderTop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sliderTop).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sliderTop);
        }

        // GET: Admin/AdTopBanners/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SliderTop sliderTop = await db.SliderTops.FindAsync(id);
            if (sliderTop == null)
            {
                return HttpNotFound();
            }
            return View(sliderTop);
        }

        // POST: Admin/AdTopBanners/Delete/5

        public async Task<ActionResult> DeleteConfirmed(int? id, string category)
        {
            if (id.HasValue)
            {
                SliderTop sliderTop = await db.SliderTops.FindAsync(id);
                db.SliderTops.Remove(sliderTop);
                await db.SaveChangesAsync();
            }
            return Json(new { redirectUrl = Url.Action("Index", category), isRedirect = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(int id)
        {
            return Json(new
            {
                status = new ChangesDAO().SlideStatus(id)
            });
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
