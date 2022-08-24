using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMDT.Models.CustomModel
{
    public class UserCookie
    { 
        //Model for UserSessionManager in common
        public string UserName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Cookie { get; set; }
     }
}