using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;

namespace Web.Application.Shared.Dtos.Users
{
    public class UserInfoDto
    {
        public string FullName { get; set; }
        public Guid? GroupId { get; set; }
        public string GroupName { get; set; }
        public bool IsOwner { get; set; }
        public string Role { get; set; }
        public long TotalJoys { get; set; } = 0;
        public long CurrentJoys { get; set; }
        public string CMND { get; set; }
        public Rank Rank { get; set; }
        public string Avatar { get; set; }
    }
}
