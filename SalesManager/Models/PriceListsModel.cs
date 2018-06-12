using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalesManagerLib;

namespace SalesManager.Models
{
    public class PriceListsModel:ViewModelBase
    {
        public short[] PriceListsId { get; set; }
        public string PriceListName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string OrderBy { get; set; }

        public byte StatusId { get; set; }
        public List<PriceLists> ListPriceLists { get; set; }
        public List<Status> ListStatus { get; set; }
        public List<Users> ListUsers { get; set; }
        public List<OrderByClauses> ListOrderByClauses { get; set; }

        /// <summary>
        /// Danh sách thứ tự hiển thị
        /// </summary>
        public List<PriceListsDisplayOrders> DisplayOrders { get; set; }
    }

    public class PriceListsEditModel:ViewModelBase
    {
        public int PriceListId { get; set; }
        public string PriceListName { get; set; }
        public string PriceListDesc { get; set; }
        public byte PriceListTypeId { get; set; }
        public byte IsDetail { get; set; }
        public byte StatusId { get; set; }
        public int DisplayOrder { get; set; }
        public List<Status> ListStatus { get; set; }
    }

    public class PriceListsDisplayOrders
    {
        public short PriceListId { get; set; }
        public short DisplayOrder { get; set; }
    }
}