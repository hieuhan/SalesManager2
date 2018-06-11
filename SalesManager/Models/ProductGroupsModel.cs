using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class ProductGroupsModel:ViewModelBase
    {
        public short[] ProductGroupsId { get; set; }
        public string ProductGroupsName { get; set; }
        public string SubmitType { get; set; }
        public List<ProductGroups> ListProductGroups { get; set; }

        /// <summary>
        /// Danh sách thứ tự hiển thị
        /// </summary>
        public List<ProductGroupsDisplayOrders> DisplayOrders { get; set; }
    }

    public class ProductGroupsEditModel : ViewModelBase
    {
        public short ProductGroupId { get; set; }

        [Display(Name = "Nhóm sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string ProductGroupName { get; set; }
        public string ProductGroupDesc { get; set; }
        public short DisplayOrder { get; set; }
    }

    public class ProductGroupsDisplayOrders
    {
        public short ProductGroupId { get; set; }
        public short DisplayOrder { get; set; }
    }
}