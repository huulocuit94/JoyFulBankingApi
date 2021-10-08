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
using Web.Application.Shared.Dtos.Groups;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Groups
{
    public class QueryGroupCommandHandler: IRequestHandler<GroupQueriesCommand, ResponseDto<IPagedList<GroupDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryGroupCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<IPagedList<GroupDto>>> Handle(GroupQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IPagedList<GroupDto>>();
            try
            {
                var items = await unitOfWork.GetRepository<Group>().Query(request.Filter, request.Order, request.PageIndex, request.PageSize);
                var count = await unitOfWork.GetRepository<Group>().CountAsync(request.Filter);
                var res = new PagedList<GroupDto>
                {
                    Items = mapper.Map<IList<GroupDto>>(items),
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
