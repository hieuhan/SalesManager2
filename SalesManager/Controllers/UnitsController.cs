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
    public class UnitsController : Controller
    {
        // GET: Units
        public ActionResult Index(string unitName = "", int page = 1)
        {
            int rowCount = 0;
            var model = new UnitsModel
            {
                ListUnits = new Units { UnitName = unitName }.GetPage(string.Empty, string.Empty, string.Empty, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(short unitId = 0)
        {
            var model = new UnitEditModel();
            if (unitId > 0)
            {
                var unit = new Units { UnitId = unitId }.Get();
                if (unit.UnitId > 0)
                {
                    model.UnitId = unit.UnitId;
                    model.UnitName = unit.UnitName;
                    model.UnitDesc = unit.UnitDesc;
                    model.CrDateTime = unit.CrDateTime;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UnitEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var unit = new Units
                {
                    UnitId = model.UnitId,
                    UnitName = model.UnitName,
                    UnitDesc = model.UnitDesc,
                    CrDateTime = model.CrDateTime,
                };
                if (model.UnitId > 0)
                {
                    unit.Update(ref sysMessageId);
                }
                else
                {
                    unit.Insert(ref sysMessageId);
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

        public ActionResult Delete(short unitId = 0)
        {
            if (unitId > 0)
            {
                short sysMessageId = 0;
                new Units
                {
                    UnitId = unitId
                }.Delete(ref sysMessageId);
            }
            return Redirect("/Units/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(short[] UnitsId, string SubmitType = "")
        {
            if (!string.IsNullOrEmpty(SubmitType) && SubmitType.Equals("deleteItems"))
            {
                if (UnitsId != null && UnitsId.Length > 0)
                {
                    short sysMessageId = 0;
                    foreach (var unitId in UnitsId)
                    {
                        new Units
                        {
                            UnitId = unitId
                        }.Delete(ref sysMessageId);
                    }
                }
            }
            return Redirect("/Units/Index");
        }
    }
}