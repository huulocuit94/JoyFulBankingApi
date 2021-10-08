using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Commands.Deals
{
    public class ShareDealCommand: BaseCommandDto, IRequest<ResponseDto<string>>
    {
        public Guid DealId { get; set; }
    }
}
