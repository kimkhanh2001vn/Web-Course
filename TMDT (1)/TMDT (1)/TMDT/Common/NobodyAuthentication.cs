using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Common
{
    public class NobodyAuthentication : ActionFilterAttribute,IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if ( HttpContext.Current.Session["UserInfo"] == null && string.IsNullOrEmpty(UserCookiesManager.GetUserNameFromRequestCookie()))
            {
                return; //If User Session is null and no cookie or cookie not working then authenticated
            }
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary {
                {"action", "Index"},
                {"controller", "Home"}
            });
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {           
        }
    }
}