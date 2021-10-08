using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Commands.Groups
{
    public class AddGroupCommand : BaseCommandDto, IRequest<ResponseDto<Entity>>
    {
        [Required]
        public string Name { get; set; }
    }
}
