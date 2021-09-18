using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.InitDb;
using Web.Application.Shared;
using Web.Core.Infrastructures;
using Web.Data.Models.IdentityUser;
namespace Web.Application.Handlers.InitDb
{
    public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public SeedDataCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            await SeedRoles();
            await SeedUsers();
            return true;
        }
        private async Task SeedRoles()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                //creating roles
                var roles = new List<Role>
                {
                    new Role {Name = Constants.RoleAdmin, Title = "Admin"},
                    new Role {Name = Constants.RoleMember, Title = "Member"},
                    new Role {Name = Constants.RoleGroupAdmin, Title = "Group Admin"},
                };

                foreach (var t in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(t.Name))
                    {
                        await _roleManager.CreateAsync(t);
                    }
                }
            }
        }
        private async Task SeedUsers()
        {
            var dicUsers = new Dictionary<User, string>();

            dicUsers.Add(new User
            {
                UserName = "admin",
                FullName = "Admin"
            }, Constants.RoleAdmin);
            dicUsers.Add(new User
            {
                UserName = "groupadmin",
                FullName = "Group Admin"
            }, Constants.RoleGroupAdmin);
            dicUsers.Add(new User
            {
                UserName = "member",
                FullName = "Member"
            }, Constants.RoleMember);
            await CreateUser(dicUsers);
        }
        private async Task CreateUser(Dictionary<User, string> dicUsers)
        {
            foreach (var item in dicUsers)
            {
                var dicUser = item.Key;
                var user = await _userManager.FindByNameAsync(dicUser.UserName);
                if (user != null)
                    return;

                string password = Constants.DefaultPassword;
                if (item.Value == Constants.RoleAdmin)
                {
                    password = Constants.AdminPassword;
                }
                var createUser = await _userManager.CreateAsync(dicUser, password);
                if (createUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(dicUser, item.Value);
                    await _userManager.AddClaimAsync(dicUser, new Claim(ClaimTypes.Role, item.Value));
                }
            }

        }
    }
}
