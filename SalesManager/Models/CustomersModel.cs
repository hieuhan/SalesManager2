using SalesManager.AppCode;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class CustomersModel : ViewModelBase
    {
        public int CustomerId { get; set; }
        public int[] CustomersId { get; set; }

        public string FullName { get; set; }

        public string Mobile { get; set; }
        public short CustomerGroupId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string OrderBy { get; set; }
        public string SubmitType { get; set; }

        public List<Customers> ListCustomers { get; set; }

        /// <summary>
        /// Danh sách thứ tự hiển thị
        /// </summary>
        public List<CustomerDisplayOrders> CustomerDisplayOrders { get; set; }

        private List<CustomerGroups> _listCustomerGroups;
        public List<CustomerGroups> ListCustomerGroups
        {
            get { return !_listCustomerGroups.HasValue() ? CustomerGroups.Static_GetListAll() : _listCustomerGroups; }
            set { _listCustomerGroups = value; }
        }

        private List<Genders> _listGenders;
        public List<Genders> ListGenders
        {
            get { return !_listGenders.HasValue() ? Genders.Static_GetList() : _listGenders; }
            set { _listGenders = value; }
        }

        private List<OrderByClauses> _listOrderByClauses;
        public List<OrderByClauses> ListOrderByClauses
        {
            get { return !_listOrderByClauses.HasValue() ? OrderByClauses.Static_GetList("Customers") : _listOrderByClauses; }
            set { _listOrderByClauses = value; }
        }
    }

    public class CustomerEditModel : ViewModelBase
    {
        public int CustomerId { get; set; }

        [Display(Name = "Tên khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string FullName { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "{0} không hợp lệ !")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string Mobile { get; set; }
        public short CustomerGroupId { get; set; }
        public byte GenderId { get; set; }
        public byte StatusId { get; set; }
        public string Note { get; set; }
        private string _DebitBalance;
        public string DebitBalance
        {
            get { return string.IsNullOrEmpty(_DebitBalance) ? "0" : _DebitBalance; }
            set { _DebitBalance = value; }
        }

        private string _PaymentLimit;
        public string PaymentLimit
        {
            get { return string.IsNullOrEmpty(_PaymentLimit) ? "0" : _PaymentLimit; }
            set { _PaymentLimit = value; }
        }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastTradingDay { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CrDateTime { get; set; }

        public List<Customers> ListCustomers { get; set; }
        public List<CustomerGroups> ListCustomerGroups { get; set; }
        public List<Genders> ListGenders { get; set; }
        public List<OrderByClauses> ListOrderByClauses { get; set; }
    }

    public class CustomerDisplayOrders
    {
        public int CustomerId { get; set; }
        public int DisplayOrder { get; set; }
    }
}