using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class CustomerGroupModel : ViewModelBase
    {
        public short[] CustomerGroupsId { get; set; }
        public string CustomerGroupName { get; set; }
        public List<CustomerGroups> ListCustomerGroups { get; set; }
        public List<Users> ListUsers { get; set; }
    }
    public class CustomerGroupEditModel : ViewModelBase
    {
        public short CustomerGroupId { get; set; }

        [Display(Name = "Tên nhóm khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string CustomerGroupName { get; set; }
        public string CustomerGroupDesc { get; set; }
        public int CrUserId { get; set; }
        public DateTime CrDateTime { get; set; }
    }
}