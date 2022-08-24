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
    [AuthorizeBusiness]
    public class SelectivePostsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/HotPosts
      
        // POST: Admin/HotPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save([Bind(Include = "ID,PostID")] SelectivePost[] savePosts,string currentLocation)
        {
            if (ModelState.IsValid)
            {
                TempData["Saved"] = 1;
                try
                {
                    foreach (SelectivePost savePost in savePosts)
                    {
                        db.SelectivePosts.Attach(savePost);
                        db.Entry(savePost).Property(x => x.PostID).IsModified = true;                       
                    }     
                    await db.SaveChangesAsync();
                    await LogActivity.LogSavedActivity(savePosts);
                }
                catch(Exception ex)
                {
                    TempData["Saved"] = 0;
                }
                
                return RedirectToAction(currentLocation);
            }
            TempData["Saved"] = 0;
            return RedirectToAction(currentLocation);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchResultAjax(string searchString)
        {
            string searchAlias = StringHelper.ToAlias(searchString);
            return View("SearchResultAjax", db.Posts.Where(x => x.Status == true && ( x.Name.Contains(searchString) || x.Alias.Contains(searchAlias) ) && x.PostCategory.Status == true).Include(x=>x.PostCategory).ToList());
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
