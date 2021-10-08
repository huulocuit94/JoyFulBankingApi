using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Dtos;

namespace Web.Application.Commands.Deals
{
    public class UseDealCommand : BaseCommandDto, IRequest<ResponseDto<string>>
    {
        public Guid DealId { get; set; }
    }
}
