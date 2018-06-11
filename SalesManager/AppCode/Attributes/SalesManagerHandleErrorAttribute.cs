using SalesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesManager.AppCode.Attributes
{
    public class SalesManagerHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext is null");
            }
            //ICSoft.ViewLibV3.LogUtil.WriteLog(filterContext.Exception.ToString(), "LawsHandleErrorAttribute OnException");
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var httpException = new HttpException(null, filterContext.Exception);

            if (new HttpException(null, httpException).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(httpException))
            {
                return;
            }

            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new AjaxResult
                {
                    AllowGet = true,
                    Data = new
                    {
                        Completed = false,
                        Message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var controllerName = (String)filterContext.RouteData.Values["controller"];
                var actionName = (String)filterContext.RouteData.Values["action"];
                var model = new ViewModelBase
                {
                    InnerException = httpException,
                    ErrorMessage = filterContext.Exception.Message,
                    ControllerName = controllerName,
                    ActionName = actionName
                };

                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Error/Index.cshtml",
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}