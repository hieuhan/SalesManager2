using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class UnitsModel : ViewModelBase
    {
        public short[] UnitsId { get; set; }
        public string UnitName { get; set; }
        public List<Units> ListUnits { get; set; }
    }

    public class UnitEditModel : ViewModelBase
    {
        public short UnitId { get; set; }

        [Display(Name = "Tên đơn vị tính")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string UnitName { get; set; }
        public string UnitDesc { get; set; }
        public DateTime CrDateTime { get; set; }
    }
}