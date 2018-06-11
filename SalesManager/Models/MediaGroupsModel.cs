using SalesManagerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManager.Models
{
    public class MediaGroupsModel : ViewModelBase
    {
        public short MediaGroupId { get; set; }

        public string MediaGroupName { get; set; }
        public string MediaGroupDesc { get; set; }

        public List<MediaGroups> ListMediaGroups { get; set; }

        public List<Users> ListUsers { get; set; }
    }

    public class MediaGroupsEditModel : ViewModelBase
    {
        public short MediaGroupId { get; set; }

        [Display(Name = "Tên nhóm Media")]
        [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
        public string MediaGroupName { get; set; }

        public string MediaGroupDesc { get; set; }

        public short ParentGroupId { get; set; }

        public int CrUserId { get; set; }

        public DateTime CrDateTime { get; set; }

    }
}