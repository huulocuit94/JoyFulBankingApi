using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Users;
using Web.Application.Helpers.Users;
using Web.Application.Shared;
using Web.Application.Shared.Dtos.Users;
using Web.Core.Dtos;
using Web.Core.ServiceExtentions;
using Web.Data.Models.IdentityUser;

namespace Web.Application.Handlers.Users
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, ResponseDto<TokenDto>>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        public UserLoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task<ResponseDto<TokenDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<TokenDto>();
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var signInResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
                if (signInResult.Succeeded)
                {
                    var tokenDto = await CreateJwtToken(user);
                    response.Result = tokenDto;
                }
                else
                {
                    response.Errors.Add(new ErrorDto { Code = Constants.UserWrongPassword, Message = "Your user name or password is not correct. Please try again." });
                }
            }
            return response;
        }
        private async Task<UserInfoDto> GetUserInfo(User user)
        {
            var roleNames = await userManager.GetRolesAsync(user);
            var result = new UserInfoDto
            {
                FullName = user.FullName,
                Role = roleNames.FirstOrDefault()
            };
            return result;
        }
        private async Task<TokenDto> CreateJwtToken(User user)
        {
            var jwtSecurityToken = await new UserHelper().GetJwtSecurityToken(userManager, user, configuration);
            var model = new TokenDto
            {
                UserInfo = await GetUserInfo(user),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expiration = jwtSecurityToken.ValidTo.ToEpochTime()
            };

            return model;
        }
    }
}
