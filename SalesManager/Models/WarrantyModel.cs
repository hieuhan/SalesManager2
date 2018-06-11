using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class WarrantyModel:ViewModelBase
    {
        public short[] WarrantysId { get; set; }
        public string WarrantyName { get; set; }
        public string SubmitType { get; set; }
        public List<Warranty> ListWarranty { get; set; }

        /// <summary>
        /// Danh sách thứ tự hiển thị
        /// </summary>
        public List<WarrantyDisplayOrders> DisplayOrders { get; set; }
    }

    public class WarrantyEditModel:ViewModelBase
    {
        public short WarrantyId { get; set; }
        public string WarrantyName { get; set; }
        public string WarrantyDesc { get; set; }
        public short DisplayOrder { get; set; }
    }

    public class WarrantyDisplayOrders
    {
        public short WarrantyId { get; set; }
        public short DisplayOrder { get; set; }
    }
}