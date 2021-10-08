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

    public class QueryDealCommandHandler: IRequestHandler<DealQueriesCommand, ResponseDto<IPagedList<DealDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryDealCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<IPagedList<DealDto>>> Handle(DealQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IPagedList<DealDto>>();
            try
            {
                var items = await unitOfWork.GetRepository<Deal>().Query(request.Filter, request.Order, request.PageIndex, request.PageSize, include: sourc=> sourc.Include(y=>y.Compaign).Include(z=>z.Category));
                var count = await unitOfWork.GetRepository<Deal>().CountAsync(request.Filter);
                var res = new PagedList<DealDto>
                {
                    Items = mapper.Map<IList<DealDto>>(items),
                    TotalCount = count
                };
                response.Result = res;
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
