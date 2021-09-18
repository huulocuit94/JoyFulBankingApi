using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Models.IdentityUser
{
    public class User: IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserToken> Tokens { get; set; }
        public ICollection<UserRole> Roles { get; set; }
    }
}
