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
using Web.Application.Shared.Dtos.Categories;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Categories
{
    public class QueryCategoryCommandHandler: IRequestHandler<CategoryQueriesCommand, ResponseDto<IPagedList<CategoryDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<IPagedList<CategoryDto>>> Handle(CategoryQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IPagedList<CategoryDto>>();
            try
            {
                var items = await unitOfWork.GetRepository<Category>().Query(request.Filter, request.Order, request.PageIndex, request.PageSize);
                var count = await unitOfWork.GetRepository<Category>().CountAsync(request.Filter);
                var res = new PagedList<CategoryDto>
                {
                    Items = mapper.Map<IList<CategoryDto>>(items),
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
