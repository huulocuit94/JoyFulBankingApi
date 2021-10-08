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
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        public SeedDataCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<bool> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            await SeedRoles();
            await SeedUsers();
            return true;
        }
        private async Task SeedRoles()
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                //creating roles
                var roles = new List<Role>
                {
                    new Role {Name = Constants.RoleAdmin, Title = "Admin"},
                    new Role {Name = Constants.RoleMember, Title = "Member"}
                };

                foreach (var t in roles)
                {
                    if (!await roleManager.RoleExistsAsync(t.Name))
                    {
                        await roleManager.CreateAsync(t);
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
                var user = await userManager.FindByNameAsync(dicUser.UserName);
                if (user != null)
                    return;

                string password = Constants.DefaultPassword;
                if (item.Value == Constants.RoleAdmin)
                {
                    password = Constants.AdminPassword;
                }
                var createUser = await userManager.CreateAsync(dicUser, password);
                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(dicUser, item.Value);
                    await userManager.AddClaimAsync(dicUser, new Claim(ClaimTypes.Role, item.Value));
                }
            }

        }
    }
}
