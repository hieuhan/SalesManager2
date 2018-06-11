using SalesManager.AppCode;
using SalesManager.Models;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SalesManager.Models.SharedModel;

namespace SalesManager.Controllers
{
    public class SharedController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;

        // GET: Shared
        [ChildActionOnly]
        public ActionResult PartialHeader()
        {
            var model = new Actions().GetActionsByUser(_userId);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult PartialPagination(PaginationModel model)
        {
            return PartialView(model);
        }
    }
}