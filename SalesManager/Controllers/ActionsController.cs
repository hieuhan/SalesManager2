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
    public class ActionsController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;

        // GET: Actions
        public ActionResult Index(int page = 1, short parentActionId = 0, string actionName = "")
        {
            int rowCount = 0;
            var model = new ActionsModel
            {
                ListActions = new Actions{ActionName =  actionName, ParentActionId = parentActionId}.GetPage("","","",SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
                ListActionStatus = ActionStatus.Static_GetList(),
                ListParentActions = new Actions().GetRoots(),
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

        public ActionResult Select(int page = 1, short parentActionId = 0, string actionName = "")
        {
            int rowCount = 0;
            var model = new ActionsModel
            {
                ListActions = new Actions { ActionName = actionName, ParentActionId = parentActionId, Display = 1, ActionStatusId = 1 }.GetPage("", "", "", SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
                ListParentActions = new Actions().GetRoots(),
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
        public ActionResult Edit(short actionId = 0)
        {
            var model = new ActionEditModel();
            if (actionId > 0)
            {
                var action = new Actions().Get(actionId);
                if (action.ActionId > 0)
                {
                    model.ActionId = action.ActionId;
                    model.ActionName = action.ActionName;
                    model.ActionDesc = action.ActionDesc;
                    model.Url = action.Url;
                    model.ParentActionId = action.ParentActionId;
                    model.ActionStatusId = action.ActionStatusId;
                    model.ActionOrder = action.ActionOrder;
                    model.Display = action.Display == 1;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActionEditModel model)
        {
            if (ModelState.IsValid)
            {
                short systemMessageId = 0;
                byte sysMessageTypeId = 0;
                model.SystemStatus = SystemStatus.Error;
                var action = new Actions
                {
                    ActionId = model.ActionId,
                    ActionName = model.ActionName,
                    ActionDesc = model.ActionDesc,
                    Url = model.Url.TrimmedOrDefault(string.Empty),
                    ParentActionId = model.ParentActionId,
                    ActionOrder = model.ActionOrder,
                    Display = byte.Parse(model.Display ? "1" : "0"),
                    ActionStatusId = model.ActionStatusId
                };
                sysMessageTypeId = model.ActionId > 0 ? action.Update(0, _userId, ref systemMessageId) : action.Insert(0, _userId, ref systemMessageId);

                if (systemMessageId > 0)
                {
                    var systemMessage = new SystemMessages().Get(systemMessageId);
                    if (sysMessageTypeId == SalesManagerConstants.SystemMessageIdSuccess)
                    {
                        model.SystemStatus = SystemStatus.Success;
                    }
                    ModelState.AddModelError("SystemMessages", systemMessage.SystemMessageDesc);
                }
                else
                {
                    ModelState.AddModelError("SystemMessages", "Bạn vui lòng thử lại sau.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(short actionId = 0)
        {
            if (actionId > 0)
            {
                short systemMessageId = 0;
                new Actions
                {
                    ActionId = actionId
                }.Delete(0, 0, ref systemMessageId);
            }
            return Redirect("/Actions/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(ActionsModel model)
        {
            //ToDo xóa
            if (model.SubmitType.Equals("deleteItems"))
            {
                if (model.ActionsId != null && model.ActionsId.Length > 0)
                {
                    short systemMessageId = 0;
                    foreach (var actionId in model.ActionsId)
                    {
                        new Actions
                        {
                            ActionId = actionId
                        }.Delete(0, 0, ref systemMessageId);
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
                        new Actions
                        {
                            ActionId = item.ActionId,
                            ActionOrder = item.ActionOrder
                        }.UpdateDisplayOrder();
                    }
                }
            }
            return Redirect("/Actions/Index");
        }

        [HttpGet]
        public ActionResult ActionRoles(short actionId = 0)
        {
            var model = new ActionRolesModel
            {
                ActionId = actionId,
                ListRoles = new Roles().GetAll(),
                ListRoleActions = new RoleActions().GetByActionId(actionId)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActionRoles(ActionRolesModel model)
        {
            var roleAction = new RoleActions
            {
                ActionId = model.ActionId
            };
            roleAction.DeleteQuickBy();
            if (model.RolesId != null && model.RolesId.Length > 0)
            {
                foreach (var roleId in model.RolesId)
                {
                    roleAction.RoleId = roleId;
                    roleAction.InsertQuick(_userId);
                }
            }
            model.ListRoles = new Roles().GetAll();
            model.ListRoleActions = new RoleActions().GetByActionId(model.ActionId);
            model.SystemStatus = SystemStatus.Success;
            ModelState.AddModelError("SystemMessages", "Gán quyền cho Chức năng thành công !");
            return View(model);
        }

    }
}