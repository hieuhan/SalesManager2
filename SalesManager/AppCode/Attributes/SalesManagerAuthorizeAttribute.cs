using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SalesManager.AppCode.Attributes
{
    public class SalesManagerAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SessionHelpers.UserId > 0)
            {
                bool isInPageRole = new Users(LibConstants.CONNECTION_STRING).HasPriv(SessionHelpers.UserId,
                    HttpContext.Current.Request.Url.AbsolutePath);
                if (!isInPageRole)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new
                    {
                        controller = "Users",
                        action = "Login",
                        returnUrl = filterContext.HttpContext.Request.RawUrl //HttpUtility.UrlEncode
                    }));
            }
        }
    }
}