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
    public class OriginController : Controller
    {
        // GET: Origin
        public ActionResult Index(string originName = "", int page = 1)
        {
            int rowCount = 0;
            var model = new OriginModel
            {
                ListOrigin = new Origin { OriginName = originName}.GetPage("",SalesManagerConstants.RowAmount20,page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(short originId = 0)
        {
            var model = new OriginEditModel();
            if (originId > 0)
            {
                var origin = new Origin { OriginId = originId }.Get();
                if (origin.OriginId > 0)
                {
                    model.OriginId = origin.OriginId;
                    model.OriginName = origin.OriginName;
                    model.OriginDesc = origin.OriginDesc;
                    model.DisplayOrder = origin.DisplayOrder;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OriginEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var origin = new Origin
                {
                    OriginId = model.OriginId,
                    OriginName = model.OriginName,
                    OriginDesc = model.OriginDesc,
                    DisplayOrder = model.DisplayOrder
                };
                if (model.OriginId > 0)
                {
                    origin.Update(ref sysMessageId);
                }
                else
                {
                    origin.Insert(ref sysMessageId);
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

        public ActionResult Delete(short originId = 0)
        {
            if (originId > 0)
            {
                short sysMessageId = 0;
                new Origin
                {
                    OriginId = originId
                }.Delete(ref sysMessageId);
            }
            return Redirect("/Origin/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(OriginModel model)
        {
            //ToDo xóa
            if (model.SubmitType.Equals("deleteItems"))
            {
                if (model.OriginsId != null && model.OriginsId.Length > 0)
                {
                    short systemMessageId = 0;
                    foreach (var originId in model.OriginsId)
                    {
                        new Origin
                        {
                            OriginId = originId
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
            return Redirect("/Origin/Index");
        }
    }
}