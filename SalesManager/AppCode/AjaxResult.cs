using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SalesManager.AppCode
{
    public class AjaxResult : ActionResult
    {
        public AjaxResult()
        {
            this.AllowGet = false;
            this.StatusCode = 200;
            this.ContentEncoding = Encoding.UTF8;
        }

        public int StatusCode { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Trạng thái trả về
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Thông báo
        /// </summary>
        public string Message { get; set; }

        public bool AllowGet { get; set; }

        /// <summary>
        /// Url trả về
        /// </summary>
        public string ReturnUrl { get; set; }

        public string ContentType { get; set; }

        public Encoding ContentEncoding { get; set; }

        public int? MaxJsonLength { get; set; }

        public int? RecursionLimit { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context is null");
            if (!this.AllowGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Yêu cầu Get dữ liệu không được cho phép.");

            HttpResponseBase response = context.HttpContext.Response;
            response.Clear();
            response.StatusCode = this.StatusCode;
            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            var responseData = new
            {
                Completed = this.Completed,
                Message = this.Message,
                Data = this.Data,
                ReturnUrl = this.ReturnUrl
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (this.MaxJsonLength.HasValue)
            {
                serializer.MaxJsonLength = this.MaxJsonLength.Value;
            }
            if (this.RecursionLimit.HasValue)
            {
                serializer.RecursionLimit = this.RecursionLimit.Value;
            }
            response.Write(200 != response.StatusCode ? "{}" : serializer.Serialize(responseData));
        }
    }
}