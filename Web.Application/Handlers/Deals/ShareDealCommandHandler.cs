using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Deals;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Deals
{
    public class ShareDealCommandHandler : IRequestHandler<ShareDealCommand, ResponseDto<string>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly string DOMAIN = "http://joyacb.fun:8089";
        public ShareDealCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<string>> Handle(ShareDealCommand request, CancellationToken cancellationToken)
        {
            var currentDeal = await unitOfWork.GetRepository<Deal>().FirstOrDefaultAsync(x => x.Id == request.DealId);
            if (currentDeal != null)
            {
                var currentGroup = await unitOfWork.GetRepository<GroupUserMapping>().FirstOrDefaultAsync(x => x.UserId == request.CurrentUserId);
                if (currentGroup != null)
                {
                    var sharedDealTracking = await unitOfWork.GetRepository<SharedDealTracking>().FirstOrDefaultAsync(x => x.GroupId == currentGroup.GroupId && x.DealId == request.DealId);
                    if (sharedDealTracking != null)
                    {
                        return new ResponseDto<string> { Result = sharedDealTracking.LinkToSharing };
                    }
                    else
                    {
                        var newsharedDealTracking = new SharedDealTracking
                        {
                            DealId = request.DealId,
                            GroupId = currentGroup.GroupId,
                            LinkToSharing = string.Format("{0}/deal/{1}/{2}", DOMAIN, currentDeal.Id, currentGroup.GroupId),
                            CreatedByUserId = request.CurrentUserId,
                            ModifiedByUserId = request.CurrentUserId
                        };
                        await unitOfWork.GetRepository<SharedDealTracking>().AddAsync(newsharedDealTracking);
                        await unitOfWork.SaveChangesAsync();
                        return new ResponseDto<string> { Result = newsharedDealTracking.LinkToSharing };
                    }
                }
                else
                {
                    var sharedDealTracking = await unitOfWork.GetRepository<SharedDealTracking>().FirstOrDefaultAsync(x => x.DealId == request.DealId && x.CreatedByUserId == request.CurrentUserId);
                    if (sharedDealTracking != null)
                    {
                        return new ResponseDto<string> { Result = sharedDealTracking.LinkToSharing };
                    }
                    else
                    {
                        var newsharedDealTracking = new SharedDealTracking
                        {
                            DealId = request.DealId,
                            LinkToSharing = string.Format("{0}/deal/{1}", DOMAIN, currentDeal.Id),
                            CreatedByUserId = request.CurrentUserId,
                            ModifiedByUserId = request.CurrentUserId
                        };
                        await unitOfWork.GetRepository<SharedDealTracking>().AddAsync(newsharedDealTracking);
                        await unitOfWork.SaveChangesAsync();
                        return new ResponseDto<string> { Result = newsharedDealTracking.LinkToSharing };
                    }
                }

            }
            return new ResponseDto<string> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
        }
    }
}
