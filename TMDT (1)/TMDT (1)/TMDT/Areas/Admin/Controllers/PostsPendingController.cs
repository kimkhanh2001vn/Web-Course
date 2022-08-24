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
using TMDT.Models.CustomModel;
using Common;
using TMDT.Models.DAO;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class PostsPendingController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private PostCategoriesDAO PostCatDAO = new PostCategoriesDAO();
        
        // GET: Admin/PostsPending
        public async Task<ActionResult> Index()
        {
            int currentUserId = ((UserInfo)Session["UserInfo"]).UserId;
            var posts = db.Posts.Include(p => p.PostCategory);
            ViewBag.PostCatNames = PostCatDAO.getPostCatNames();
            return View(await posts.ToListAsync());
        }       
        // GET: Admin/PostsPending/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(PostCatDAO.getOrderedCatgories(), "ID", "Name");
            return View();
        }

        // POST: Admin/PostsPending/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,CategoryID,Image,Description,Content,CreatedDate,CreatedBy,MetaKeyword,MetaDescription,Status,Author,MinRead,Image2")] Post post)
        {
            post.Alias = StringHelper.ToAlias(post.Name);
            
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.PostCategories, "ID", "Name", post.CategoryID);
            return View(post);
        }

        // GET: Admin/PostsPending/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.PostCategories, "ID", "Name", post.CategoryID);
            return View(post);
        }

        // POST: Admin/PostsPending/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Alias,CategoryID,Image,Description,Content,HomeFlag,HotFlag,ViewCount,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status,BackgroundImage,Color,BackgroundColor,Author,MagazineCategory,MinRead,Image2")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.PostCategories, "ID", "Name", post.CategoryID);
            return View(post);
        }
        
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id, string category)
        {
            CheckProcessingPost(id);            
            Post post = await db.Posts.FindAsync(id);            
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
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

        private bool CheckProcessingPost(int PostID)
        {
            int currentUserId = ((UserInfo)Session["UserInfo"]).UserId;

            //Check whether this post:
            //  is currently pending (status = false)
            //  is created by the same editing user
            if(db.Posts.Where(x=> x.Status == false && x.ID == PostID).Any())
            {
                return true;
            }
            throw new System.InvalidOperationException();
        }
    }
}
