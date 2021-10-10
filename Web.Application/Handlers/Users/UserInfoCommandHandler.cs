
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Users;
using Web.Application.Shared.Dtos.Users;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;
using Web.Data.Models.IdentityUser;

namespace Web.Application.Handlers.Users
{
    public class UserInfoCommandHandler : IRequestHandler<UserInfoCommand, ResponseDto<UserInfoDto>>
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;
        public UserInfoCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<UserInfoDto>> Handle(UserInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.CurrentUserId.ToString());
                if (user != null)
                {
                    return new ResponseDto<UserInfoDto>
                    {
                        Result = await GetUserInfo(user)
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return new ResponseDto<UserInfoDto> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found User" } } };
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
