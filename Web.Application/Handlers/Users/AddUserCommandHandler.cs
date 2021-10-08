using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Users;
using Web.Application.Shared;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;
using Web.Data.Models.IdentityUser;

namespace Web.Application.Handlers.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ResponseDto<Entity>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        public AddUserCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public async Task<ResponseDto<Entity>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<Entity>();
            var user = await userManager.FindByNameAsync(request.PhoneNumber);
            if (user != null)
            {
                response.Errors.Add(new ErrorDto { Code = Constants.UserWrongPassword, Message = "Username" });
                return response;
            }
            var newUser = new User
            {
                UserName = request.PhoneNumber,
                Email = request.Email,
                FullName = request.FullName
            };
            var createdUser = await userManager.CreateAsync(newUser, request.Password);
            if (createdUser.Succeeded)
            {
                var createRole = await userManager.AddToRoleAsync(newUser, request.RoleName);
                var createClaim = await userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, request.RoleName));
                if (!createRole.Succeeded)
                {
                    response.Errors.Add(new ErrorDto { Code = Constants.UserError, Message = createRole.Errors.First().Description });
                }
                else if (!createClaim.Succeeded)
                {
                    response.Errors.Add(new ErrorDto { Code = Constants.UserError, Message = createClaim.Errors.First().Description });
                }
                if (request.GroupId.HasValue)
                {
                    var group = await unitOfWork.GetRepository<Group>().FirstOrDefaultAsync(x => x.Id == request.GroupId);
                    if (group != null)
                    {
                        await unitOfWork.GetRepository<GroupUserMapping>().AddAsync(new GroupUserMapping
                        {
                            GroupId = request.GroupId.Value,
                            UserId = newUser.Id,
                            CreatedByUserId = newUser.Id,
                            ModifiedByUserId = newUser.Id
                        });
                    }
                }
                await unitOfWork.SaveChangesAsync();
                response.Result = new Entity { Id = newUser.Id };
            }
            else
            {
                response.Errors.Add(new ErrorDto { Code = Constants.UserWrongPassword, Message = "Username" });
            }
            return response;
        }
    }
}
