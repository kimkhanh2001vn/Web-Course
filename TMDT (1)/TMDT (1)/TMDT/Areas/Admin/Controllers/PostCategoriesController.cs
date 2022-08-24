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
using TMDT.Models.CustomModel;
using TMDT.Models.DAO;

namespace TMDT.Areas.Admin.Controllers
{
    public class PostCategoriesController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/PostCategories
        public ActionResult Index()
        {

      
            return View(db.PostCategories.ToList());
        }

        // GET: Admin/PostCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postCategory = db.PostCategories.Find(id);
            if (postCategory == null)
            {
                return HttpNotFound();
            }
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Create
        public ActionResult Create()
        {
       
            return View();
        }

        // POST: Admin/PostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StoreID,Name,Alias,Description,ParentID,DisplayOrder,Status,Image,FreeCourseStatus,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,MetaTitle")] PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                postCategory.Alias = StringHelper.ToAliasWithRandomNumber(postCategory.Name);
                while (db.PostCategories.Where(x => x.Alias == postCategory.Alias).Any())
                {
                    postCategory.Alias = StringHelper.ToAliasWithRandomNumber(postCategory.Name); //If exists that alias then do it again to get another random suffix number 
                }

                db.PostCategories.Add(postCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postCategory);
        }

        // GET: Admin/PostCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postCategory = db.PostCategories.Find(id);
            if (postCategory == null)
            {
                return HttpNotFound();
            }
            return View(postCategory);
        }

        // POST: Admin/PostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StoreID,Name,Alias,Description,ParentID,DisplayOrder,Status,FontSize,FontFamily,Image,HomeFlag,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,FontAwesomeClass")] PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postCategory = db.PostCategories.Find(id);
            if (postCategory == null)
            {
                return HttpNotFound();
            }
            return View(postCategory);
        }

        // POST: Admin/PostCategories/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int? id, string category)
        {
            if (id.HasValue)
            {

                PostCategory postCategory = db.PostCategories.Find(id.Value);
                db.PostCategories.Remove(postCategory);
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(postCategory));
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
        public JsonResult ChangeStatus(int id)
        {
            bool result = new ChangesDAO().PostsCateStatus(id);
            return Json(new
            {
                status = result
            }) ;
        }
        public JsonResult ChangeHomeFlag(int id)
        {
            bool result = new ChangesDAO().PostsCateHomeFlag(id);
            return Json(new
            {
                homeflag = result
            }) ;

        }
        
 
    }
}
