using SalesManager.AppCode;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class ActionsModel : ViewModelBase
    {
        public short ActionId { get; set; }
        public short[] ActionsId { get; set; }
        public short ParentActionId { get; set; }
        public string ActionName { get; set; }
        public string SubmitType { get; set; }

        /// <summary>
        /// Danh sách thứ tự hiển thị
        /// </summary>
        public List<DisplayOrders> DisplayOrders { get; set; }

        /// <summary>
        /// Danh sách chức năng
        /// </summary>
        public List<Actions> ListActions { get; set; }

        /// <summary>
        /// Danh sách chức năng cha
        /// </summary>
        public List<Actions> ListParentActions { get; set; }

        /// <summary>
        /// Danh sách trạng thái của chức năng
        /// </summary>
        public List<ActionStatus> ListActionStatus { get; set; }
        //public List<DisplayOrderModel> DisplayOrders { get; set; }
    }

    public class ActionEditModel : ViewModelBase
    {
        private List<ActionStatus> _listActionStatus;
        private List<Actions> _listActions;
        public short ActionId { get; set; }

        [Display(Name = "Tên chức năng")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string ActionName { get; set; }

        [Display(Name = "Mô tả chức năng")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string ActionDesc { get; set; }

        public short ParentActionId { get; set; }

        public byte LevelId { get; set; }

        public string Url { get; set; }

        public byte ActionStatusId { get; set; }

        public short ActionOrder { get; set; }

        public bool Display { get; set; }

        public List<Actions> ListActions
        {
            get => !_listActions.HasValue() ? new Actions().GetRoots() : _listActions;
            set => _listActions = value;
        }

        public List<ActionStatus> ListActionStatus
        {
            get => !_listActionStatus.HasValue() ? ActionStatus.Static_GetList() : _listActionStatus;
            set => _listActionStatus = value;
        }
    }

    public class ActionRolesModel : ViewModelBase
    {
        public short ActionId { get; set; }
        public short[] RolesId { get; set; }
        public List<RoleActions> ListRoleActions { get; set; }

        private List<Roles> _listRoles;

        public List<Roles> ListRoles
        {
            get { return !_listRoles.HasValue() ? new Roles().GetAll() : _listRoles; }
            set { _listRoles = value; }
        }
    }

    public class DisplayOrders
    {
        public short ActionId { get; set; }
        public short ActionOrder { get; set; }
    }
}