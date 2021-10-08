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

                var itemMappings = mapper.Map<IList<CompaignDto>>(items);
                if (itemMappings.Any())
                {
                    var currentGroup = await unitOfWork.GetRepository<GroupUserMapping>().FirstOrDefaultAsync(x => x.UserId == request.CurrentUserId);
                    if (currentGroup != null)
                    {
                        var allUsedDeals = await unitOfWork.GetRepository<DealUserMapping>().Query(x => x.FromGroupId == currentGroup.GroupId && x.UserDealStatus == Shared.Enums.UserDealStatus.Used, include: source => source.Include(y => y.Deal.Compaign));
                        foreach (var item in itemMappings)
                        {
                            item.Percent = GetPercent(item, allUsedDeals);
                        }
                    }
                }
                var res = new PagedList<CompaignDto>
                {
                    Items = itemMappings,
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
        private decimal GetPercent(CompaignDto compaign, IEnumerable<DealUserMapping> usedDeals)
        {
            if (usedDeals.Any(x => x.Deal.CompaignId == compaign.Id))
            {
                var totalJoyOfGroupByCompaigns = usedDeals.Where(x => x.Deal.CompaignId == compaign.Id).Sum(y => y.Deal.Joys);
                return Math.Round((decimal)totalJoyOfGroupByCompaigns / compaign.YoysAchievement, 0);
            }
            return 0;
        }
    }
}
