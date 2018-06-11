using SalesManager.AppCode;
using SalesManager.AppCode.Attributes;
using SalesManager.Models;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SalesManagerLib.Helpers;

namespace SalesManager.Controllers
{
    [SalesManagerAuthorize]
    public class MediasController : Controller
    {
        private readonly int _userId = SessionHelpers.UserId;

        // GET: Medias
        public ActionResult Index(short mediaGroupId = 0, byte mediaTypeId = 0, string mediaName = "", int page = 1)
        {
            int rowCount = 0;
            var model = new MediasModel
            {
                ListMediaGroups = new MediaGroups().GetList(),
                ListDataTypes = MediaTypes.Static_GetList(),
                ListUsers = new Users().GetAll(),
                ListMedias = new Medias().GetPage(0, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, "", mediaTypeId, mediaGroupId, mediaName, "", "", ref rowCount),
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
        public ActionResult UploadFiles(MediasModel model)
        {
            short sysMessageId = 0;
            if (model.FileMedias != null && model.FileMedias.Count > 0)
            {
                foreach (var file in model.FileMedias)
                {
                    if (file != null)
                    {
                        string rootDir = Request.PhysicalApplicationPath;
                        string filePath = FileUploadHelpers.SaveFile(file, rootDir, SalesManagerConstants.MEDIA_PATH, true);
                        new Medias
                        {
                            MediaName = file.FileName.Split('.')[0],
                            MediaDesc = file.FileName.Split('.')[0],
                            MediaGroupId = model.MediaGroupId,
                            MediaTypeId = model.MediaTypeId,
                            FilePath = filePath.Replace("\\", "/"),
                            FileSize = file.ContentLength
                        }.Insert(0, _userId, ref sysMessageId);
                    }
                }
            }
            return Redirect("/Medias/Index");
        }

        [HttpGet]
        public ActionResult Edit(int mediaId = 0)
        {
            var model = new MediasModel
            {
                ListMediaGroups = new MediaGroups().GetList(),
                ListDataTypes = MediaTypes.Static_GetList()
            };
            if (mediaId > 0)
            {
                var media = new Medias().Get(mediaId);
                if (media.MediaId > 0)
                {
                    model.MediaId = media.MediaId;
                    model.MediaGroupId = media.MediaGroupId;
                    model.MediaTypeId = media.MediaTypeId;
                    model.MediaName = media.MediaName;
                    model.MediaDesc = media.MediaDesc;
                    model.FilePath = media.FilePath;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MediasModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                model.ListMediaGroups = new MediaGroups().GetList();
                model.ListDataTypes = MediaTypes.Static_GetList();
                var media = new Medias
                {
                    MediaId = model.MediaId,
                    MediaGroupId = model.MediaGroupId,
                    MediaTypeId = model.MediaTypeId,
                    MediaName = model.MediaName,
                    MediaDesc = model.MediaDesc,
                    FilePath = model.FilePath
                };
                if (model.FileMedia != null)
                {
                    string rootDir = Request.PhysicalApplicationPath;
                    string filePath = FileUploadHelpers.SaveFile(model.FileMedia, rootDir, SalesManagerConstants.MEDIA_PATH, true);
                    string fileName = model.FileMedia.FileName.Split('.')[0];
                    media.FilePath = filePath.Replace("\\", "/");
                    media.MediaName = model.MediaName.TrimmedOrDefault(fileName);
                    media.MediaDesc = model.MediaDesc.TrimmedOrDefault(fileName);
                    media.FileSize = model.FileMedia.ContentLength;
                }
                if (model.MediaId > 0)
                {
                    media.Update(0, _userId, ref sysMessageId);
                }
                else
                {
                    media.Insert(0, _userId, ref sysMessageId);
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

        public ActionResult Delete(int mediaId = 0)
        {
            if (mediaId > 0)
            {
                short cmsMessageId = 0;
                new Medias
                {
                    MediaId = mediaId
                }.Delete(0, 0, ref cmsMessageId);
            }
            return Redirect("/Medias/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MultipleAction(int[] MediasId, string SubmitType)
        {
            if (!string.IsNullOrEmpty(SubmitType) && SubmitType.Equals("deleteItems"))
            {
                if (MediasId != null && MediasId.Length > 0)
                {
                    short cmsMessageId = 0;
                    foreach (var mediaId in MediasId)
                    {
                        new Medias
                        {
                            MediaId = mediaId
                        }.Delete(0, 0, ref cmsMessageId);
                    }
                }
            }
            return Redirect("/Medias/Index");
        }

        public ActionResult Select(short mediaGroupId = 0, byte mediaTypeId = 0, string mediaName = "", int page = 1)
        {
            int rowCount = 0;

            var model = new MediasModel
            {
                ListMediaGroups = new MediaGroups().GetList(),
                ListDataTypes = MediaTypes.Static_GetList(),
                ListUsers = new Users().GetAll(),
                ListMedias = new Medias().GetPage(0, SalesManagerConstants.RowAmount20, page > 0 ? page - 1 : page, "", mediaTypeId, mediaGroupId, mediaName, "", "", ref rowCount),
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
    }
}