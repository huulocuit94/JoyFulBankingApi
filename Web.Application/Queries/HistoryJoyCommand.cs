using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Users;
using Web.Core.Dtos;

namespace Web.Application.Queries
{
    public class HistoryJoyCommand : BaseCommandDto, IRequest<ResponseDto<List<HistoryJoyDto>>>
    {
        public int Top { get; set; } 
    }
}
