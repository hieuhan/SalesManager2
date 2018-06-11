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
    public class WarrantyController : Controller
    {
        // GET: Warranty
        public ActionResult Index(string warrantyName = "" , int page = 1)
        {
            int rowCount = 0;
            var model = new WarrantyModel
            {
                ListWarranty = new Warranty { WarrantyName = warrantyName }.GetPage("",SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(short warrantyId = 0)
        {
            var model = new WarrantyEditModel();
            if (warrantyId > 0)
            {
                var warranty = new Warranty { WarrantyId = warrantyId }.Get();
                if (warranty.WarrantyId > 0)
                {
                    model.WarrantyId = warranty.WarrantyId;
                    model.WarrantyName = warranty.WarrantyName;
                    model.WarrantyDesc = warranty.WarrantyDesc;
                    model.DisplayOrder = warranty.DisplayOrder;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WarrantyEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var warranty = new Warranty
                {
                    WarrantyId = model.WarrantyId,
                    WarrantyName = model.WarrantyName,
                    WarrantyDesc = model.WarrantyDesc,
                    DisplayOrder = model.DisplayOrder
                };
                if (model.WarrantyId > 0)
                {
                    warranty.Update(ref sysMessageId);
                }
                else
                {
                    warranty.Insert(ref sysMessageId);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(WarrantyModel model)
        {
            //ToDo xóa
            if (model.SubmitType.Equals("deleteItems"))
            {
                if (model.WarrantysId != null && model.WarrantysId.Length > 0)
                {
                    short systemMessageId = 0;
                    foreach (var warrantyId in model.WarrantysId)
                    {
                        new Warranty
                        {
                            WarrantyId = warrantyId
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
                        new Warranty
                        {
                            WarrantyId = item.WarrantyId,
                            DisplayOrder = item.DisplayOrder
                        }.UpdateDisplayOrder();
                    }
                }
            }
            return Redirect("/Warranty/Index");
        }
    }
}