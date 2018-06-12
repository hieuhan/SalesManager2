using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesManager.AppCode;
using SalesManager.AppCode.Attributes;
using SalesManager.Models;
using SalesManagerLib;

namespace SalesManager.Controllers
{
    [SalesManagerAuthorize]
    public class PriceListDetailsController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;
        // GET: PriceListDetails
        public ActionResult Index(int priceListId = 0, int page = 1)
        {
            int rowCount = 0;
            var model = new PriceListDetailsModel
            {
                ListUnits = new Units().GetList(),
                ListUsers = new Users().GetAll(),
                ListStatus = new Status().GetAll(),
                ListPriceListDetails = priceListId > 0 ? new PriceListDetails { PriceListId = priceListId }.GetPage(string.Empty, string.Empty, string.Empty, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount) : new List<PriceListDetails>(),
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PriceListDetailsModel model)
        {
            if(model.PriceListId > 0 && model.SubmitType.Equals("insertPriceListDetail"))
            {
                new PriceListDetails { PriceListId = model.PriceListId, CrUserId = _userId }.Insert_Auto();
            }
            else if (model.SubmitType.Equals("updatePrices"))
            {
                if (model.PriceListDetailsDisplayOrders.HasValue())
                {
                    foreach (var item in model.PriceListDetailsDisplayOrders)
                    {
                        new PriceListDetails
                        {
                            UpdateUserId = _userId,
                            PriceListDetailId = item.PriceListDetailId,
                            Price = Double.Parse(item.Price, System.Globalization.NumberStyles.Currency)
                        }.UpdatePrice();
                    }
                }
            }
            return Redirect(string.Concat("/PriceListDetails/Index?priceListId=", model.PriceListId));
        }
    }
}