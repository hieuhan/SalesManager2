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
    public class CustomerGroupsController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;

        // GET: CustomerGroups
        public ActionResult Index(string customerGroupName = "", int page = 1)
        {
            int rowCount = 0;
            var model = new CustomerGroupModel
            {
                ListUsers = new Users().GetAll(),
                ListCustomerGroups = new CustomerGroups { CustomerGroupName = customerGroupName }.GetPage(string.Empty, string.Empty, string.Empty, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(short customerGroupId = 0)
        {
            var model = new CustomerGroupEditModel();
            if (customerGroupId > 0)
            {
                var customerGroup = new CustomerGroups().Get(customerGroupId);
                if (customerGroup.CustomerGroupId > 0)
                {
                    model.CustomerGroupId = customerGroup.CustomerGroupId;
                    model.CustomerGroupName = customerGroup.CustomerGroupName;
                    model.CustomerGroupDesc = customerGroup.CustomerGroupDesc;
                    model.CrUserId = customerGroup.CrUserId;
                    model.CrDateTime = customerGroup.CrDateTime;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerGroupEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var customerGroup = new CustomerGroups
                {
                    CustomerGroupId = model.CustomerGroupId,
                    CustomerGroupName = model.CustomerGroupName,
                    CustomerGroupDesc = model.CustomerGroupDesc
                };
                if (model.CustomerGroupId > 0)
                {
                    customerGroup.Update(0, _userId, ref sysMessageId);
                }
                else
                {
                    customerGroup.Insert(0, _userId, ref sysMessageId);
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

        public ActionResult Delete(short customerGroupId = 0)
        {
            if (customerGroupId > 0)
            {
                short cmsMessageId = 0;
                new CustomerGroups
                {
                    CustomerGroupId = customerGroupId
                }.Delete(0, 0, ref cmsMessageId);
            }
            return Redirect("/CustomerGroups/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(short[] CustomerGroupIds, string SubmitType)
        {
            if (!string.IsNullOrEmpty(SubmitType) && SubmitType.Equals("deleteItems"))
            {
                if (CustomerGroupIds != null && CustomerGroupIds.Length > 0)
                {
                    short sysMessageId = 0;
                    foreach (var customerGroupId in CustomerGroupIds)
                    {
                        new CustomerGroups
                        {
                            CustomerGroupId = customerGroupId
                        }.Delete(0, 0, ref sysMessageId);
                    }
                }
            }
            return Redirect("/CustomerGroups/Index");
        }

    }
}