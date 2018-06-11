using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class SystemErrorsModel:ViewModelBase
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public List<SystemErrors> ListSystemErrors { get; set; }
    }
}