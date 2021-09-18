using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Shared.Dtos.Users
{
    public class TokenDto: JwtTokenDto
    {
        public UserInfoDto UserInfo { get; set; }
    }
}
