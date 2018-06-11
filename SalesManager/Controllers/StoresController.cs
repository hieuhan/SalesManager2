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
    public class StoresController : Controller
    {
        // GET: Stores
        public ActionResult Index()
        {
            int rowCount = 0;
            var model = new StoresModel
            {
                ListStores = new Stores().GetPage("", "", "", 1, 0, ref rowCount),
                RowCount = rowCount,
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(byte storeId = 0)
        {
            var model = new StoreEditModel();
            if(storeId > 0)
            {
                var store = new Stores { StoreId=storeId }.Get();
                if(store.StoreId > 0)
                {
                    model.StoreId = store.StoreId;
                    model.StoreName = store.StoreName;
                    model.StoreDesc = store.StoreDesc;
                    model.Address = store.Address;
                    model.Mobile = store.Mobile;
                    model.Email = store.Email;
                    model.Website = store.Website;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreEditModel model)
        {
            if (ModelState.IsValid)
            {
                short sysMessageId = 0;
                var store = new Stores
                {
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    StoreDesc = model.StoreDesc,
                    Address = model.Address,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    Website = model.Website
                };
                if (model.StoreId > 0)
                {
                    store.Update(ref sysMessageId);
                    if (sysMessageId > 0)
                    {
                        var sysMessage = new SystemMessages().Get(sysMessageId);
                        ModelState.AddModelError("SystemMessages", sysMessage.SystemMessageDesc);
                    }
                    else ModelState.AddModelError("SystemMessages", "Bạn vui lòng thử lại sau.");
                }
                else
                {
                    ModelState.AddModelError("SystemMessages", "Thông tin cửa hàng đã có, bạn không thể thêm mới bản ghi !");
                }
            }
            return View(model);
        }
    }
}