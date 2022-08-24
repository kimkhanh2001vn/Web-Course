using Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TMDT.Common;
using TMDT.Models.DAO;
using System.Web.Script.Serialization;


namespace TMDT.Controllers
{
    public class HomeController : Controller
    {
        private TapChiDB db = new TapChiDB();

        public ActionResult DaoTao()
        {
            ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();

            return View();
        }
        public ActionResult Index()
        {
            //ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            //ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();
            ViewBag.TinTucNew = db.TinTucNews.ToList();
            ViewBag.Topmenu = db.Topmenus.ToList();
            ViewBag.khachhang = db.Khachhangs.ToList();
            ViewBag.duanganday = db.duangandays.ToList();
            ViewBag.taisaochontoi = db.taisaochontois.ToList();
            ViewBag.dichvuchinh = db.dichvuchinhs.ToList();
            ViewBag.hinhtruot = db.Hinhtruots.ToList();
            ViewBag.dacsac = db.dacsacs.ToList();
            return View();
        }
        public ActionResult Tesst()
        {
           
            return View();
        }
        public ActionResult thuecanho()
        {
            ViewBag.thue = db.thues.ToList();
            return View();
        }
        public ActionResult Details(int id)
        {
            //         .Join(database.Post_Metas, // the source table of the inner join
            //post => post.ID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
            //meta => meta.Post_ID,   // Select the foreign key (the second part of the "on" clause)
            //(post, meta) => new { Post = post, Meta = meta }) // selection
            var Course = db.Posts.Where(x => x.ID == id).ToList();
            if ( Course.Count == 0)
            {
               Course = db.Posts.Where(x => x.CategoryID == id).ToList();
            }
            else
            {
                Course = db.Posts.Where(x => x.ID == id).ToList();
            }
            string courseName = Course[0].Name;
            var CourseList = db.Courses.Where(x => x.CourseName == courseName).ToList();
            int courseID = CourseList[0].CourseID;
            ViewBag.Chapters = db.Chapters.Where(x => x.CourseID == courseID).ToList();
            ViewBag.ChapterDetails = db.Chapters.Where(x => x.CourseID == courseID).Join(db.ChapterDetails, chapter => chapter.ChapterID, chapterDetail => chapterDetail.ChapterID, (chapter, chapterDetail) => new
            {
                chapter.ChapterName,
                chapter.ChapterID,
                chapterDetail.ChapterDetailsID,
                chapterDetail.ChapterDetailsName
            }).ToList();

            ViewBag.Course = CourseList;
            ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();

            return View();
        }

        public ActionResult ProductDetail(int id)
        {

            ViewBag.ProductDetail = db.Posts.Where(x => x.Status == true && x.ID == id).ToList(); ;

            return View();
        }

        //public ActionResult Sport()
        //{
        //    ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
        //    ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();

