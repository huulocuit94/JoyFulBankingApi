using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Dtos;

namespace Web.Application.Commands.Gifts
{
    public class TransferGiftCommand: BaseCommandDto, IRequest<ResponseDto<bool>>
    {
        public Guid GiftId { get; set; }
    }
}
