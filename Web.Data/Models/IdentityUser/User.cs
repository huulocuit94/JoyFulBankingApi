
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;

namespace Web.Data.Models.IdentityUser
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Claims = new HashSet<UserClaim>();
            Tokens = new HashSet<UserToken>();
            Roles = new HashSet<UserRole>();
            GroupMappings = new HashSet<GroupUserMapping>();
            CompaignUserMappings = new HashSet<CompaignUserMapping>();
            DealUserMappings = new HashSet<DealUserMapping>();
        }

        public string FullName { get; set; }
        public long TotalJoys { get; set; } = 0;
        public long CurrentJoys { get; set; }
        public string CMND { get; set; }
        public Rank Rank { get; set; }
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<UserToken> Tokens { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<GroupUserMapping> GroupMappings { get; set; }
        public virtual ICollection<CompaignUserMapping> CompaignUserMappings { get; set; }
        public virtual ICollection<DealUserMapping> DealUserMappings { get; set; }
    }
}
