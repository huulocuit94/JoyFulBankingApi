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
using Web.Data.Models.IdentityUser;

namespace Web.Application.Handlers.Ranks
{
    public class QueryRankUserCommandHandler : IRequestHandler<RankUserQueriesCommand, ResponseDto<IPagedList<RankDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public QueryRankUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<IPagedList<RankDto>>> Handle(RankUserQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IPagedList<RankDto>>();
            try
            {
                var curentGroup = await unitOfWork.GetRepository<GroupUserMapping>().FirstOrDefaultAsync(x => x.UserId == request.CurrentUserId);
                if (curentGroup != null)
                {
                    var allUsers = await unitOfWork.GetRepository<User>().Query(x => !x.IsAdmin && x.GroupMappings.FirstOrDefault()!= null && x.GroupMappings.FirstOrDefault().GroupId == curentGroup.GroupId, include: source => source.Include(y => y.GroupMappings));
                    var rankUsers = new List<RankDto>();
                    foreach (var item in allUsers)
                    {
                        rankUsers.Add(new RankDto
                        {
                            Name = item.FullName,
                            Joys = item.TotalJoys
                        });
                    }
                    rankUsers = rankUsers.OrderByDescending(x => x.Joys).Take(request.Top).ToList();
                    response.Result = new PagedList<RankDto>
                    {
                        Items = rankUsers,
                        TotalCount = allUsers.Count()
                    };
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
