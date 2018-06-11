using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class ProductTypesModel:ViewModelBase
    {
        public short[] ProductTypesId { get; set; }
        public string ProductTypeName { get; set; }
        public string SubmitType { get; set; }
        public List<ProductTypes> ListProductTypes { get; set; }

        /// <summary>
        /// Danh sách thứ tự hiển thị
        /// </summary>
        public List<ProductTypesDisplayOrders> DisplayOrders { get; set; }
    }

    public class ProductTypesEditModel : ViewModelBase
    {
        public short ProductTypeId { get; set; }

        [Display(Name = "Loại sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string ProductTypeName { get; set; }
        public string ProductTypeDesc { get; set; }
        public short DisplayOrder { get; set; }
    }

    public class ProductTypesDisplayOrders
    {
        public short ProductTypeId { get; set; }
        public short DisplayOrder { get; set; }
    }
}