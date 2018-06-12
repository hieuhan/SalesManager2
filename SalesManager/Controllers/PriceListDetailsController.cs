using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesManager.AppCode.Attributes;

namespace SalesManager.Controllers
{
    [SalesManagerAuthorize]
    public class PriceListDetailsController : Controller
    {
        // GET: PriceListDetails
        public ActionResult Index()
        {
            return View();
        }
    }
}