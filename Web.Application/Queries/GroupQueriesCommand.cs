using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Groups;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Queries
{
    public class GroupQueriesCommand: BasePagedListCommandDto, IRequest<ResponseDto<IPagedList<GroupDto>>>
    {
    }
}
