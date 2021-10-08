using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Queries;
using Web.Application.Shared.Dtos.Deals;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Deals
{
    public class QueryDealGroupInfoCommandHandler : IRequestHandler<DealGroupQueryInfoCommand, ResponseDto<DealGroupDto>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryDealGroupInfoCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<DealGroupDto>> Handle(DealGroupQueryInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<DealGroupDto>();
            try
            {
                if (request.DealId != Guid.Empty)
                {
                    var currentDeal = await unitOfWork.GetRepository<Deal>().FirstOrDefaultAsync(x => x.Id == request.DealId, include: source => source.Include(y => y.Compaign).Include(z => z.Category));
                    if (currentDeal != null)
                    {
                        var res = mapper.Map<DealGroupDto>(currentDeal);
                        if (request.GroupId.HasValue && request.GroupId.Value != Guid.Empty)
                        {
                            var currentGroup = await unitOfWork.GetRepository<Group>().FirstOrDefaultAsync(x => x.Id == request.GroupId);
                            if (currentGroup != null)
                            {
                                res.GroupId = currentGroup.Id;
                                res.GroupName = currentGroup.Name;

                            }
                        }
                        response.Result = res;
                    }
                    else
                    {
                        return new ResponseDto<DealGroupDto> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
                    }
                }
                else
                {
                    return new ResponseDto<DealGroupDto> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return response;
            }
            return response;
        }

    }
}
