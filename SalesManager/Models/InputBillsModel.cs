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
        public string CustomerName { get; set; }
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
    }
}