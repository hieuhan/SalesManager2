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
    public class PriceListsController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;
        // GET: PriceLists
        public ActionResult Index(string priceListName = "", string dateFrom = "", string dateTo = "", string orderBy = "", int page = 1)
        {
            int rowCount = 0;
            var model = new PriceListsModel
            {
                ListStatus = new Status().GetAll(),
                ListUsers = new Users().GetAll(),
                ListPriceListTypes = new PriceListTypes().GetAll(),
                ListOrderByClauses = OrderByClauses.Static_GetList("PriceLists"),
                ListPriceLists = new PriceLists { PriceListName = priceListName }.GetPage(dateFrom, dateTo, orderBy, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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

        [HttpGet]
        public ActionResult Edit(int priceListId = 0)
        {
            var model = new PriceListsEditModel();
            if (priceListId > 0)
            {
                var priceList = new PriceLists { PriceListId = priceListId }.Get();
                if (priceList.PriceListId > 0)
                {
                    model.PriceListId = priceList.PriceListId;
                    model.PriceListName = priceList.PriceListName;
                    model.PriceListDesc = priceList.PriceListDesc;
                    model.PriceListTypeId = priceList.PriceListTypeId;
                    model.DisplayOrder = priceList.DisplayOrder;
                    model.IsDefault = priceList.IsDefault == 1;
                    model.StatusId = priceList.StatusId;
                }
            }
            model.ListStatus = new Status().GetAll();
            model.ListPriceListTypes = new PriceListTypes().GetAll();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PriceListsEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var priceList = new PriceLists
                {
                    PriceListId = model.PriceListId,
                    PriceListName = model.PriceListName,
                    PriceListDesc = model.PriceListDesc,
                    PriceListTypeId = model.PriceListTypeId,
                    StatusId = model.StatusId,
                    IsDefault = (byte) (model.IsDefault ? 1 : 0),
                    DisplayOrder = model.DisplayOrder
                };
                if (model.PriceListId > 0)
                {
                    priceList.UpdateUserId = _userId;
                    priceList.Update(ref sysMessageId);
                }
                else
                {
                    priceList.CrUserId = _userId;
                    priceList.Insert(ref sysMessageId);
                }

                if (sysMessageId > 0)
                {
                    var sysMessage = new SystemMessages().Get(sysMessageId);
                    ModelState.AddModelError("SystemMessages", sysMessage.SystemMessageDesc);
                }
                else ModelState.AddModelError("SystemMessages", "Bạn vui lòng thử lại sau.");
            }
            model.ListStatus = new Status().GetAll();
            model.ListPriceListTypes = new PriceListTypes().GetAll();
            return View(model);
        }

        public ActionResult Delete(short priceListId = 0)
        {
            if (priceListId > 0)
            {
                short sysMessageId = 0;
                new PriceLists
                {
                    PriceListId = priceListId
                }.Delete(ref sysMessageId);
            }
            return Redirect("/PriceLists/Index");
        }
    }
}