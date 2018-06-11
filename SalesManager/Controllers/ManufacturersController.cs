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
    public class ManufacturersController : Controller
    {
        // GET: Manufacturers
        public ActionResult Index(string manufacturerName = "", int page = 1)
        {
            int rowCount = 0;
            var model = new ManufacturersModel
            {
                ListManufacturers = new Manufacturers { ManufacturerName = manufacturerName }.GetPage(string.Empty, string.Empty, string.Empty, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(short manufacturerId = 0)
        {
            var model = new ManufacturerEditModel();
            if (manufacturerId > 0)
            {
                var manufacturer = new Manufacturers { ManufacturerId = manufacturerId }.Get();
                if (manufacturer.ManufacturerId > 0)
                {
                    model.ManufacturerId = manufacturer.ManufacturerId;
                    model.ManufacturerName = manufacturer.ManufacturerName;
                    model.ManufacturerDesc = manufacturer.ManufacturerDesc;
                    model.DisplayOrder = manufacturer.DisplayOrder;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManufacturerEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var manufacturer = new Manufacturers
                {
                    ManufacturerId = model.ManufacturerId,
                    ManufacturerName = model.ManufacturerName,
                    ManufacturerDesc = model.ManufacturerDesc,
                    DisplayOrder = model.DisplayOrder
                };
                if (model.ManufacturerId > 0)
                {
                    manufacturer.Update(0, 0, ref sysMessageId);
                }
                else
                {
                    manufacturer.Insert(0, 0, ref sysMessageId);
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

        public ActionResult Delete(short manufacturerId = 0)
        {
            if (manufacturerId > 0)
            {
                short sysMessageId = 0;
                new Manufacturers
                {
                    ManufacturerId = manufacturerId
                }.Delete(0, 0, ref sysMessageId);
            }
            return Redirect("/Manufacturers/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(int[] ManufacturersId, string SubmitType)
        {
            if (!string.IsNullOrEmpty(SubmitType) && SubmitType.Equals("deleteItems"))
            {
                if (ManufacturersId != null && ManufacturersId.Length > 0)
                {
                    short sysMessageId = 0;
                    foreach (var manufacturerId in ManufacturersId)
                    {
                        new Manufacturers
                        {
                            ManufacturerId = manufacturerId
                        }.Delete(0, 0, ref sysMessageId);
                    }
                }
            }
            return Redirect("/Manufacturers/Index");
        }


    }
}