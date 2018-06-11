using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class ViewModelBase
    {
        public string AddOrders { get; set; }
        public string Delete { get; set; }

        public string Active { get; set; }

        public string DeActive { get; set; }

        public PaginationModel Pagination { get; set; }

        public int RowCount { get; set; }

        public SystemStatus SystemStatus { get; set; }

        public string SystemMessages { get; set; }

        #region Exception
        public Exception InnerException { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ErrorMessage { get; set; }
        public int HttpStatusCode { get; set; }
        #endregion
    }

    public enum SystemStatus
    {
        Success,
        Error,
        Unknow
    }

    public class TempDataKeys
    {
        public const string KeyName = "TempDataKeys";
        public string Message { get; set; }
        public SystemStatus SystemStatus { get; set; }
    }
}