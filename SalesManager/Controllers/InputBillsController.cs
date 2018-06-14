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
    public class InputBillsController : Controller
    {
        // GET: InputBills
        public ActionResult Index(int customerId = 0, int userId = 0, int supplierId = 0, int page = 1, byte billStatusId = 0, byte paymentTypeId = 0, byte warehouseId = 0, string dateFrom = "", string dateTo = "", string fullName = "", string orderBy = "")
        {
            int rowCount = 0;
            var model = new InputBillsModel
            {
                FullName = fullName,
                ListOrderByClauses = OrderByClauses.Static_GetList("InputBills"),
                ListBillStatus = new BillStatus().GetAll(),
                ListPaymentTypes = new PaymentTypes().GetAll(),
                ListWarehouse = new Warehouse().GetAll(),
                ListSuppliers = new Suppliers().GetAll(),
                ListUsers = new Users().GetAll(),
                ListInputBills = new InputBills { CustomerId = customerId, UserId = userId, SupplierId = supplierId, BillStatusId = billStatusId, PaymentTypeId = paymentTypeId , WarehouseId = warehouseId }.GetPage(dateFrom, dateTo, orderBy, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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

        public ActionResult Edit()
        {
            var model = new InputBillsEditModel
            {
                ListBillStatus = new BillStatus().GetAll(),
                ListPaymentTypes = new PaymentTypes().GetAll(),
                ListWarehouse = new Warehouse().GetAll(),
                ListSuppliers = new Suppliers().GetAll(),
                ListUsers = new Users().GetAll()
            };
            //if (manufacturerId > 0)
            //{
            //    var manufacturer = new Manufacturers { ManufacturerId = manufacturerId }.Get();
            //    if (manufacturer.ManufacturerId > 0)
            //    {
            //        model.ManufacturerId = manufacturer.ManufacturerId;
            //        model.ManufacturerName = manufacturer.ManufacturerName;
            //        model.ManufacturerDesc = manufacturer.ManufacturerDesc;
            //        model.DisplayOrder = manufacturer.DisplayOrder;
            //    }
            //}
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InputBillsEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var inputBill = new InputBills
                {
                    InputBillId = model.InputBillId,
                    CustomerId = model.CustomerId,
                    UserId = model.UserId,
                    SupplierId = model.SupplierId,
                    WarehouseId = model.WarehouseId,
                    PaymentTypeId = model.PaymentTypeId,
                    BillStatusId = model.BillStatusId,
                    Notes = model.Notes
                };
                if (model.InputBillId > 0)
                {
                    inputBill.Update(ref sysMessageId);
                }
                else
                {
                    inputBill.Insert(ref sysMessageId);
                    model.InputBillId = inputBill.InputBillId;
                }

                if (sysMessageId > 0)
                {
                    var sysMessage = new SystemMessages().Get(sysMessageId);
                    ModelState.AddModelError("SystemMessages", sysMessage.SystemMessageDesc);
                }
                else ModelState.AddModelError("SystemMessages", "Bạn vui lòng thử lại sau.");
            }
            model.ListBillStatus = new BillStatus().GetAll();
            model.ListPaymentTypes = new PaymentTypes().GetAll();
            model.ListWarehouse = new Warehouse().GetAll();
            model.ListSuppliers = new Suppliers().GetAll();
            model.ListUsers = new Users().GetAll();
            return View(model);
        }
    }
}