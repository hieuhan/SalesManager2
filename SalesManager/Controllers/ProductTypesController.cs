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
    public class ProductTypesController : Controller
    {
        // GET: ProductTypes
        public ActionResult Index(string productTypeName = "", int page = 1)
        {
            int rowCount = 0;
            var model = new ProductTypesModel
            {
                ListProductTypes = new ProductTypes { ProductTypeName = productTypeName }.GetPage("", SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(short productTypeId = 0)
        {
            var model = new ProductTypesEditModel();
            if (productTypeId > 0)
            {
                var productType = new ProductTypes { ProductTypeId = productTypeId }.Get();
                if (productType.ProductTypeId > 0)
                {
                    model.ProductTypeId = productType.ProductTypeId;
                    model.ProductTypeName = productType.ProductTypeName;
                    model.ProductTypeDesc = productType.ProductTypeDesc;
                    model.DisplayOrder = productType.DisplayOrder;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductTypesEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var productType = new ProductTypes
                {
                    ProductTypeId = model.ProductTypeId,
                    ProductTypeName = model.ProductTypeName,
                    ProductTypeDesc = model.ProductTypeDesc,
                    DisplayOrder = model.DisplayOrder
                };
                if (model.ProductTypeId > 0)
                {
                    productType.Update(ref sysMessageId);
                }
                else
                {
                    productType.Insert(ref sysMessageId);
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

        public ActionResult Delete(short productTypeId = 0)
        {
            if (productTypeId > 0)
            {
                short sysMessageId = 0;
                new ProductTypes
                {
                    ProductTypeId = productTypeId
                }.Delete(ref sysMessageId);
            }
            return Redirect("/ProductTypes/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(ProductTypesModel model)
        {
            //ToDo xóa
            if (model.SubmitType.Equals("deleteItems"))
            {
                if (model.ProductTypesId != null && model.ProductTypesId.Length > 0)
                {
                    short systemMessageId = 0;
                    foreach (var productTypesId in model.ProductTypesId)
                    {
                        new ProductTypes
                        {
                            ProductTypeId = productTypesId
                        }.Delete(ref systemMessageId);
                    }
                }
            }
            //ToDo cập nhật thứ tự hiển thị
            else if (model.SubmitType.Equals("updateOrders"))
            {
                if (model.DisplayOrders.HasValue())
                {
                    foreach (var item in model.DisplayOrders)
                    {
                        new ProductTypes
                        {
                            ProductTypeId = item.ProductTypeId,
                            DisplayOrder = item.DisplayOrder
                        }.UpdateDisplayOrder();
                    }
                }
            }
            return Redirect("/ProductTypes/Index");
        }
    }
}