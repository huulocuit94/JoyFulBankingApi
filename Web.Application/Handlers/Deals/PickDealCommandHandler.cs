using AutoMapper;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Deals;
using Web.Application.Shared.Dtos.Deals;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Deals
{
    public class PickDealCommandHandler : IRequestHandler<PickDealCommand, ResponseDto<DealDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public PickDealCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ResponseDto<DealDto>> Handle(PickDealCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // kiem tra da nhan link nay hay chua
                var pickedDeal = await unitOfWork.GetRepository<DealUserMapping>().AnyAsync(x => x.DealId == request.DealId && x.UserId == request.CurrentUserId);
                if (pickedDeal)
                {
                    return new ResponseDto<DealDto> { Errors = new List<ErrorDto> { new ErrorDto { Code = 409, Message = "Bạn đã nhận ưu đãi trước đó" } } };
                }
                else
                {
                    var newPickDeal = new DealUserMapping
                    {
                        DealId = request.DealId,
                        UserId = request.CurrentUserId,
                        CreatedByUserId = request.CurrentUserId,
                        ModifiedByUserId = request.CurrentUserId,
                        UserDealStatus = Shared.Enums.UserDealStatus.Received
                    };
                    var currentGroup = await unitOfWork.GetRepository<GroupUserMapping>().FirstOrDefaultAsync(x => x.UserId == request.CurrentUserId);
                    if (currentGroup!= null)
                    {
                        newPickDeal.FromGroupId = currentGroup.GroupId;
                    }
                    await unitOfWork.GetRepository<DealUserMapping>().AddAsync(newPickDeal);
                    await unitOfWork.SaveChangesAsync();
                    var currentDeal = await unitOfWork.GetRepository<Deal>().FirstOrDefaultAsync(x => x.Id == request.DealId);
                    return new ResponseDto<DealDto>() { Result = mapper.Map<DealDto>(currentDeal) };
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return new ResponseDto<DealDto> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
        }
    }
}
