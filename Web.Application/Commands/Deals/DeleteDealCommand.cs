using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Dtos;

namespace Web.Application.Commands.Deals
{
    public class DeleteDealCommand : IRequest<ResponseDto<bool>>
    {
        public Guid DealId { get; set; }
    }
}
