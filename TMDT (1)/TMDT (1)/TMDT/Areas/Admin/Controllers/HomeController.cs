using Common;
using System;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TMDT.Models.CustomModel;
using System.Threading.Tasks;

namespace TMDT.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/Home
        [AuthorizeBusiness]
        public ActionResult Index()
        {           
                GoogleAPI googleApi = new GoogleAPI();
                GoogleApiGetData api = new GoogleApiGetData();
                googleApi = db.GoogleAPIs.FirstOrDefault();
                ViewBag.Count = api.Pageview(googleApi.GoogleJson, googleApi.ViewID);
                ViewBag.Graph = api.Graphs(googleApi.GoogleJson, googleApi.ViewID);
                ViewBag.NotShowContentHeader = 1; //If exists , content header section in Layout will not show
                return View();            
        }
        [NobodyAuthentication]
        public ActionResult Login()
        {     
            return View();           
        }

        [HttpPost]
        public ActionResult Login(string userName, string password, string remember)
        {                        
            string passMD5 = Encryptor.MD5Hash(userName + password);
            UserInfo userInfo = db.UserAdministrators
                            .Include(x => x.UserRole)
                            .Where(x => x.UserName == userName && x.Password == passMD5 && x.Allowed == true)
                            .Select(x=> new UserInfo
                            {
                                UserId = x.UserId,
                                UserName = x.UserName,
                                FullName = x.FullName,
                                Email=x.Email,
                                Avatar = x.Avatar,
                                RoleID = x.RoleID,
                                RoleName = x.UserRole.Name,
                                CreatedDate = x.CreatedDate
                            }).FirstOrDefault();
            if (userInfo != null)
            {
                Session["UserInfo"] = userInfo;
                DateTime Today = DateTime.Now;
                UserCookie userCookie = new UserCookie {
                                                Cookie = Encryptor.MD5Hash(passMD5 + Today.ToString()),
                                                UserName = userInfo.UserName,
                                                ExpirationDate = Today.AddDays(30)
                                            };
                UserCookiesManager.AddNewCookie(userCookie);
                ///////////////////////////////////
                HttpCookie somecookie = new HttpCookie("somecookie");
                somecookie["somebrownie"] = userCookie.Cookie;
                if(remember == "on") somecookie.Expires = userCookie.ExpirationDate;
                Response.Cookies.Add(somecookie);
                /////////////////////////////////////
                return RedirectToAction("Index");
            }
            ViewBag.error = "Đăng nhập sai hoặc bạn không có quyền vào";
            return View();
        }
        
        public ActionResult Logout()
        {
            Session["UserInfo"] = null;
            Response.Cookies["somecookie"].Expires = DateTime.Now.AddDays(-1);
            UserCookiesManager.RemoveCookie(UserCookiesManager.GetCookieFromRequest());
            return RedirectToAction("Login");
        }

        public ActionResult NotificationAuthorize()
        {
            return View();
        }

        [NobodyAuthentication]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NobodyAuthentication]
        public async Task<ActionResult> Register([Bind(Include = "UserName,Password,FullName,Email")] UserAdministrator userAdministrator)
        {
            if(ModelState.IsValid)
            {
                if(db.UserAdministrators.Where(x=>x.UserName == userAdministrator.UserName).Any())
                {                    
                    ModelState.AddModelError("UserName", "Tên đăng nhập đã có người sử dụng");
                    return View(userAdministrator);
                }
                if (db.UserAdministrators.Where(x => x.Email == userAdministrator.Email).Any())
                {
                    ModelState.AddModelError("Email", "Email đã có người sử dụng");
                    return View(userAdministrator);
                }                
                userAdministrator.Password = Encryptor.HashAdminPassword(userAdministrator.UserName, userAdministrator.Password);
                userAdministrator.CreatedDate = DateTime.Now;
                userAdministrator.Allowed = false;
                userAdministrator.RoleID = 3;
                db.UserAdministrators.Add(userAdministrator);
                await db.SaveChangesAsync();
                TempData["CreateUserSucceed"] = 1;
                return RedirectToAction("Login");
            }
            return View(userAdministrator);
        }
        //public EmptyResult KeepAlive()
        //{
        //    Session["UserInfo"] = Session["UserInfo"];
        //    return new EmptyResult();
        //}       
        
    }
}