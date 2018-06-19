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
    public class AjaxController : Controller
    {
        // GET: Ajax
        private readonly int _userId = SessionHelpers.UserId;

        [HttpPost]
        [SalesManagerAuthorize]
        public AjaxResult MediaSelect(int mediaId = 0)
        {
            string mediaPath = string.Empty;
            if (mediaId > 0)
            {
                var media = new Medias { MediaId = mediaId }.Get();

                if (media.MediaId > 0)
                {
                    mediaPath = media.FilePath;
                }
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = mediaPath,
                Completed = !string.IsNullOrEmpty(mediaPath)
            };
        }

        [HttpPost]
        [SalesManagerAuthorize]
        public AjaxResult ActionSelect(short actionId = 0)
        {
            Actions action = null;
            if (actionId > 0)
            {
                action = new Actions { ActionId = actionId }.Get(actionId);
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Data = action,
                Completed = action.ActionId > 0
            };
        }

        public AjaxResult SelectPriceList(int priceListId = 0, int priceListIdClone = 0)
        {
            if(priceListId > 0)
            {
                new PriceListDetails { PriceListId = priceListId, CrUserId = _userId }.Clone_Auto(priceListIdClone);
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Sao chép bảng giá thành công !",
                Completed = true
            };
        }
    }
}