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
using Web.Application.Shared.Dtos.Gifts;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Gifts
{
    public class QueriesMyGiftCommandHandler : IRequestHandler<MyGiftQueriesCommand, ResponseDto<List<GiftDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueriesMyGiftCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<List<GiftDto>>> Handle(MyGiftQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<List<GiftDto>>();
            try
            {
                var result = new List<GiftDto>();
                var transferJoys = await unitOfWork.GetRepository<TransferJoy>().Query(x => x.UserId == request.CurrentUserId, include: source => source.Include(y => y.Gift));
                if (transferJoys.Any())
                {
                    foreach (var item in transferJoys)
                    {
                        result.Add(mapper.Map<GiftDto>(item.Gift));
                    }
                }
                response.Result = result.Take(request.Top).ToList();
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
