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
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        public ActionResult Index(string warehouseName = "", int page =1)
        {
            int rowCount = 0;
            var model = new WarehouseModel
            {
                ListWarehouseStatus = new WarehouseStatus().GetAll(),
                ListWarehouse = new Warehouse { WarehouseName = warehouseName }.GetPage("", SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(byte warehouseId = 0)
        {
            var model = new WarehouseEditModel();
            if (warehouseId > 0)
            {
                var warehouse = new Warehouse { WarehouseId = warehouseId }.Get();
                if (warehouse.WarehouseId > 0)
                {
                    model.WarehouseId = warehouse.WarehouseId;
                    model.WarehouseName = warehouse.WarehouseName;
                    model.WarehouseDesc = warehouse.WarehouseDesc;
                    model.Address = warehouse.Address;
                    model.Mobile = warehouse.Mobile;
                    model.WarehouseStatusId = warehouse.WarehouseStatusId;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WarehouseEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var warehouse = new Warehouse
                {
                    WarehouseId = model.WarehouseId,
                    WarehouseName = model.WarehouseName,
                    WarehouseDesc = model.WarehouseDesc,
                    Address = model.Address,
                    Mobile = model.Mobile,
                    WarehouseStatusId = model.WarehouseStatusId
                };
                if (model.WarehouseId > 0)
                {
                    warehouse.Update(ref sysMessageId);
                }
                else
                {
                    warehouse.Insert(ref sysMessageId);
                }

                if (sysMessageId > 0)
                {
                    var sysMessage = new SystemMessages().Get(sysMessageId);
                    ModelState.AddModelError("SystemMessages", sysMessage.SystemMessageDesc);
                }
                else ModelState.AddModelError("SystemMessages", "Bạn vui lòng thử lại sau.");
            }
            return View(model);
        }

        public ActionResult Delete(byte warehouseId = 0)
        {
            if (warehouseId > 0)
            {
                short sysMessageId = 0;
                new Warehouse
                {
                    WarehouseId = warehouseId
                }.Delete(ref sysMessageId);
            }
            return Redirect("/Warehouse/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(byte[] WarehousesId, string SubmitType)
        {
            if (!string.IsNullOrEmpty(SubmitType) && SubmitType.Equals("deleteItems"))
            {
                if (WarehousesId != null && WarehousesId.Length > 0)
                {
                    short sysMessageId = 0;
                    foreach (var warehouseId in WarehousesId)
                    {
                        new Warehouse
                        {
                            WarehouseId = warehouseId
                        }.Delete(ref sysMessageId);
                    }
                }
            }
            return Redirect("/Warehouse/Index");
        }
    }
}