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
    public class PickCompaignCommandHandler : IRequestHandler<PickCompaignCommand, ResponseDto<Entity>>
    {
        private readonly IUnitOfWork unitOfWork;
        public PickCompaignCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<Entity>> Handle(PickCompaignCommand request, CancellationToken cancellationToken)
        {
            var currentCompaign = await unitOfWork.GetRepository<Compaign>().FirstOrDefaultAsync(x => x.Id == request.CompaignId);
            if (currentCompaign != null)
            {
                var currentGroup = await unitOfWork.GetRepository<GroupUserMapping>().FirstOrDefaultAsync(x => x.UserId == request.CurrentUserId);
                if (currentGroup != null)
                {
                    var groupCompaign = new CompaignGroupMapping
                    {
                        GroupId = currentGroup.GroupId,
                        CompaignId = request.CompaignId,
                        CreatedByUserId = request.CurrentUserId,
                        ModifiedByUserId = request.CurrentUserId
                    };
                    await unitOfWork.GetRepository<CompaignGroupMapping>().AddAsync(groupCompaign);
                    await unitOfWork.SaveChangesAsync();
                    return new ResponseDto<Entity> { Result = new Entity { Id = groupCompaign.Id } };
                }
            }
            return new ResponseDto<Entity> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information"} } };
        }
    }
}
