using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Users;
using Web.Data.Models.IdentityUser;

namespace Web.Application.Helpers.Users
{
    public class UserHelper
    {
        public async Task<JwtSecurityToken> GetJwtSecurityToken(UserManager<User> userManager, User user, IConfiguration configuration)
        {
            var now = DateTime.Now;
            var accessTokenLifetimeValue = configuration.GetValue<double>("JwtSecurityToken:AccessTokenLifetime");
            var accessTokenLifetime = now.AddMinutes(accessTokenLifetimeValue);
            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim("FullName", user.FullName),
                }.Union(userClaims);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityToken:SecretKey"]));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: configuration["JwtSecurityToken:Issuer"],
                audience: configuration["JwtSecurityToken:Audience"],
                claims: claims,
                notBefore: now,
                expires: accessTokenLifetime,
                signingCredentials: creds);
        }
    }
}
