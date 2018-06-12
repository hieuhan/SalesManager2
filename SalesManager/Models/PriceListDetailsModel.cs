using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class PriceListDetailsModel:ViewModelBase
    {
        public int PriceListId { get; set; }
        public string SubmitType { get; set; }
        public List<Units> ListUnits { get; set; }
        public List<Users> ListUsers { get; set; }
        public List<Status> ListStatus { get; set; }
        public List<PriceListDetails> ListPriceListDetails { get; set; }

        public List<PriceListDetailsDisplayOrders> PriceListDetailsDisplayOrders { get; set; }
    }

    public class PriceListDetailsDisplayOrders
    {
        public int PriceListDetailId { get; set; }

        private string _Price;
        public string Price
        {
            get { return string.IsNullOrEmpty(_Price) ? "0" : _Price; }
            set { _Price = value; }
        }
    }
}