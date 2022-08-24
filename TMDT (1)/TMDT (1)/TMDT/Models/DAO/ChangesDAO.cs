using System;

namespace TMDT.Models.DAO
{
    public class ChangesDAO
    {
        private TapChiDB db = new TapChiDB();

        public bool PostsStatus(int id)
        {
            Post post = db.Posts.Find(id);
            post.Status = !post.Status;
            db.SaveChanges();
            return post.Status;
        }
        public bool PostsHomeFlag(int id)
        {
            Post post = db.Posts.Find(id);
            post.FreeCourseStatus = !post.FreeCourseStatus;
            db.SaveChanges();
            return post.FreeCourseStatus;
        }
        public bool OrderStatus(int id)
        {
            Order order = db.Orders.Find(id);
            order.Status = !order.Status;
            db.SaveChanges();
            return order.Status;
        }
        public bool PostsCateStatus(int id)
        {
            PostCategory postcat = db.PostCategories.Find(id);
            postcat.Status = !postcat.Status;
            db.SaveChanges();
            return postcat.Status;
        }
        public bool PostsCateHomeFlag(int id)
        {
            PostCategory postcat = db.PostCategories.Find(id);
            postcat.FreeCourseStatus = !postcat.FreeCourseStatus;
            db.SaveChanges();
            return postcat.FreeCourseStatus;
        }

        public bool SubscribedEmailStatus(int id)
        {
            SubscribedEmail email = db.SubscribedEmails.Find(id);
            email.Status = !email.Status;
            db.SaveChanges();
            return email.Status;
        }

        public bool PageDataStatus(int id)
        {
            PageData pageData = db.PageDatas.Find(id);
            pageData.Status = pageData.Status;
            db.SaveChanges();
            return pageData.Status;
        }

        public bool FooterContentStatus(int id)
        {
            FooterContent status = db.FooterContents.Find(id);
            status.Status = !status.Status;
            db.SaveChanges();
            return status.Status;
        }

        public bool SlideStatus(int id)
        {
            SliderTop slide = db.SliderTops.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }

        public bool AdsStatus(int id)
        {
            Advertisement ad = db.Advertisements.Find(id);
            ad.Status = !ad.Status;
            db.SaveChanges();
            return ad.Status;
        }
        public bool VideosStatus(int id)
        {
            Video video = db.Videos.Find(id);
            video.Status = !video.Status;
            db.SaveChanges();
            return video.Status;
        }
    }
}
