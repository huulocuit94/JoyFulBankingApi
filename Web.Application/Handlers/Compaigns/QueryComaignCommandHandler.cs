using AutoMapper;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Queries;
using Web.Application.Shared.Dtos.Compaigns;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Compaigns
{
    public class QueryComaignCommandHandler : IRequestHandler<CompaignQueriesCommand, ResponseDto<IPagedList<CompaignDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryComaignCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<IPagedList<CompaignDto>>> Handle(CompaignQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IPagedList<CompaignDto>>();
            try
            {
                var items = await unitOfWork.GetRepository<Compaign>().Query(request.Filter, request.Order, request.PageIndex, request.PageSize);
                var count = await unitOfWork.GetRepository<Compaign>().CountAsync(request.Filter);
                var res = new PagedList<CompaignDto>
                {
                    Items = mapper.Map<IList<CompaignDto>>(items),
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
