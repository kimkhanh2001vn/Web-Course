using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TMDT.Models.CustomModel;

namespace Common
{
    public class UserCookiesManager
    {
        public static string CheckCookie(string cookieValue)
        {
            List<UserCookie> CookieList = GetCookiesListFromFile();
            return CookieList.Where(x => x.Cookie == cookieValue && DateTime.Now < x.ExpirationDate).Select(x => x.UserName).FirstOrDefault();        }
        
        public static string GetCookieFromRequest()
        {
            string name = string.Empty;
            HttpCookie somecookie = HttpContext.Current.Request.Cookies.Get("somecookie");
            if (somecookie != null)
            {
                name = somecookie["somebrownie"];
            }
            return name;
        }

        public static string GetUserNameFromRequestCookie()
        {
            return CheckCookie(GetCookieFromRequest());
        }

        public static void AddNewCookie(UserCookie newCookie)
        {
            List<UserCookie> CookieList = GetCookiesListFromFile();
            CookieList.Add(newCookie);
            SaveCookie(CookieList);
        }

        public static void RemoveCookie(string cookie)
        {
            List<UserCookie> CookieList = GetCookiesListFromFile();
            CookieList.Remove(CookieList.Where(x => x.Cookie == cookie).FirstOrDefault());
            SaveCookie(CookieList);
        }

        public void UpdateCookie()
        { }

        

        private static void SaveCookie(List<UserCookie> cookieList)
        {
            string dataString = JsonConvert.SerializeObject(cookieList);
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/UserCookie")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UserCookie"));               
            }
            File.WriteAllText(HttpContext.Current.Server.MapPath("~/UserCookie/UserCookie.json"), dataString);
        }

        private static List<UserCookie> GetCookiesListFromFile()
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/UserCookie")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UserCookie"));
                File.WriteAllText(HttpContext.Current.Server.MapPath("~/UserCookie/UserCookie.json"), null);
            }
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/UserCookie/UserCookie.json"));
            string dataString = reader.ReadToEnd();
            reader.Close();
            if (string.IsNullOrEmpty(dataString)) dataString = "[]"; //Make sure this method doesnt return null
            return JsonConvert.DeserializeObject<List<UserCookie>>(dataString)?? new List<UserCookie>();
        }
    }
}