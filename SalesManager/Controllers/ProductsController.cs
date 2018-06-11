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
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(string productName = "", string orderBy = "", string dateFrom = "", string dateTo = "", int manufacturerId = 0, short productGroupId = 0, short productTypeId = 0, short originId = 0, short warrantyId = 0 , short unitId = 0 , byte statusId = 0, int page = 1)
        {
            int rowCount = 0;
            var model = new ProductsModel
            {
                ListManufacturers = new Manufacturers().GetList(),
                ListProductGroups = new ProductGroups().GetList(),
                ListProductTypes = new ProductTypes().GetList(),
                ListOrigin = new Origin().GetList(),
                ListWarranty = new Warranty().GetList(),
                ListUnits = new Units().GetList(),
                ListStatus = new Status().GetAll(),
                ListProducts = new Products { ProductName = productName, ProductGroupId = productGroupId, ProductTypeId = productTypeId, ManufacturerId = manufacturerId, OriginId = originId, WarrantyId = warrantyId, UnitId = unitId, StatusId = statusId }.GetPage(dateFrom, dateTo, orderBy, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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