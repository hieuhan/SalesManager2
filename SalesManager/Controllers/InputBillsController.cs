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
        public ActionResult Index(int customerId = 0, int userId = 0, int supplierId = 0, int page = 1, byte billStatusId = 0, byte paymentTypeId = 0, byte warehouseId = 0, string dateFrom = "", string dateTo = "")
        {
            int rowCount = 0;
            var model = new InputBillsModel
            {
                ListBillStatus = new BillStatus().GetAll(),
                ListPaymentTypes = new PaymentTypes().GetAll(),
                ListWarehouse = new Warehouse().GetAll(),
                ListSuppliers = new Suppliers().GetAll(),
                ListUsers = new Users().GetAll(),
                ListInputBills = new InputBills { CustomerId = customerId, UserId = userId, SupplierId = supplierId, BillStatusId = billStatusId, PaymentTypeId = paymentTypeId , WarehouseId = warehouseId }.GetPage(dateFrom, dateTo, string.Empty, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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