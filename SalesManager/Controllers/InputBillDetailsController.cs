using SalesManager.AppCode;
using SalesManager.AppCode.Attributes;
using SalesManager.Models;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesManager.Controllers
{
    [SalesManagerAuthorize]
    public class InputBillDetailsController : Controller
    {
        // GET: InputBillDetails
        public ActionResult Index(int inputBillId = 0, int page = 1)
        {
            int rowCount = 0;
            var model = new InputBillDetailsModel
            {
                ListInputBillDetails = new InputBillDetails{ }.GetPage("","","",SalesManagerConstants.RowAmount20, page > 1 ? page - 1 : page, ref rowCount)
            };
            return View(model);
        }
    }
}