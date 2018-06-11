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
    public class RolesController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;

        // GET: Roles
        public ActionResult Index()
        {
            var model = new Roles().GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(short roleId = 0)
        {
            var model = new RolesModel();
            if (roleId > 0)
            {
                var action = new Roles().Get(roleId);
                if (action.RoleId > 0)
                {
                    model.RoleId = action.RoleId;
                    model.RoleName = action.RoleName;
                    model.RoleDesc = action.RoleDesc;
                    model.BuildIn = 0;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RolesModel model)
        {
            if (ModelState.IsValid)
            {
                short systemMessageId = 0;
                byte sysMessageTypeId = 0;
                model.SystemStatus = SystemStatus.Error;
                var role = new Roles
                {
                    RoleId = model.RoleId,
                    RoleName = model.RoleName,
                    RoleDesc = model.RoleDesc,
                    BuildIn = 0
                };
                sysMessageTypeId = model.RoleId > 0 ? role.Update(_userId, ref systemMessageId) : role.Insert(_userId, ref systemMessageId);
                if (systemMessageId > 0)
                {
                    var sysMessage = new SystemMessages().Get(systemMessageId);
                    if (sysMessageTypeId == SalesManagerConstants.SystemMessageIdSuccess)
                    {
                        model.SystemStatus = SystemStatus.Success;
                    }
                    ModelState.AddModelError("SystemMessages", sysMessage.SystemMessageDesc);
                }
                else ModelState.AddModelError("SystemMessages", "Bạn vui lòng thử lại sau.");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(short roleId = 0)
        {
            short sysMessageId = 0;
            if (roleId > 0)
            {
                new Roles { RoleId = roleId }.Delete(0, ref sysMessageId);
            }
            return Redirect("/Roles/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(short[] RoleIds, string SubmitType = "")
        {
            if (!string.IsNullOrEmpty(SubmitType) && SubmitType.Equals("deleteItems"))
            {
                if (RoleIds != null && RoleIds.Length > 0)
                {
                    short systemMessageId = 0;
                    foreach (var roleId in RoleIds)
                    {
                        new Roles
                        {
                            RoleId = roleId
                        }.Delete(0, ref systemMessageId);
                    }
                }
            }
            return Redirect("/Roles/Index");
        }

        [HttpGet]
        public ActionResult RoleActions(short roleId, int page = 1)
        {
            int rowCount = 0;
            var model = new RoleActionsModel
            {
                RoleId = roleId,
                ListActions = new Actions { ActionStatusId = 1 }.GetPage("", "", "", SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
                ListActionsByRole = new Actions().GetActionsByRole(roleId),
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleActions(RoleActionsModel model)
        {
            var roleActions = new RoleActions
            {
                RoleId = model.RoleId
            };
            roleActions.DeleteQuickBy();
            if (model.ActionsId != null && model.ActionsId.Length > 0)
            {
                foreach (var item in model.ActionsId)
                {
                    roleActions.ActionId = item;
                    roleActions.InsertQuick(0);
                }
            }
            model.ListActionsByRole = new Actions().GetActionsByRole(model.RoleId);
            model.SystemStatus = SystemStatus.Success;
            ModelState.AddModelError("SystemMessages", "Gán chức năng cho quyền thành công !");
            return View(model);
        }

    }
}