using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Gifts;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Queries
{
    public class MyGiftQueriesCommand : BaseCommandDto ,IRequest<ResponseDto<List<GiftDto>>>
    {
        public int Top { get; set; }
    }
}
