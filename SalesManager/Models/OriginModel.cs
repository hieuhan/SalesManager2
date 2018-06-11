using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalesManagerLib;

namespace SalesManager.Models
{
    public class OriginModel:ViewModelBase
    {
        public short[] OriginsId { get; set; }
        public string OriginName { get; set; }
        public string SubmitType { get; set; }
        public List<Origin> ListOrigin { get; set; }

        /// <summary>
        /// Danh sách thứ tự hiển thị
        /// </summary>
        public List<WarrantyDisplayOrders> DisplayOrders { get; set; }
    }

    public class OriginEditModel : ViewModelBase
    {
        public short OriginId { get; set; }
        public string OriginName { get; set; }
        public string OriginDesc { get; set; }
        public short DisplayOrder { get; set; }
    }

    public class OriginDisplayOrders
    {
        public short OriginId { get; set; }
        public short DisplayOrder { get; set; }
    }
}