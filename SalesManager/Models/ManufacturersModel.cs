using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class ManufacturersModel: ViewModelBase
    {
        public int[] ManufacturersId { get; set; }
        public string ManufacturerName { get; set; }
        public List<Manufacturers> ListManufacturers { get; set; }
    }

    public class ManufacturerEditModel : ViewModelBase
    {
        public int ManufacturerId { get; set; }

        [Display(Name = "Tên nhóm khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string ManufacturerName { get; set; }
        public string ManufacturerDesc { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CrDateTime { get; set; }
    }
}