using AutoMapper;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Gifts;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Gifts
{
    public class DeleteGiftCommandHandler : IRequestHandler<DeleteGiftCommand, ResponseDto<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DeleteGiftCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ResponseDto<bool>> Handle(DeleteGiftCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var gift = await unitOfWork.GetRepository<Gift>().FirstOrDefaultAsync(x => x.Id == request.GiftId);
                if (gift != null)
                {
                    unitOfWork.GetRepository<Gift>().Remove(gift);
                }
                await unitOfWork.SaveChangesAsync();
                return new ResponseDto<bool>() { Result = true };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return new ResponseDto<bool> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
        }
    }
}
