using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Deals;
using Web.Core.Dtos;

namespace Web.Application.Queries
{
    public class DealGroupQueryInfoCommand: IRequest<ResponseDto<DealGroupDto>>
    {
        public Guid DealId { get; set; }
        public Guid? GroupId { get; set; }
    }
}
