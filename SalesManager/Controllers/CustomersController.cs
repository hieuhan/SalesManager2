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
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index(short customerGroupId = 0, string fullName = "", string mobile = "", string dateFrom = "", string dateTo = "", string orderBy = "", int page = 1)
        {
            int rowCount = 0;
            var model = new CustomersModel
            {
                ListCustomers = new Customers
                {
                    CustomerGroupId = customerGroupId,
                    FullName = fullName,
                    Mobile = mobile
                }.GetPage(dateFrom, dateTo, orderBy, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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

        public ActionResult Edit(int customerId = 0)
        {
            var model = new CustomerEditModel
            {
                ListCustomerGroups = CustomerGroups.Static_GetListAll(),
                ListGenders = Genders.Static_GetList()
            };
            if (customerId > 0)
            {
                var customer = new Customers { CustomerId = customerId }.Get();
                if (customer.CustomerId > 0)
                {
                    model.CustomerId = customer.CustomerId;
                    model.CustomerGroupId = customer.CustomerGroupId;
                    model.FullName = customer.FullName.TrimmedOrDefault(string.Empty);
                    model.Address = customer.Address.TrimmedOrDefault(string.Empty);
                    model.Mobile = customer.Mobile.TrimmedOrDefault(string.Empty);
                    model.Email = customer.Email.TrimmedOrDefault(string.Empty);
                    model.Note = customer.Note.TrimmedOrDefault(string.Empty);
                    model.GenderId = customer.GenderId;
                    model.StatusId = customer.StatusId;
                    model.DebitBalance = customer.DebitBalance.CurrencyToString();
                    model.PaymentLimit = customer.PaymentLimit.CurrencyToString();
                    model.DisplayOrder = customer.DisplayOrder;
                    model.DateOfBirth = customer.DateOfBirth;
                    model.CrDateTime = customer.CrDateTime;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEditModel model)
        {
            model.ListCustomerGroups = CustomerGroups.Static_GetListAll();
            model.ListGenders = Genders.Static_GetList();
            short sysMessageId = 0;
            if (ModelState.IsValid)
            {
                var customer = new Customers
                {
                    CustomerId = model.CustomerId,
                    CustomerGroupId = model.CustomerGroupId,
                    FullName = model.FullName,
                    Address = model.Address,
                    Email = model.Email,
                    Note = model.Note,
                    Mobile = model.Mobile,
                    GenderId = model.GenderId,
                    StatusId = model.StatusId,
                    DebitBalance = Double.Parse(model.DebitBalance, System.Globalization.NumberStyles.Currency),
                    PaymentLimit = Double.Parse(model.PaymentLimit, System.Globalization.NumberStyles.Currency),
                    DisplayOrder = model.DisplayOrder,
                    DateOfBirth = model.DateOfBirth
                };
                customer.InsertOrUpdate(0,0,ref sysMessageId);

                if (sysMessageId > 0)
                {
                    var sysMessage = new SystemMessages().Get(sysMessageId);
                    ModelState.AddModelError("SystemMessages", sysMessage.SystemMessageDesc);
                }
                else ModelState.AddModelError("SystemMessages", "Bạn vui lòng thử lại sau.");
            }
            return View(model);
        }

        public ActionResult Delete(int customerId = 0)
        {
            if (customerId > 0)
            {
                short sysMessageId = 0;
                new Customers { CustomerId = customerId }.Delete(0,0,ref sysMessageId);
            }
            return Redirect("/Customers/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(CustomersModel model)
        {
            //ToDo xóa
            if (!string.IsNullOrEmpty(model.SubmitType) && model.SubmitType.Equals("deleteItems"))
            {
                if (model.CustomersId != null && model.CustomersId.Length > 0)
                {
                    short sysMessageId = 0;
                    foreach (var customerId in model.CustomersId)
                    {
                        new Customers
                        {
                            CustomerId = customerId
                        }.Delete(0, 0, ref sysMessageId);
                    }
                }
            }
            //ToDo cập nhật thứ tự hiển thị
            else if (!string.IsNullOrEmpty(model.SubmitType) && model.SubmitType.Equals("updateOrders"))
            {
                if (model.CustomerDisplayOrders.HasValue())
                {
                    foreach (var item in model.CustomerDisplayOrders)
                    {
                        new Customers
                        {
                            CustomerId = item.CustomerId,
                            DisplayOrder = item.DisplayOrder
                        }.UpdateDisplayOrder();
                    }
                }
            }
            return Redirect("/Customers/Index");
        }

    }
}