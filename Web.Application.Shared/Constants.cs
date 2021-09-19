using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Shared
{
    public static class Constants
    {
        #region Account
        public const string AdminPassword = "12345678";
        public const string DefaultPassword = "12345678";
        #endregion
        #region Role
        public const string RoleAdmin = "Admin";
        public const string RoleMember = "Member";
        public const string RoleGroupAdmin = "GroupAdmin";
        #endregion
        #region ErrorCode
        public const int UserWrongPassword = 4001;
        public const int UserExist = 4002;
        public const int UserError = 4003;
        #endregion
    }
}
