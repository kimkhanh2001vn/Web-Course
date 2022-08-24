using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TMDT.Models.CustomModel;
using TMDT.Models.DAO;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class PostsController : Controller
    {
        private TapChiDB db = new TapChiDB();
        private PostCategoriesDAO PostCatDAO = new PostCategoriesDAO();
        private static string pic;

        // GET: Admin/Posts
        public async Task<ActionResult> Index()
        {
           // ViewBag.PostCatNames = PostCatDAO.getPostCatNames();
           // IQueryable<Post> posts = db.Posts;
            return View(db.Posts.ToList());
        }

        // GET: Admin/Posts/Details/5
        public async Task<ActionResult> Details(int? id)
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
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.ID = ProductSelectListItem();
           // ViewBag.MagazineCategory = new SelectList(PostCatDAO.ProductCategories(), "Value", "Text", "");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Alias,CategoryID,Image,NewCost,FreeCourseStatus,ViewCount,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaTitle,MetaKeyword,MetaDescription,Status,Tags,ActiveDate,OldCost")] Post post,string[] postTags)
        {            
            //post.Tags = string.Join(",", postTags.Select(x => x.Trim()).ToArray()); //Trim each postTags then join them with seperator ","
            //post.Tags = post.Tags.Replace(",,", ",");//Delete the joined empty value
            if (ModelState.IsValid)
            {                
                post.Alias = StringHelper.ToAliasWithRandomNumber(post.Name);
                while (db.Posts.Where(x => x.Alias == post.Alias).Any())
                {
                    post.Alias = StringHelper.ToAliasWithRandomNumber(post.Name); //If exists that alias then do it again to get another random suffix number 
                }
                post.UpdatedDate = DateTime.Now;

                post.CreatedDate = DateTime.Now;

                db.Posts.Add(post);
                await db.SaveChangesAsync();
               // await LogActivity.LogSavedActivity(post);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = ProductSelectListItem();
            // ViewBag.MagazineCategory = new SelectList(PostCatDAO.CreateMagazineCategorySelectList(post.MagazineCategory), "Value", "Text");

            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = await db.Posts.FindAsync(id);
            pic = post.Image;
            if (post == null)
            {
                return HttpNotFound();
            }
            if(!string.IsNullOrEmpty(post.Tags))
            ViewBag.PostTags = post.Tags.Split(',');
            ViewBag.CategoryID = ProductSelectListItem();

            // ViewBag.MagazineCategory = new SelectList(PostCatDAO.CreateMagazineCategorySelectList(post.MagazineCategory), "Value", "Text", post.MagazineCategory);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Alias,CategoryID,Image,NewCost,FreeCourseStatus,ViewCount,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,MetaTitle,MetaKeyword,MetaDescription,Status,Tags,ActiveDate,OldCost")] Post post, string[] postTags)
        {
            //post.Tags = string.Join(",", postTags.Select(x => x.Trim()).ToArray()); //Trim each postTags then join them with seperator ","
            //post.Tags = post.Tags.Replace(",,", ",");//Remove the joined empty value
            if (ModelState.IsValid)
            {           
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(post).State = EntityState.Modified;
                Post oldPost = db.Posts.AsNoTracking().Where(x => x.ID == post.ID).FirstOrDefault();
                //If post name changed then change alias
                if (oldPost.Name != post.Name) 
                {
                    post.Alias = StringHelper.ToAliasWithRandomNumber(post.Name);
                    while (db.Posts.Where(x => x.Alias == post.Alias).Any())
                    {
                        post.Alias = StringHelper.ToAliasWithRandomNumber(post.Name); 
                    }
                }
                else
                {
                    db.Entry(post).Property("Alias").IsModified = false;
                }
                 post.CreatedDate = DateTime.Now;
                 post.UpdatedDate = DateTime.Now;
                //post.UpdatedBy = ((UserInfo)Session["UserInfo"]).FullName;

                await db.SaveChangesAsync();
              
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = ProductSelectListItem();
         //   ViewBag.MagazineCategory = new SelectList(PostCatDAO.CreateMagazineCategorySelectList(post.MagazineCategory), "Value", "Text", post.MagazineCategory);
            return View(post);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int? id, string category)
        {
            if (id.HasValue)
            {
                Post posts = await db.Posts.FindAsync(id.Value);
                db.Posts.Remove(posts);
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(posts));
            }

            return Json(new { redirectUrl = Url.Action("Index", category), isRedirect = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(int id)
        {
            return Json(new
            {
                status = new ChangesDAO().PostsStatus(id)
            });
        }
        public JsonResult ChangeHomeFlag(int id)
        {
            bool result = new ChangesDAO().PostsHomeFlag(id);
            return Json(new
            {
                homeflag = result
            });
        }

        public async Task<string> ChangeImage(int? id, string picture)
        {
            pic = picture;
            if (id == null)
            {
                return "Mã không tồn tại";
            }

            Post user = db.Posts.Find(id);
            if (user == null)
            {
                return "Mã không tồn tại";
            }
            user.Image = picture;
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

            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return "Mã không tồn tại";
            }
            post.Image = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change PostID(" + id + ")-Image2: " + picture);
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


        private List<ProductCategories> ProductSelectListItem()
        {
            return PostCatDAO.getProduct().Select(x => new ProductCategories
            {
                ID = x.ID,
                Name = x.Name
       
            }).ToList();
        }
        private List<PostCategorySelectListItem> PostCategoryCustomSelectList(int selectedCategoryID)
        {
            return PostCatDAO.getOrderedCatgories().Select(x => new PostCategorySelectListItem
            {
                ID = x.ID,
                Name = x.Name,
               
                //Selected = (selectedCategoryID == x.ID)
            }).ToList();
        }
    }
}
