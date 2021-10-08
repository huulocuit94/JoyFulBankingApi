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
    public class QueryUserDealCommandHandler : IRequestHandler<UserDealQueriesCommand, ResponseDto<IPagedList<UserDealDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryUserDealCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<IPagedList<UserDealDto>>> Handle(UserDealQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IPagedList<UserDealDto>>();
            try
            {
                var currentDeals = await unitOfWork.GetRepository<DealUserMapping>().Query(x => x.UserId == request.CurrentUserId, include: source => source.Include(y => y.Deal).Include(z=>z.Deal.Compaign).Include(t=>t.Deal.Category));
                if (request.Status.HasValue)
                {
                    currentDeals = currentDeals.Where(x => x.UserDealStatus == request.Status.Value);
                }
                if (currentDeals.Any())
                {
                    var items = new List<UserDealDto>();
                    foreach (var item in currentDeals)
                    {
                        items.Add(new UserDealDto
                        {
                            Id = item.Id,
                            Title = item.Deal.Title,
                            Code = item.Deal.Code,
                            Rules = item.Deal.Rules,
                            Description = item.Deal.Description,
                            FileData = item.Deal.FileData,
                            Compaign = item.Deal.Compaign.Name,
                            Category = item.Deal.Category.Name,
                            DealId = item.DealId,
                            Status  = item.Deal.Status,
                            UserDealStatus = item.UserDealStatus,
                            ExpiredDate = item.Deal.Compaign.ExpiredDate,
                            GroupId = item.FromGroupId
                        });
                    }
                    response.Result = new PagedList<UserDealDto>
                    {
                        Items = items,
                        TotalCount = items.Count
                    };
                }
                return response;
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
