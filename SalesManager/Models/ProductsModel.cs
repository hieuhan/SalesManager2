using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class ProductsModel:ViewModelBase
    {
        public short[] ProductsId { get; set; }
        public string ProductName { get; set; }
        public int ManufacturerId { get; set; }
        public short UnitId { get; set; }
        public short ProductGroupId { get; set; }
        public short ProductTypeId { get; set; }
        public short OriginId { get; set; }
        public short WarrantyId { get; set; }
        public byte StatusId { get; set; }
        public int DisplayOrder { get; set; }
        public string SubmitType { get; set; }
        public List<Products> ListProducts { get; set; }
        public List<Manufacturers> ListManufacturers { get; set; }
        public List<ProductGroups> ListProductGroups { get; set; }
        public List<ProductTypes> ListProductTypes { get; set; }
        public List<Origin> ListOrigin { get; set; }
        public List<Warranty> ListWarranty { get; set; }
        public List<Units> ListUnits { get; set; }
        public List<Status> ListStatus { get; set; }
        /// <summary>
        /// Danh sách thứ tự hiển thị
        /// </summary>
        public List<ProductsDisplayOrders> DisplayOrders { get; set; }
    }

    public class ProductsEditModel : ViewModelBase
    {
        public int ProductId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string ProductName { get; set; }
        public string ProductContent { get; set; }
        public string ImagePath { get; set; }
        public int ManufacturerId { get; set; }
        public short UnitId { get; set; }
        public short ProductGroupId { get; set; }
        public short ProductTypeId { get; set; }
        public short OriginId { get; set; }
        public short WarrantyId { get; set; }
        public byte StatusId { get; set; }
        public int DisplayOrder { get; set; }
        public int CrUserId { get; set; }
        public int UpdateUserId { get; set; }
    }

    public class ProductsDisplayOrders
    {
        public short ProductId { get; set; }
        public short DisplayOrder { get; set; }
    }
}