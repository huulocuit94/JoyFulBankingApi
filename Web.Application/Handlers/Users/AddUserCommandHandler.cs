using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Users;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Handlers.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ResponseDto<Entity>>
    {
        public Task<ResponseDto<Entity>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
