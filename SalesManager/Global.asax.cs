using SalesManager.Controllers;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SalesManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            Context.Response.Clear();
            Context.ClearError();
            var httpException = exception as HttpException ?? new HttpException((Int32)HttpStatusCode.InternalServerError, "Internal Server Error", exception);
            var httpStatusCode = httpException.GetHttpCode();
            if (exception != null)
            {
                Logger.Start();
                var log = new Logger(typeof(MvcApplication)); 
                log.Log(Logger.ERROR, "Application_Error", exception);

                var routeData = new RouteData();
                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = "Index";
                routeData.Values.Add("Exception", exception);
                routeData.Values.Add("ErrorMessage", httpException.ToString());
                routeData.Values.Add("HttpStatusCode", httpStatusCode);

                Server.ClearError();
                IController controller = new ErrorController();
                controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
        }
    }
}