        //    return View();
        //}
        public ActionResult TuVan()
        {

            ViewBag.TuVan = db.TuVans.ToList();
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult vatlieuda()
        {
            ViewBag.vatlieu = db.VatLieux.ToList();
            return View();
        }
        public ActionResult VatLieu()
        {
            ViewBag.vatlieu = db.VatLieux.ToList();
            return View();
        }
        public ActionResult Furniture()
        {
            ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();

            return View();
        }
        //public ActionResult Thue()
        //{
        //    ViewBag.Message = "Your contact page.";
        //    ViewBag.thue = db.thues.ToList();

        //    return View();
        //}
        public ActionResult Thue()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.thue = db.thues.ToList();

            return View();
        }
        public ActionResult Registration()
        {
            ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();
            //    var pList = new OrderDetail();

            //    pList.OrderID = 1;
            //    pList.ProductID = (int)obj["id"];
            //    pList.Price = double.Parse(obj["price"] + "");
            //    pList.Quantity = int.Parse(obj["quantity"] + "");

            //    db.OrderDetails.Add(pList);
            //    db.SaveChanges();



            return View();
        }
        public ActionResult Oder()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult Login()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        public ActionResult Login(FormCollection collection)
        {
            ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();
            try
            {
                string account = Request.Form["txtEmail"].ToString();
                string password = Request.Form["txtPass"].ToString();
                string pass = Encryptor.HashAdminPassword(account, password);
                var checkpass = db.Users.Where(x => x.UserName == account&& x.Password == pass).ToList();
                var checkrole = db.Users.Where(x => x.UserName == account && x.Password == pass).ToList();
                string role = checkrole[0].Role.ToString();
                if ( checkpass.Count > 0 && role=="Student")
                {
                    HttpContext.Application["StudentName"] = checkrole[0].UserName.ToString();
                    HttpContext.Application["ID"] = checkrole[0].ID.ToString();

                    Response.Redirect("/Home/PageUser") ;
                    int id = int.Parse(Session["ID"].ToString());
                }

                //else if(checkpass.Count > 0 && role == "Teacher")
                //{   
                //    Teacher.teacher =int.Parse(checkrole[0].ID.ToString());
                //    Response.Redirect("/Admin/Courses/Index");
                //}

                else
                {
                    Response.Redirect("/Home/Login");
                }

            }
            catch(Exception ex)
            { }


            ViewBag.Message = "Your contact page."; 

            return View();
        }
        public ActionResult SendMail(FormCollection collection)
        {
            //MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
            //SmtpClient client = new SmtpClient();
            //client.Port = 25;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Host = "smtp.gmail.com";
            //mail.Subject = "this is a test email.";
            //mail.Body = "this is my test email body";
            //client.Send(mail);
            string inputName = Request.Form["txtName"].ToString();
            string inputEmail = Request.Form["txtEmail"].ToString();
            string inputPhone = Request.Form["txtPhone"].ToString();
            string inputMessage = Request.Form["txtMessage"].ToString();
            //if (CheckClient.IsEmail(inputEmail))
            //{
            //if (!(new CheckClient().EmailExists(inputEmail)))
            //{

            string from = "lehaiha10@gmail.com";
            //Replace this with your own correct Gmail Address
            //thanh.tran @viet-tradeplus.com.vn
            string to = "ngokhanh858@gmail.com";
            //thanh.tran @viet-tradeplus.com.vn
            //Replace this with the Email Address
            //to whom you want to send the mail

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from, "le Hai Ha", System.Text.Encoding.UTF8);
            mail.Subject = "Request from Clients";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = inputMessage;
            ;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            //587  port google mail
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            //Add the Creddentials- use your own email id and password
            System.Net.NetworkCredential nt =
            new System.Net.NetworkCredential(from, "lehaihadeptrai");

            client.Port = 587; // Gmail works on this port
            client.EnableSsl = true; //Gmail works on Server Secured Layer
            client.UseDefaultCredentials = false;
            client.Credentials = nt;
            client.Send(mail);
            return View("Contact");
        }
        public ActionResult Product()
        {
            ViewBag.Product = db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).ToList();

