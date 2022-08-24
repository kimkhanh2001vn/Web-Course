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
using TMDT.Models.DAO;
using Newtonsoft.Json;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class VideosController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/Videos
        public async Task<ActionResult> Index()
        {
            return View(await db.Videos.ToListAsync());
        }

        // GET: Admin/Videos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await db.Videos.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: Admin/Videos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Alias,Description,VideoURL,ImageURL,Category,Status,CreatedDate,Duration,Color,ViewCount,Description,SourceName,SourceURL")] Video video)
        {
            if (ModelState.IsValid)
            {
                video.Alias = StringHelper.ToAlias(video.Title);
                while (db.Videos.Where(x=>x.Alias == video.Alias).Any())
                { 
                    video.Alias = StringHelper.ToAlias(video.Title);
                }
                video.CreatedDate = DateTime.Now;                
                db.Videos.Add(video);
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(video);
                return RedirectToAction("Index");
            }

            return View(video);
        }

        // GET: Admin/Videos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await db.Videos.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Admin/Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Alias,Description,VideoURL,ImageURL,Category,Status,CreatedDate,Duration,Color,ViewCount,Description,SourceName,SourceURL")] Video video)
        {
            db.Entry(video).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                Video oldVideo = await db.Videos.Where(x => x.ID == video.ID).FirstOrDefaultAsync();
                if(oldVideo.Title != video.Title)
                {
                    video.Alias = StringHelper.ToAlias(video.Title);
                    while (db.Videos.Where(x => x.Alias == video.Alias).Any())
                    {
                        video.Alias = StringHelper.ToAlias(video.Title);
                    }
                }
                else
                {
                    db.Entry(video).Property(x => x.Alias).IsModified = false;
                }
                db.Entry(video).Property(x => x.CreatedDate).IsModified = false;
                await db.SaveChangesAsync();
                await LogActivity.LogSavedActivity(video);
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // POST: Admin/Videos/Delete/5
        [HttpPost]       
        public async Task<ActionResult> DeleteConfirmed(int id,string category)
        {
            Video video = await db.Videos.FindAsync(id);
            db.Videos.Remove(video);
            await db.SaveChangesAsync();
            await LogActivity.LogSavedActivity("Delete " + JsonConvert.SerializeObject(video));
            return Json(new { redirectUrl = Url.Action("Index", category), isRedirect = true }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ChangeStatus(int id)
        {
            bool result = new ChangesDAO().VideosStatus(id);
            await LogActivity.LogSavedActivity("Change VideoID(" + id + ")-Status(" + result + ")");
            return Json(new
            {
                status = result
            });
        }
        public async Task<string> ChangeImage(int? id, string picture)
        {            
            if (id == null)
            {
                return "Mã không tồn tại";
            }

            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return "Mã không tồn tại";
            }
            video.ImageURL = picture;
            db.SaveChanges();
            await LogActivity.LogSavedActivity("Change VideoID(" + id + ")-Image: " + picture);
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
