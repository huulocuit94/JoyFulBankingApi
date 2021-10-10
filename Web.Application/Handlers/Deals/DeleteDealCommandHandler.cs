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
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Deals
{
    public class DeleteDealCommandHandler : IRequestHandler<DeleteDealCommand, ResponseDto<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DeleteDealCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(DeleteDealCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deal = await unitOfWork.GetRepository<Deal>().FirstOrDefaultAsync(x => x.Id == request.DealId);
                if (deal != null)
                {
                    var sharedDeal = await unitOfWork.GetRepository<SharedDealTracking>().FirstOrDefaultAsync(x => x.DealId == request.DealId);
                    if (sharedDeal != null)
                    {
                        unitOfWork.GetRepository<SharedDealTracking>().Remove(sharedDeal);
                    }
                    var dealUser = await unitOfWork.GetRepository<DealUserMapping>().FirstOrDefaultAsync(x => x.DealId == request.DealId);
                    if (dealUser != null)
                    {
                        unitOfWork.GetRepository<DealUserMapping>().Remove(dealUser);
                    }
                    unitOfWork.GetRepository<Deal>().Remove(deal);
                }
                await unitOfWork.SaveChangesAsync();
                return new ResponseDto<bool>() { Result = true };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return new ResponseDto<bool> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
        }
    }
}
