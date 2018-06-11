using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class RolesModel : ViewModelBase
    {
        public short RoleId { get; set; }

        [Display(Name = "Tên quyền")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string RoleName { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string RoleDesc { get; set; }

        public byte BuildIn { get; set; }
    }

    public class RoleActionsModel : ViewModelBase
    {
        public short RoleId { get; set; }

        public short[] ActionsId { get; set; }

        public short[] RolesId { get; set; }

        public List<Actions> ListActions{ get;set; }

        public List<Actions> ListActionsByRole { get; set; }
    }
}