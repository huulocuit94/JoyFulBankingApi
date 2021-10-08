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
using Web.Application.Shared.Dtos.Gifts;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Gifts
{
    public class QueryGiftCommandHandler : IRequestHandler<GiftQueriesCommand, ResponseDto<IPagedList<GiftDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryGiftCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<IPagedList<GiftDto>>> Handle(GiftQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IPagedList<GiftDto>>();
            try
            {
                var items = await unitOfWork.GetRepository<Gift>().Query(request.Filter, request.Order, request.PageIndex, request.PageSize);
                var count = await unitOfWork.GetRepository<Gift>().CountAsync(request.Filter);
                var transferGifts = await unitOfWork.GetRepository<TransferJoy>().Query(x => x.CreatedByUserId == request.CurrentUserId);
                var ids = transferGifts.Select(x => x.GiftId);
                var res = new PagedList<GiftDto>
                {
                    Items = mapper.Map<IList<GiftDto>>(items.Where(x => !ids.Contains(x.Id))),
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
