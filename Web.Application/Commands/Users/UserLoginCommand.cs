using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Users;
using Web.Core.Dtos;

namespace Web.Application.Commands.Users
{
    public class UserLoginCommand: IRequest<ResponseDto<TokenDto>>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
