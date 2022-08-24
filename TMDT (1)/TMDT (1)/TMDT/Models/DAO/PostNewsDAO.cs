using System;
using System.Collections.Generic;
using System.Linq;


namespace TMDT.Models.DAO
{
    public class PostNewsDAO
    {
        private TapChiDB db = new TapChiDB();

        public IEnumerable<Post> ListByGroupAll()
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).ToList(); ;
        }

        public IEnumerable<PostCategory> ListCategory()
        {
            return db.PostCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList(); ;
        }

        public IEnumerable<Post> ListByGroupTT(int top)
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true && x.CategoryID != 1002 && x.CategoryID == 1).Take(top).ToList(); ;
        }

        public IEnumerable<Post> ListByGroupCT(int top)
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).Take(top).ToList(); ;
        }

        public IEnumerable<Post> ListByGroup()
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).ToList(); ;
        }

        public IEnumerable<Post> ListByGroup(int top)
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).Take(top).ToList(); ;
        }

        public IEnumerable<Post> ListByGroupId(long id)
        {
            return db.Posts.Where(x => x.Status == true && x.ID == id).ToList();
        }

        public Post ViewDetail(long id)
        {
            Post item = db.Posts.Find(id);
            // Tăng số lần xem
            item.ViewCount++;
            db.SaveChanges();
            return item;
        }

        public bool Delete(int id)
        {
            try
            {
                Post user = db.Posts.Find(id);
                db.Posts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Deletect(int id)
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
    }
}
