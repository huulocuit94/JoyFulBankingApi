using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Queries;
using Web.Application.Shared.Dtos.Users;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;
using Web.Data.Models.IdentityUser;

namespace Web.Application.Handlers.Users
{
    public class QueriesUserCommandHandler : IRequestHandler<UserQueriesCommand, ResponseDto<IPagedList<UserInfoDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        public QueriesUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public async Task<ResponseDto<IPagedList<UserInfoDto>>> Handle(UserQueriesCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<IPagedList<UserInfoDto>>();
            try
            {
                var items = await unitOfWork.GetRepository<User>().Query(request.Filter, "fullName asc", request.PageIndex, request.PageSize);
                var count = await unitOfWork.GetRepository<User>().CountAsync(request.Filter);
                var userInfoDtos = new List<UserInfoDto>();
                foreach (var user in await items.ToListAsync())
                {
                    var userInfo = await GetUserInfo(user);
                    userInfoDtos.Add(userInfo);
                }
                var res = new PagedList<UserInfoDto>
                {
                    Items = userInfoDtos,
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
        private async Task<UserInfoDto> GetUserInfo(User user)
        {
            var roleNames = await userManager.GetRolesAsync(user);
            var currentGroup = await unitOfWork.GetRepository<GroupUserMapping>().FirstOrDefaultAsync(x => x.UserId == user.Id, include: source => source.Include(y => y.Group));
            var result = new UserInfoDto
            {
                FullName = user.FullName,
                Role = roleNames.FirstOrDefault(),
                Rank = user.Rank,
                TotalJoys = user.TotalJoys,
                CurrentJoys = user.CurrentJoys,
                CMND = user.CMND,
                Avatar = user.Avatar
            };
            if (currentGroup != null)
            {
                result.GroupName = currentGroup.Group.Name;
                result.GroupId = currentGroup.Group.Id;
                result.IsOwner = currentGroup.IsOwner;
            }
            return result;
        }
    }
}
