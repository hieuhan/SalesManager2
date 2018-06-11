using SalesManager.AppCode;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class WarehouseModel:ViewModelBase
    {
        public byte WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public List<Warehouse> ListWarehouse { get; set; }
        public List<WarehouseStatus> ListWarehouseStatus { get; set; }
    }

    public class WarehouseEditModel:ViewModelBase
    {
        public byte WarehouseId { get; set; }

        [Display(Name = "Tên kho hàng")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string WarehouseName { get; set; }
        public string WarehouseDesc { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string Mobile { get; set; }
        public byte WarehouseStatusId { get; set; }

        private List<WarehouseStatus> _listWarehouseStatus;
        public List<WarehouseStatus> ListWarehouseStatus
        {
            get { return !_listWarehouseStatus.HasValue() ? new WarehouseStatus().GetAll() : _listWarehouseStatus; }
            set { _listWarehouseStatus = value; }
        }
    }
}