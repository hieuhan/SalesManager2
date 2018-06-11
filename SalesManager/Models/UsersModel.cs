using SalesManager.AppCode;
using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class UsersModel:ViewModelBase
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string OrderBy { get; set; }
        public byte GenderId { get; set; }
        
        public byte UserStatusId { get; set; }
        public byte UserTypeId { get; set; }

        public int[] UsersId { get; set; }

        private List<UserStatus> _listUserStatus;
        private List<UserTypes> _listUserTypes;
        public List<Users> ListUsers { get; set; }

        public List<UserStatus> ListUserStatus
        {
            get { return !_listUserStatus.HasValue() ? new UserStatus().GetAll() : _listUserStatus; }
            set { _listUserStatus = value; }
        }

        public List<UserTypes> ListUserTypes
        {
            get { return !_listUserTypes.HasValue() ? UserTypes.Static_GetList() : _listUserTypes; }
            set { _listUserTypes = value; }
        }
    }

    public class UserLoginModel
    {
        [Display(Name = "Tên truy cập")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [RegularExpression("^[a-z0-9]*$", ErrorMessage =
            "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu bao gồm 6 ký tự trở lên.", MinimumLength = 6)]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class UserCreateModel : ViewModelBase
    {
        private List<UserStatus> _listUserStatus;
        private List<UserTypes> _listUserTypes;
        private List<Genders> _listGenders;
        private List<Actions> _listActions;
        public int UserId { get; set; }

        public byte GenderId { get; set; }

        [Display(Name = "Tên truy cập")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [RegularExpression("^[a-z0-9]*$", ErrorMessage =
            "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu bao gồm 6 ký tự trở lên.", MinimumLength = 6)]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "{0} không hợp lệ !")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string Mobile { get; set; }

        public short DefaultActionId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }

        public byte UserTypeId { get; set; }

        public byte UserStatusId { get; set; }

        public string Comments { get; set; }

        public List<UserStatus> ListUserStatus
        {
            get => !_listUserStatus.HasValue() ? new UserStatus().GetAll() : _listUserStatus;
            set => _listUserStatus = value;
        }

        public List<UserTypes> ListUserTypes
        {
            get => !_listUserTypes.HasValue() ? UserTypes.Static_GetList() : _listUserTypes;
            set => _listUserTypes = value;
        }

        public List<Genders> ListGenders
        {
            get => !_listGenders.HasValue() ? Genders.Static_GetListAll() : _listGenders;
            set => _listGenders = value;
        }

        public List<Actions> ListActions
        {
            get => !_listActions.HasValue() ? new Actions().GetAllHierachy() : _listActions;
            set => _listActions = value;
        }
    }

    public class UserEditModel : ViewModelBase
    {
        private List<UserStatus> _listUserStatus;
        private List<UserTypes> _listUserTypes;
        private List<Genders> _listGenders;
        private List<Actions> _listActions;
        public int UserId { get; set; }

        public byte GenderId { get; set; }

        [Display(Name = "Tên truy cập")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [RegularExpression("^[a-z0-9]*$", ErrorMessage =
            "{0} không bao gồm khoảng trắng, chỉ bao gồm chữ cái thường và số.")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu bao gồm 6 ký tự trở lên.", MinimumLength = 6)]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z_-]+\.[a-zA-Z_-]+(\.[a-zA-Z_-]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
        public string Email { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(84|0)\\d{9,10}$", ErrorMessage = "Số điện thoại không đúng ! \n Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")]
        public string Mobile { get; set; }

        public short DefaultActionId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }

        public byte UserTypeId { get; set; }

        public byte UserStatusId { get; set; }

        public List<UserStatus> ListUserStatus
        {
            get { return !_listUserStatus.HasValue() ? new UserStatus().GetAll() : _listUserStatus; }
            set { _listUserStatus = value; }
        }

        public List<UserTypes> ListUserTypes
        {
            get { return !_listUserTypes.HasValue() ? UserTypes.Static_GetList() : _listUserTypes; }
            set { _listUserTypes = value; }
        }

        public List<Genders> ListGenders
        {
            get { return !_listGenders.HasValue() ? Genders.Static_GetListAll() : _listGenders; }
            set { _listGenders = value; }
        }

        public List<Actions> ListActions
        {
            get { return !_listActions.HasValue() ? new Actions().GetAllHierachy() : _listActions; }
            set { _listActions = value; }
        }
    }

    public class UserRolesModel : ViewModelBase
    {
        public short[] RolesId { get; set; }
        public int UserId { get; set; }
        public List<UserRoles> ListUserRoles { get; set; }

        private List<Roles> _listRoles;
        public List<Roles> ListRoles
        {
            get { return !_listRoles.HasValue() ? new Roles().GetAll() : _listRoles; }
            set { _listRoles = value; }
        }
    }

    public class UserActionsModel : ViewModelBase
    {
        public int UserId { get; set; }
        public short[] ActionsId { get; set; }
        public List<Actions> ListActions{get;set;}
        public List<UserActions> ListUserActions { get; set; }
    }

}