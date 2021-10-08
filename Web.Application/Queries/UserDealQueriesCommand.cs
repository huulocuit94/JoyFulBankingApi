using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Deals;
using Web.Application.Shared.Enums;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Queries
{
    public class UserDealQueriesCommand : BaseCommandDto ,IRequest<ResponseDto<IPagedList<UserDealDto>>>
    {
        public UserDealStatus? Status { get; set; }
    }
}
