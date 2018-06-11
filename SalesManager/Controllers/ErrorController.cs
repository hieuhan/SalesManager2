using SalesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesManager.Controllers
{
    public class ErrorController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);
            var exception = RouteData.Values["Exception"];
            var errorMessage = RouteData.Values["ErrorMessage"];
            var httpStatusCode = RouteData.Values["HttpStatusCode"];
            var model = new ViewModelBase();
            if (exception != null)
            {
                model.InnerException = (Exception)exception;
            }

            if (errorMessage != null)
            {
                model.ErrorMessage = (String)errorMessage;
            }

            if (httpStatusCode != null)
            {
                model.HttpStatusCode = Response.StatusCode = (Int32)httpStatusCode;
            }
            filterContext.Controller.ViewData.Model = model;
            // TODO: thiết lập true để IIS7 không sử dụng trang báo lỗi riêng
            Response.TrySkipIisCustomErrors = true;
        }

        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}