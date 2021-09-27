using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Commands.Users
{
    public class AddUserCommand : IRequest<ResponseDto<Entity>>
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        [BindNever]
        public string Email { get; set; } = string.Format("{0}@gmail.com",Guid.NewGuid());
        public string CMND { get; set; }
        [JsonIgnore]
        [BindNever]
        public string RoleName { get; set; }
        [JsonIgnore]
        [BindNever]
        public bool SendSetPasswordEmail { get; set; } = false;
        public Guid GroupId { get; set; }
    }
}
