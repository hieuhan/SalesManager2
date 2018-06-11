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
    public class UsersController : Controller
    {
        private int _userId = SessionHelpers.UserId;

        // GET: Users
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            if (Request.IsAuthenticated)
            {
                return Redirect("/Home/Index");
            }
            var model = new UserLoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                string urlRedirect = "/Users/ChangePassword";
                var result = new Users().Login(model.UserName, md5.MD5Hash(model.Password));
                if (result.ActionStatus.Equals("OK"))
                {
                    SessionHelpers.UserId = result.User.UserId;
                    SessionHelpers.UserName = result.User.UserName;
                    SessionHelpers.DefaultAction = result.User.DefaultActionId;

                    //ToDo ghi logs
                    //new UserLogs { UserName = result.User.UserName, StatusId = result.User.UserStatusId, CrDateTime = result.User.CrDateTime, IPAddress = Request.UserHostAddress, UserAgent = Request.UserAgent }.InsertQuick();

                    if (Url.IsLocalUrl(model.ReturnUrl) && model.ReturnUrl.Length > 1 &&
                        model.ReturnUrl.StartsWith("/") && !model.ReturnUrl.StartsWith("//") &&
                        !model.ReturnUrl.StartsWith("/\\"))
                    {
                        urlRedirect = model.ReturnUrl;
                    }
                    else
                    {
                        if (result.User.DefaultActionId > 0)
                        {
                            var action = new Actions().Get(result.User.DefaultActionId);
                            if (action.ActionId > 0 && !string.IsNullOrEmpty(action.Url))
                            {
                                urlRedirect = action.Url.StartsWith(SalesManagerConstants.ROOT_PATH)
                                    ? action.Url
                                    : string.Concat(SalesManagerConstants.ROOT_PATH, action.Url);
                            }
                        }
                    }

                    return Redirect(urlRedirect);
                }
                ModelState.AddModelError(string.Empty, result.ActionMessage);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return Redirect("/Users/Login");
        }

        [HttpGet]
        [SalesManagerAuthorize]
        public ActionResult Create()
        {
            return View(new UserCreateModel());
        }

        [HttpPost]
        [SalesManagerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                byte sysMessageTypeId = 0;
                model.SystemStatus = SystemStatus.Error;
                var user = new Users
                {
                    UserId = model.UserId,
                    UserName = model.UserName,
                    Password = md5.MD5Hash(model.Password),
                    FullName = model.FullName,
                    Email = model.Email,
                    Address = model.Address,
                    Mobile = model.Mobile,
                    BirthDay = model.BirthDay,
                    GenderId = model.GenderId,
                    UserTypeId = model.UserTypeId,
                    UserStatusId = model.UserStatusId,
                    DefaultActionId = model.DefaultActionId,
                };
                sysMessageTypeId = user.Insert(0, ref sysMessageId);
                if (sysMessageId > 0)
                {
                    var sysMessage = new SystemMessages().Get(sysMessageId);
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

        [SalesManagerAuthorize]
        public ActionResult Index(byte userTypeId = 0, byte userStatusId = 0, string userName = "", int page = 1)
        {
            int rowCount = 0;
            string fullName = string.Empty,
                address = string.Empty,
                email = string.Empty,
                mobile = string.Empty, dateFrom = string.Empty, dateTo = string.Empty, orderBy = string.Empty;
            byte genderId = 0;

            var model = new UsersModel
            {
                ListUsers = new Users().GetPage(userName, fullName, address, email, mobile, genderId, userStatusId, userTypeId, dateFrom, dateTo, orderBy, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        [SalesManagerAuthorize]
        public ActionResult Edit(int userId = 0)
        {
            var model = new UserEditModel();
            if (userId > 0)
            {
                var user = new Users().Get(userId);
                if (user.UserId > 0)
                {
                    model.UserId = user.UserId;
                    model.UserName = user.UserName;
                    model.FullName = user.FullName;
                    model.Email = user.Email;
                    model.Mobile = user.Mobile;
                    model.Address = user.Address;
                    model.GenderId = user.GenderId;
                    model.UserTypeId = user.UserTypeId;
                    model.UserStatusId = user.UserStatusId;
                    model.DefaultActionId = user.DefaultActionId;
                    model.BirthDay = user.BirthDay;
                    model.ActionName = new Actions().Get(user.DefaultActionId).ActionName.TrimmedOrDefault(string.Empty);
                }
            }
            return View(model);
        }

        [HttpPost]
        [SalesManagerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var user = new Users
                {
                    UserId = model.UserId,
                    UserName = model.UserName,
                    Password = md5.MD5Hash(model.Password),
                    FullName = model.FullName,
                    Email = model.Email,
                    Address = model.Address,
                    Mobile = model.Mobile,
                    BirthDay = model.BirthDay,
                    GenderId = model.GenderId,
                    UserTypeId = model.UserTypeId,
                    UserStatusId = model.UserStatusId,
                    DefaultActionId = model.DefaultActionId
                };
                if (model.UserId > 0)
                {
                    user.Update(0, ref sysMessageId);
                }
                else
                {
                    user.Insert(0, ref sysMessageId);
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

        [HttpGet]
        [SalesManagerAuthorize]
        public ActionResult Delete(int userId = 0)
        {
            if (userId > 0)
            {
                short sysMessageId = 0;
                new Users { UserId = userId }.Delete(0, ref sysMessageId);
            }
            return Redirect("/Users/Index");
        }

        [HttpPost]
        [SalesManagerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(int[] UserIds, string SubmitType ="")
        {
            if (!string.IsNullOrEmpty(SubmitType) && SubmitType.Equals("deleteItems"))
            {
                if (UserIds != null && UserIds.Length > 0)
                {
                    short sysMessageId = 0;
                    foreach (var userId in UserIds)
                    {
                        new Users { UserId = userId }.Delete(0, ref sysMessageId);
                    }
                }
            }
            return Redirect("/Users/Index");
        }

        [SalesManagerAuthorize]
        public ActionResult UserRoles(int userId)
        {
            var model = new UserRolesModel
            {
                UserId = userId,
                ListUserRoles = new UserRoles().GetListByUserId(userId)
            };
            return View(model);
        }

        [HttpPost]
        [SalesManagerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult UserRoles(UserRolesModel model)
        {
            var userRoles = new UserRoles
            {
                UserId = model.UserId
            };
            userRoles.DeleteQuickBy(_userId);
            if (model.RolesId != null && model.RolesId.Length > 0)
            {
                foreach (var item in model.RolesId)
                {
                    userRoles.RoleId = item;
                    userRoles.InsertQuick(_userId);
                }
            }
            model.ListUserRoles = new UserRoles().GetListByUserId(model.UserId);
            model.SystemStatus = SystemStatus.Success;
            ModelState.AddModelError("SystemMessages", "Gán nhóm chức năng cho User thành công !");
            return View(model);
        }

        [SalesManagerAuthorize]
        public ActionResult UserActions(int userId, int page = 1)
        {
            int rowCount = 0;
            var model = new UserActionsModel
            {
                UserId = userId,
                ListActions = new Actions { ActionStatusId = 1 }.GetPage("","","",SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
                ListUserActions = new UserActions().GetByUser(userId),
                RowCount = rowCount,
                Pagination = new PaginationModel
                {
                    TotalPage = rowCount,
                    PageSize = SalesManagerConstants.RowAmount20,
                    LinkLimit = 5,
                    PageIndex = page
                }
            };
            var tempDataKeys = TempData.Get<TempDataKeys>(TempDataKeys.KeyName);
            if(tempDataKeys != null)
            {
                model.SystemStatus = tempDataKeys.SystemStatus;
                ModelState.AddModelError("SystemMessages", tempDataKeys.Message);
            }
            return View(model);
        }

        [HttpPost]
        [SalesManagerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult UserActions(UserActionsModel model)
        {
            var userAction = new UserActions
            {
                UserId = model.UserId
            };
            userAction.DeleteQuickBy();
            if (model.ActionsId != null && model.ActionsId.Length > 0)
            {
                foreach (var actionId in model.ActionsId)
                {
                    userAction.ActionId = actionId;
                    userAction.InsertQuick();
                }
            }
            var tempDataKeys = new TempDataKeys
            {
                Message = "Gán Chức năng cho User thành công !",
                SystemStatus = SystemStatus.Success
            };
            TempData.Put(TempDataKeys.KeyName, tempDataKeys);
            return Redirect(string.Concat("/Users/UserActions?userId=", model.UserId));
        }

    }
}