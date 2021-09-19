using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Groups;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Groups
{
    public class AddGroupCommandHanlder : IRequestHandler<AddGroupCommand, ResponseDto<Entity>>
    {
        private readonly IUnitOfWork unitOfWork;
        public AddGroupCommandHanlder(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<Entity>> Handle(AddGroupCommand request, CancellationToken cancellationToken)
        {
            var newGroup = new Group
            {
                Name = request.Name,
                CreatedByUserId = request.CurrentUserId,
                ModifiedByUserId = request.CurrentUserId
            };
            var isAvailableGroup = await unitOfWork.GetRepository<Group>().AnyAsync(x => x.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase));
            if (!isAvailableGroup)
            {
                return new ResponseDto<Entity> { Errors = new List<ErrorDto> { new ErrorDto { Code = 4004, Message = "InValid" } } };
            }
            else
            {
                await unitOfWork.GetRepository<Group>().AddAsync(newGroup);
                await unitOfWork.GetRepository<GroupUserMapping>().AddAsync(new GroupUserMapping
                {
                    GroupId = newGroup.Id,
                    UserId = request.CurrentUserId,
                    IsOwner = true
                });
                await unitOfWork.SaveChangesAsync();
            }

            return new ResponseDto<Entity> { Result = new Entity { Id = newGroup.Id } };
        }
    }
}
