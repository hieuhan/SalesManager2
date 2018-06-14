using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class InputBillsModel:ViewModelBase
    {
        public byte WarehouseId { get; set; }
        public byte PaymentTypeId { get; set; }
        public byte BillStatusId { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string OrderBy { get; set; }
        public int UserId { get; set; }
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
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public int SupplierId { get; set; }
        public float DebitBalance { get; set; }
        public float Pay { get; set; }
        public float Total { get; set; }
        public byte WarehouseId { get; set; }
        public byte PaymentTypeId { get; set; }
        public byte BillStatusId { get; set; }
        public DateTime Payment { get; set; }
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