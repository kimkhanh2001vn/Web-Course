using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace TMDT.Models.DAO
{
    public class PostDAO
    {
        private TapChiDB db = new TapChiDB();

        public IEnumerable<Post> getContentPost(int id)
        {
            List<Post> query = (from a in db.PostCategories
                                join b in db.Posts on a.ID equals b.CategoryID
                                where b.CategoryID == id
                                select b).ToList();
            return query.ToList();
        }

        public IEnumerable<Post> getSubContentDetails(int id)
        {
            return db.Posts.Where(x => x.Status == true && x.ID == id).ToList();
        }

        public IEnumerable<Post> getSubContentCategories(int id)
        {
            string id1 = id + "";
            List<Post> query = (from a in db.PostCategories
                                join b in db.Posts on a.ID equals b.CategoryID
                                where a.ParentID.ToString().Length > 1 && a.ParentID.ToString().Substring(0, 1) == id1
                                select b).ToList();
            return query.ToList();
        }

        public bool Delete(int id)
        {
            try
            {
                PostCategory user = db.PostCategories.Find(id);
                db.PostCategories.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ////////////////////////////////New//////////////////////////////////////
        public IQueryable<Post> getActivePostsIQueryable()
        {
            return db.Posts.Where(x => x.Status == true);
        }

        public List<Post> getNewestPost()
        {
            return getActivePostsIQueryable().Include(x=>x.PostCategory).OrderByDescending(x => x.CreatedDate).Take(5).ToList();
        }

        public Post ViewDetail(int id)
        {
            return db.Posts.Find(id);
        }
    }
}

