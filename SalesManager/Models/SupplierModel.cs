using SalesManager.AppCode;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class SupplierModel : ViewModelBase
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string OrderBy { get; set; }
        public int DisplayOrder { get; set; }
        public byte ReviewStatusId { get; set; }
        public List<Suppliers> ListSuppliers { get; set; }

        private List<ReviewStatus> _listReviewStatus;
        public List<ReviewStatus> ListReviewStatus
        {
            get { return !_listReviewStatus.HasValue() ? ReviewStatus.Static_GetList() : _listReviewStatus; }
            set { _listReviewStatus = value; }
        }

        private List<OrderByClauses> _listOrderByClauses;
        public List<OrderByClauses> ListOrderByClauses
        {
            get { return !_listOrderByClauses.HasValue() ? OrderByClauses.Static_GetList("Suppliers") : _listOrderByClauses; }
            set { _listOrderByClauses = value; }
        }
    }

    public class SupplierEditModel : ViewModelBase
    {
        public int SupplierId { get; set; }

        [Display(Name = "Tên nhà cung cấp")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string SupplierName { get; set; }
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z_-]+\.[a-zA-Z_-]+(\.[a-zA-Z_-]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string Mobile { get; set; }
        public string Contact { get; set; }

        private string _DebitBalance;
        public string DebitBalance {
            get { return string.IsNullOrEmpty(_DebitBalance) ? "0" : _DebitBalance; }
            set { _DebitBalance = value; }
        }
        public string Note { get; set; }
        public int DisplayOrder { get; set; }
        public byte ReviewStatusId { get; set; }
        public DateTime CrDateTime { get; set; }

        private List<ReviewStatus> _listReviewStatus;
        public List<ReviewStatus> ListReviewStatus
        {
            get { return !_listReviewStatus.HasValue() ? ReviewStatus.Static_GetList() : _listReviewStatus; }
            set { _listReviewStatus = value; }
        }
    }

}