using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Shared.Dtos.Users
{
    public class JwtTokenDto
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
    }
}
