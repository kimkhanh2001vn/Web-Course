using Common;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TMDT.Models.CustomModel;
using TMDT.Models.DAO;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class AdvertisementsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/Advertisements
        public async Task<ActionResult> Index()
        {
            IQueryable<AdvertisementCustomAdmin> AdsListWithSectionName = from x in db.Advertisements
                                                                          join y in db.HomePageSections
                                                                          on x.SectionID equals y.ID
                                                                          where x.IsDefault != true
                                                                          orderby x.Status descending
                                                                         , x.EndDate descending
                                                                          select new AdvertisementCustomAdmin
                                                                          {
                                                                              ID = x.ID,
                                                                              Customer = x.Customer,
                                                                              PhoneNumber = x.PhoneNumber,
                                                                              Email = x.Email,
                                                                              ImageLink = x.ImageLink,
                                                                              RefererLink = x.RefererLink,
                                                                              ViewCount = x.ViewCount,
                                                                              ClickCount = x.ClickCount,
                                                                              CreatedDate = x.CreatedDate,
                                                                              EndDate = x.EndDate,
                                                                              Status = x.Status,
                                                                              Description = x.Description,
                                                                              IsDefault = x.IsDefault,
                                                                              SectionName = y.SectionName
                                                                          };
            return View(await AdsListWithSectionName.ToListAsync());
        }

        // Index for default ads
        public async Task<ActionResult> Default()
        {
            IQueryable<AdvertisementCustomAdmin> AdsListWithSectionName = from x in db.Advertisements
                                                                          join y in db.HomePageSections
                                                                          on x.SectionID equals y.ID
                                                                          where x.IsDefault == true
                                                                          orderby x.Status descending
                                                                         , x.EndDate descending
                                                                          select new AdvertisementCustomAdmin
                                                                          {
                                                                              ID = x.ID,
                                                                              Customer = x.Customer,
                                                                              PhoneNumber = x.PhoneNumber,
                                                                              Email = x.Email,
                                                                              ImageLink = x.ImageLink,
                                                                              RefererLink = x.RefererLink,
                                                                              ViewCount = x.ViewCount,
                                                                              ClickCount = x.ClickCount,
                                                                              CreatedDate = x.CreatedDate,
                                                                              EndDate = x.EndDate,
                                                                              Status = x.Status,
                                                                              Description = x.Description,
                                                                              IsDefault = x.IsDefault,
                                                                              SectionName = y.SectionName
                                                                          };
            return View(await AdsListWithSectionName.ToListAsync());
        }

        // GET: Admin/Advertisements/Create
        public ActionResult Create()
        {
            ViewBag.SectionID = new SelectList(db.HomePageSections, "ID", "SectionName");
            return View();
        }

        // POST: Admin/Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Customer,Phone,Email,ImageLink,RefererLink,CreatedDate,EndDate,Status,Description,SectionID,IsDefault")] Advertisement advertisement)
        {
            advertisement.IsDefault = false;
            TryValidateModel(advertisement);
            if (ModelState.IsValid)
            {
                db.Advertisements.Add(advertisement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SectionID = new SelectList(db.HomePageSections, "ID", "SectionName");
            return View(advertisement);
        }

        // GET: Admin/Advertisements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Advertisement advertisement = await db.Advertisements.Where(x=>x.IsDefault==false && x.ID ==id).FirstOrDefaultAsync();
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionID = new SelectList(db.HomePageSections, "ID", "SectionName");
            return View(advertisement);
        }

        // POST: Admin/Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Customer,Phone,Email,ImageLink,RefererLink,CreatedDate,EndDate,Status,Description,SectionID,IsDefault")] Advertisement advertisement)
        {
            if (db.Advertisements.Where(x => x.IsDefault == false && x.ID == advertisement.ID).Any())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(advertisement).State = EntityState.Modified;
                    db.Entry(advertisement).Property("ViewCount").IsModified = false;
                    db.Entry(advertisement).Property("ClickCount").IsModified = false;
                    db.Entry(advertisement).Property("IsDefault").IsModified = false;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.SectionID = new SelectList(db.HomePageSections, "ID", "SectionName");
                return View(advertisement);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Ambiguous);
            }
        }

        public async Task<ActionResult> EditDefault(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Advertisement advertisement = await db.Advertisements.Where(x=>x.IsDefault==true && x.ID == id).FirstOrDefaultAsync();
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionID = new SelectList(db.HomePageSections, "ID", "SectionName");
            return View(advertisement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDefault([Bind(Include = "ID,ImageLink,RefererLink")] Advertisement advertisement)
        {
            if (!db.Advertisements.Where(x => x.IsDefault == true && x.ID == advertisement.ID).Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Ambiguous);
            }

            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Advertisements.Attach(advertisement);
                db.Entry(advertisement).Property(x => x.ImageLink).IsModified = true;
                db.Entry(advertisement).Property(x => x.RefererLink).IsModified = true;
                await db.SaveChangesAsync();
                return RedirectToAction("Default");
            }

            ViewBag.SectionID = new SelectList(db.HomePageSections, "ID", "SectionName");
            return View(advertisement);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int? id, string category)
        {
            if (id.HasValue)
            {
                Advertisement ad = await db.Advertisements.FindAsync(id.Value);
                if (ad.IsDefault == true)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Ambiguous);
                    
                }

                db.Advertisements.Remove(ad);
                await db.SaveChangesAsync();
            }
            return Json(new { redirectUrl = Url.Action("Index", category), isRedirect = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeStatus(int id)
        {
            if (db.Advertisements.Where(x => x.IsDefault == false && x.ID == id).Any())
            {
                return Json(new
                {
                    status = new ChangesDAO().AdsStatus(id)
                });
            }
            return new HttpStatusCodeResult(HttpStatusCode.Ambiguous);
        }

        public string ChangeImage(int? id, string picture)
        {
            if (id == null)
            {
                return "Mã không tồn tại";
            }

            Advertisement ad = db.Advertisements.Find(id);
            if (ad == null)
            {
                return "Mã không tồn tại";
            }
            ad.ImageLink = picture;
            db.SaveChanges();
            return "";
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
