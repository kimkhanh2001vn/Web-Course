using Common;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class GoogleAPIsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/GoogleAPIs/Edit/5
        public async Task<ActionResult> Edit(int? id = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GoogleAPI googleAPI = await db.GoogleAPIs.FindAsync(id);
            if (googleAPI == null)
            {
                return HttpNotFound();
            }
            return View(googleAPI);
        }

        // POST: Admin/GoogleAPIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,GoogleScript,GoogleJson,ViewID")] GoogleAPI googleAPI)
        {
            googleAPI.GoogleScript = HttpUtility.HtmlDecode(googleAPI.GoogleScript);
            if (ModelState.IsValid)
            {
                db.Entry(googleAPI).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(googleAPI);
                return RedirectToAction("Edit");
            }
            return View(googleAPI);
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
