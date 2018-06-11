using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class StoresModel:ViewModelBase
    {
        public List<Stores> ListStores { get; set; }
    }

    public class StoreEditModel : ViewModelBase
    {
        public byte StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreDesc { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }
}