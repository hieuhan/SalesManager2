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
    public class ProductGroupsController : Controller
    {
        // GET: ProductGroups
        public ActionResult Index(string productGroupName = "", int page = 1)
        {
            int rowCount = 0;
            var model = new ProductGroupsModel
            {
                ListProductGroups = new ProductGroups { ProductGroupName = productGroupName }.GetPage("", SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(short productGroupId = 0)
        {
            var model = new ProductGroupsEditModel();
            if (productGroupId > 0)
            {
                var productGroup = new ProductGroups { ProductGroupId = productGroupId }.Get();
                if (productGroup.ProductGroupId > 0)
                {
                    model.ProductGroupId = productGroup.ProductGroupId;
                    model.ProductGroupName = productGroup.ProductGroupName;
                    model.ProductGroupDesc = productGroup.ProductGroupDesc;
                    model.DisplayOrder = productGroup.DisplayOrder;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductGroupsEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var productGroup = new ProductGroups
                {
                    ProductGroupId = model.ProductGroupId,
                    ProductGroupName = model.ProductGroupName,
                    ProductGroupDesc = model.ProductGroupDesc,
                    DisplayOrder = model.DisplayOrder
                };
                if (model.ProductGroupId > 0)
                {
                    productGroup.Update(ref sysMessageId);
                }
                else
                {
                    productGroup.Insert(ref sysMessageId);
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

        public ActionResult Delete(short productGroupId = 0)
        {
            if (productGroupId > 0)
            {
                short sysMessageId = 0;
                new ProductGroups
                {
                    ProductGroupId = productGroupId
                }.Delete(ref sysMessageId);
            }
            return Redirect("/ProductGroups/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(ProductGroupsModel model)
        {
            //ToDo xóa
            if (model.SubmitType.Equals("deleteItems"))
            {
                if (model.ProductGroupsId != null && model.ProductGroupsId.Length > 0)
                {
                    short systemMessageId = 0;
                    foreach (var productGroupId in model.ProductGroupsId)
                    {
                        new ProductGroups
                        {
                            ProductGroupId = productGroupId
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
                        new ProductGroups
                        {
                            ProductGroupId = item.ProductGroupId,
                            DisplayOrder = item.DisplayOrder
                        }.UpdateDisplayOrder();
                    }
                }
            }
            return Redirect("/ProductGroups/Index");
        }
    }
}