            return View();
        }
        public ActionResult AboutUs()
        {
          
            return View();
        }
        public ActionResult UserDetails(int id)
        {
            var Course = db.Posts.Where(x => x.ID == id).ToList();
            string courseName = Course[0].Name;
            var CourseList = db.Courses.Where(x => x.CourseName == courseName).ToList();
            int courseID = CourseList[0].CourseID;
            ViewBag.Chapters = db.Chapters.Where(x => x.CourseID == courseID).ToList();
            ViewBag.ChapterDetails = db.Chapters.Where(x => x.CourseID == courseID).Join(db.ChapterDetails, chapter => chapter.ChapterID, chapterDetail => chapterDetail.ChapterID, (chapter, chapterDetail) => new
            {
                chapter.ChapterName,
                chapter.ChapterID,
                chapterDetail.ChapterDetailsID,
                chapterDetail.ChapterDetailsName
            }).ToList();
            ViewBag.Course = CourseList;
            ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();
           
            return View();
        }
        public ActionResult PageUser()
        {
            //ViewBag.StudentName = ;
            string s = HttpContext.Application["StudentName"].ToString();
            // Split string on spaces.
            // ... This will separate all the words.
            string[] words = s.Split('@');
            string value = words[0];
            ViewBag.StudentName = value;
            int id = int.Parse(HttpContext.Application["ID"].ToString());
            ViewBag.PostCatUser = db.Orders.Where(x => x.CustomerId == id).Join(db.OrderDetails, Order => Order.ID, OrderDetails => OrderDetails.OrderID, (Order, OrderDetail) => new
            {
              OrderDetail.ProductID
            }).Join(db.Posts,OrderDetail=>OrderDetail.ProductID,Posts=>Posts.ID,(OrderDetail,Posts) => new
            {
                Posts.ID,
                Posts.Name,
                Posts.Image,
                Posts.OldCost,
                Posts.NewCost,
            }).Join(db.Courses, Posts => Posts.Name, Course => Course.CourseName, (Posts, Course) => new
            {
                Course.PFFPath,
                Posts.ID,
                Posts.Name,
                Posts.Image,
                Posts.OldCost,
                Posts.NewCost,
            }).ToList();

            //ViewBag.Course = db.Orders.Where(x => x.Status == true && x.CustomerId == id).Join(db.OrderDetails, Order => Order.ID, OrderDetails => OrderDetails.OrderID, (Order, OrderDetail) => new
            //{
            //    OrderDetail.ProductID
            //}).Join(db.Posts, OrderDetail => OrderDetail.ProductID, Posts => Posts.ID, (OrderDetail, Posts) => new
            //{
            //    Posts.Name
            //}).Join(db.Courses, Posts => Posts.Name, Course => Course.CourseName, (Posts, Course) => new
            //{
            //    Course.PFFPath
            //}).ToList();

            ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();
            //ViewBag.OrderDetails = db.OrderDetails.ToList();
            //ViewBag.Order = db.Orders.Where(x => x.Status == true).ToList();
            //ViewBag.Message = "Page User";
            return View();
        }
        private void callPost()
        {
            ViewBag.Post = db.Posts.Where(x => x.Status == true).ToList();
            ViewBag.PostCate = db.PostCategories.Where(x => x.Status == true).ToList();
        }
        public ActionResult CreateAccount(FormCollection collection)
        {
            //MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
            //SmtpClient client = new SmtpClient();
            //client.Port = 25;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Host = "smtp.gmail.com";
            //mail.Subject = "this is a test email.";
            //mail.Body = "this is my test email body";
            //client.Send(mail);



            callPost();
            string account = Request.Form["txtUsername"].ToString();
            string password = Request.Form["txtPassword"].ToString();
            string pass = Encryptor.HashAdminPassword(account, password);
            var pList = new User();
            pList.UserName = account;
            pList.Password = pass;
            pList.Role = "Student";

            var value = db.Users.Where(x => x.UserName == account).ToList();

            if (value.Count() > 0)
            {
                ViewBag.Success = "Tài khoản đã tồn tại";

            }
            else
            {
                ViewBag.Success = "Đã tạo thành công tài khoản";
                db.Users.Add(pList);
                db.SaveChanges();
            }

            ////if (CheckClient.IsEmail(inputEmail))
            ////{
            ////if (!(new CheckClient().EmailExists(inputEmail)))
            ////{

            //string from = "lehaiha10@gmail.com";
            ////Replace this with your own correct Gmail Address
            ////thanh.tran @viet-tradeplus.com.vn
            //string to = "sales@atzcommodities.vn";
            ////thanh.tran @viet-tradeplus.com.vn
            ////Replace this with the Email Address
            ////to whom you want to send the mail

            //System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            //mail.To.Add(to);
            //mail.From = new MailAddress(from, "le Hai Ha", System.Text.Encoding.UTF8);
            //mail.Subject = "Request from Clients";
            //mail.SubjectEncoding = System.Text.Encoding.UTF8;
            //mail.Body = inputMessage;
            //;
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
            //mail.IsBodyHtml = true;
            //mail.Priority = MailPriority.High;
            ////587  port google mail
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            ////Add the Creddentials- use your own email id and password
            //System.Net.NetworkCredential nt =
            //new System.Net.NetworkCredential(from, "lehaihadeptrai");

            //client.Port = 587; // Gmail works on this port
            //client.EnableSsl = true; //Gmail works on Server Secured Layer
            //client.UseDefaultCredentials = false;
            //client.Credentials = nt;
            //client.Send(mail);
            return View("Registration");
        }

 
    }
}
    

    //public ActionResult Product()
    //        {
    //            ViewBag.Product = db.Posts.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).ToList();

    //            return View();
    //        }

    //    }
    //}
