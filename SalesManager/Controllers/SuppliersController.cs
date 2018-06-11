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
    public class SuppliersController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;

        // GET: Supplier
        public ActionResult Index(string orderBy = "", byte reviewStatusId = 0, string supplierName = "", int page = 1)
        {
            int rowCount = 0;
            var model = new SupplierModel
            {
                ListSuppliers = new Suppliers
                {
                    SupplierName = supplierName,
                    ReviewStatusId = reviewStatusId
                }.GetPage(string.Empty, string.Empty, orderBy, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(int supplierId = 0)
        {
            var model = new SupplierEditModel();
            if (supplierId > 0)
            {
                var supplier = new Suppliers { SupplierId = supplierId }.Get();
                if (supplier.SupplierId > 0)
                {
                    model.SupplierId = supplier.SupplierId;
                    model.SupplierName = supplier.SupplierName;
                    model.Address = supplier.Address;
                    model.Mobile = supplier.Mobile;
                    model.Email = supplier.Email;
                    model.Contact = supplier.Contact;
                    model.DebitBalance = supplier.DebitBalance.CurrencyToString();
                    model.Note = supplier.Note;
                    model.DisplayOrder = supplier.DisplayOrder;
                    model.ReviewStatusId = supplier.ReviewStatusId;
                    model.CrDateTime = supplier.CrDateTime;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var supplier = new Suppliers
                {
                    SupplierId = model.SupplierId,
                    SupplierName = model.SupplierName,
                    Address = model.Address,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    Contact = model.Contact,
                    DebitBalance = Double.Parse(model.DebitBalance, System.Globalization.NumberStyles.Currency),
                    Note = model.Note,
                    DisplayOrder = model.DisplayOrder,
                    ReviewStatusId = model.ReviewStatusId
            };
                if (model.SupplierId > 0)
                {
                    supplier.Update(0, _userId, ref sysMessageId);
                }
                else
                {
                    supplier.Insert(0, _userId, ref sysMessageId);
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
        public ActionResult MultipleAction(int[] SupplierIds, string submitType)
        {
            if(!string.IsNullOrEmpty(submitType))
            {
                short sysMessageId = 0;
                if (submitType.Equals("deleteItems"))
                {
                    foreach (var supplierId in SupplierIds)
                    {
                        new Suppliers
                        {
                            SupplierId = supplierId,
                        }.Delete(0, 0, ref sysMessageId);
                    }
                }
            }
            return Redirect("/Suppliers/Index");
        }

        public ActionResult Delete(int supplierId = 0)
        {
            if (supplierId > 0)
            {
                short systemMessageId = 0;
                new Suppliers
                {
                    SupplierId = supplierId
                }.Delete(0, 0, ref systemMessageId);
            }
            return Redirect("/Suppliers/Index");
        }

    }
}