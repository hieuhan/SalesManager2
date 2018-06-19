using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class InputBillsModel:ViewModelBase
    {
        public byte WarehouseId { get; set; }
        public byte PaymentTypeId { get; set; }
        public byte BillStatusId { get; set; }
        public string OrderBy { get; set; }
        public int SupplierId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public List<InputBills> ListInputBills { get; set; }
        public List<Users> ListUsers { get; set; }
        public List<Suppliers> ListSuppliers { get; set; }
        public List<Warehouse> ListWarehouse { get; set; }
        public List<PaymentTypes> ListPaymentTypes { get; set; }
        public List<BillStatus> ListBillStatus { get; set; }
        public List<OrderByClauses> ListOrderByClauses { get; set; }
    }

    public class InputBillsEditModel:ViewModelBase
    {
        public int InputBillId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn nhà cung cấp !")]
        public int SupplierId { get; set; }
        public float DebitBalance { get; set; }
        public float Pay { get; set; }
        public float Total { get; set; }

        [Range(1, byte.MaxValue, ErrorMessage = "Vui lòng chọn kho hàng !")]
        public byte WarehouseId { get; set; }
        [Range(1, byte.MaxValue, ErrorMessage = "Vui lòng chọn phương thức thanh toán !")]
        public byte PaymentTypeId { get; set; }
        public byte BillStatusId { get; set; }
        public DateTime CrDateTime { get; set; }
        public string Notes { get; set; }

        public List<InputBills> ListInputBills { get; set; }
        public List<Users> ListUsers { get; set; }
        public List<Suppliers> ListSuppliers { get; set; }
        public List<Warehouse> ListWarehouse { get; set; }
        public List<PaymentTypes> ListPaymentTypes { get; set; }
        public List<BillStatus> ListBillStatus { get; set; }
        public List<OrderByClauses> ListOrderByClauses { get; set; }

    }
}