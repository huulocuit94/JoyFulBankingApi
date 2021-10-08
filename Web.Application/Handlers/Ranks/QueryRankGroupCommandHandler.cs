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
using Web.Application.Shared.Dtos.Ranks;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Ranks
{
    public class QueryRankGroupCommandHandler : IRequestHandler<RankGroupQueriesCommand, ResponseDto<RankGroupDto>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryRankGroupCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<RankGroupDto>> Handle(RankGroupQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<RankGroupDto>();
            try
            {
                var result = new List<RandGroupDetailDto>();
                var allUsedDeal = await unitOfWork.GetRepository<DealUserMapping>().Query(x => x.UserDealStatus == Shared.Enums.UserDealStatus.Used && x.FromGroupId.HasValue, include: source => source.Include(y => y.Deal).Include(z => z.Deal.Compaign).Include(t => t.Group));
                var groupByCompaign = allUsedDeal.AsEnumerable().GroupBy(x => x.Deal.Compaign.Name);
                foreach (var compaign in groupByCompaign)
                {
                    var rankGroup = new RandGroupDetailDto()
                    {
                        Compaign = compaign.Key
                    };
                    var groups = compaign.GroupBy(x => x.Group.Name);
                    foreach (var group in groups)
                    {
                        rankGroup.Items.Add(new RankDto
                        {
                            Name = group.Key,
                            Joys = group.FirstOrDefault().Group.Joys
                        });
                    }
                    rankGroup.Items = rankGroup.Items.OrderByDescending(x => x.Joys).Take(request.Top).ToList();
                    result.Add(rankGroup);
                }
                response.Result = new RankGroupDto
                {
                    Data = result
                };
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
