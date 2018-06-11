using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class SharedModel
    {
        public class HeaderModel
        {
            public List<Actions> ListActionForUser { get; set; }
        }
    }
}