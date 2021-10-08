using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Commands.Groups
{
    public class PickCompaignCommand: BaseCommandDto, IRequest<ResponseDto<Entity>>
    {
        public Guid CompaignId { get; set; }
    }
  
}
