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
    public class SystemErrorsController : Controller
    {
        // GET: SystemErrors
        public ActionResult Index(int page = 1, string dateFrom = "", string dateTo = "")
        {
            int rowCount = 0;
            var model = new SystemErrorsModel
            {
                ListSystemErrors = new SystemErrors().GetPage(0, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, "CrDateTime DESC", "" , dateFrom, dateTo, ref rowCount),
                RowCount = rowCount,
                Pagination = new PaginationModel
                {
                    TotalPage = rowCount,
                    PageSize = SalesManagerConstants.RowAmount20,
                    LinkLimit = 5,
                    PageIndex = page
                }
            };
            return View(model);
        }
    }
}