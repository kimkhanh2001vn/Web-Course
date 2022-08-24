using Common;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class HomePageSectionsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/HomePageSections
        public async Task<ActionResult> Index()
        {
            ViewData["item.CategoryID"] = new SelectList(await db.PostCategories.Where(x => x.ParentID == 0 && x.Status == true).Select(x => new { x.Name, x.ID }).ToListAsync(), "ID", "Name");
            return View(await db.HomePageSections.Where(x=>x.isModable == true).ToListAsync());
        }

        // POST: Admin/HomePageSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HomePageSection[] homePageSection)
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            for (int i = 0; i < homePageSection.Length; i++)
            {
                db.HomePageSections.Attach(homePageSection[i]);
                db.Entry(homePageSection[i]).Property("CategoryID").IsModified = true;
            }
            await db.SaveChangesAsync();
            TempData["SuccessHP"] = "";
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
