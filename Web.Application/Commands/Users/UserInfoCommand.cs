using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Users;
using Web.Core.Dtos;

namespace Web.Application.Commands.Users
{
    public class UserInfoCommand : BaseCommandDto, IRequest<ResponseDto<UserInfoDto>>
    {
        public UserInfoCommand()
        {

        }
    }
}
