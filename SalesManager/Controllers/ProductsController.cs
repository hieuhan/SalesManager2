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
        private readonly int _userId = SessionHelpers.UserId;
        // GET: Products
        public ActionResult Index(string productName = "", string orderBy = "", string dateFrom = "", string dateTo = "", int manufacturerId = 0, short productGroupId = 0, short productTypeId = 0, short originId = 0, short warrantyId = 0 , short unitId = 0 , byte statusId = 0, int userId = 0, int page = 1)
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
                ListOrderByClauses = OrderByClauses.Static_GetList("Products"),
                ListUsers = new Users().GetAll(),
                ListProducts = new Products { ProductName = productName, ProductGroupId = productGroupId, ProductTypeId = productTypeId, ManufacturerId = manufacturerId, OriginId = originId, WarrantyId = warrantyId, UnitId = unitId, StatusId = statusId, CrUserId = userId }.GetPage(dateFrom, dateTo, orderBy, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(int productId = 0)
        {
            var model = new ProductsEditModel();
            if (productId > 0)
            {
                var product = new Products { ProductId = productId }.Get();
                if (product.ProductId > 0)
                {
                    model.ProductId = product.ProductId;
                    model.ProductName = product.ProductName;
                    model.ImagePath = product.ImagePath;
                    model.ManufacturerId = product.ManufacturerId;
                    model.UnitId = product.UnitId;
                    model.ProductGroupId = product.ProductGroupId;
                    model.ProductTypeId = product.ProductTypeId;
                    model.OriginId = product.OriginId;
                    model.WarrantyId = product.WarrantyId;
                    model.StatusId = product.StatusId;
                    model.ProductContent = product.ProductContent;
                    model.CrUserId = product.CrUserId;
                    model.UpdateUserId = product.UpdateUserId;
                    model.CrDateTime = product.CrDateTime;
                    model.DisplayOrder = product.DisplayOrder;
                }
            }
            model.ListManufacturers = new Manufacturers().GetList();
            model.ListProductGroups = new ProductGroups().GetList();
            model.ListProductTypes = new ProductTypes().GetList();
            model.ListOrigin = new Origin().GetList();
            model.ListWarranty = new Warranty().GetList();
            model.ListUnits = new Units().GetList();
            model.ListStatus = new Status().GetAll();
            model.ListOrderByClauses = OrderByClauses.Static_GetList("Products");
            model.ListUsers = new Users().GetAll();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductsEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var product = new Products
                {
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    ImagePath = model.ImageDisplay ? string.Empty : model.ImagePath,
                    ManufacturerId = model.ManufacturerId,
                    UnitId = model.UnitId,
                    ProductGroupId = model.ProductGroupId,
                    ProductTypeId = model.ProductTypeId,
                    WarrantyId = model.WarrantyId,
                    OriginId = model.OriginId,
                    StatusId = model.StatusId,
                    ProductContent = model.ProductContent,
                    CrUserId = _userId,
                    UpdateUserId = _userId,
                    DisplayOrder = model.DisplayOrder
                };
                if (model.ProductId > 0)
                {
                    product.Update(ref sysMessageId);
                }
                else
                {
                    product.Insert(ref sysMessageId);
                }

                if (sysMessageId > 0)
                {
                    var sysMessage = new SystemMessages().Get(sysMessageId);
                    ModelState.AddModelError("SystemMessages", sysMessage.SystemMessageDesc);
                }
                else ModelState.AddModelError("SystemMessages", "Bạn vui lòng thử lại sau.");
            }
            model.ListManufacturers = new Manufacturers().GetList();
            model.ListProductGroups = new ProductGroups().GetList();
            model.ListProductTypes = new ProductTypes().GetList();
            model.ListOrigin = new Origin().GetList();
            model.ListWarranty = new Warranty().GetList();
            model.ListUnits = new Units().GetList();
            model.ListStatus = new Status().GetAll();
            model.ListOrderByClauses = OrderByClauses.Static_GetList("Products");
            model.ListUsers = new Users().GetAll();
            return View(model);
        }

    }
}