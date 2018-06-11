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
    public class MediaGroupsController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;

        // GET: MediaGroups
        public ActionResult Index(string mediaGroupName = "", string orderBy = "", int page = 1)
        {
            int rowCount = 0;
            var model = new MediaGroupsModel
            {
                ListUsers = new Users().GetAll(),
                ListMediaGroups = new MediaGroups { MediaGroupName = mediaGroupName }.GetPage(string.Empty, string.Empty, orderBy, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, ref rowCount),
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
        public ActionResult Edit(short mediaGroupId = 0)
        {
            var model = new MediaGroupsEditModel();
            if (mediaGroupId > 0)
            {
                var mediaGroup = new MediaGroups().Get(mediaGroupId);
                if (mediaGroup.MediaGroupId > 0)
                {
                    model.MediaGroupId = mediaGroup.MediaGroupId;
                    model.MediaGroupName = mediaGroup.MediaGroupName;
                    model.MediaGroupDesc = mediaGroup.MediaGroupDesc;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MediaGroupsEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var mediaGroups = new MediaGroups
                {
                    MediaGroupId = model.MediaGroupId,
                    MediaGroupName = model.MediaGroupName,
                    MediaGroupDesc = model.MediaGroupDesc
                };
                if (model.MediaGroupId > 0)
                {
                    mediaGroups.Update(0, _userId, ref sysMessageId);
                }
                else
                {
                    mediaGroups.Insert(0, _userId, ref sysMessageId);
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

        public ActionResult Delete(short mediaGroupId = 0)
        {
            if (mediaGroupId > 0)
            {
                short cmsMessageId = 0;
                new MediaGroups
                {
                    MediaGroupId = mediaGroupId
                }.Delete(0, 0, ref cmsMessageId);
            }
            return Redirect("/MediaGroups/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(short[] MediaGroupsId, string SubmitType)
        {
            if (!string.IsNullOrEmpty(SubmitType) && SubmitType.Equals("deleteItems"))
            {
                if (MediaGroupsId != null && MediaGroupsId.Length > 0)
                {
                    short cmsMessageId = 0;
                    foreach (var mediaGroupId in MediaGroupsId)
                    {
                        new MediaGroups
                        {
                            MediaGroupId = mediaGroupId
                        }.Delete(0, 0, ref cmsMessageId);
                    }
                }
            }
            return Redirect("/MediaGroups/Index");
        }

    }
}