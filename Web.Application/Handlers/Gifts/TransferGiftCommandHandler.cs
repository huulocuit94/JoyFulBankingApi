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
using Web.Data.Models.IdentityUser;

namespace Web.Application.Handlers.Gifts
{
    public class TransferGiftCommandHandler : IRequestHandler<TransferGiftCommand, ResponseDto<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public TransferGiftCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<bool>> Handle(TransferGiftCommand request, CancellationToken cancellationToken)
        {
            var result = new ResponseDto<bool>();
            try
            {
                var currentGift = await unitOfWork.GetRepository<Gift>().FirstOrDefaultAsync(x => x.Id == request.GiftId);
                if (currentGift != null)
                {
                    var currentUser = await unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x => x.Id == request.CurrentUserId);
                    if (currentUser != null)
                    {
                        if (currentUser.CurrentJoys >= currentGift.Joys)
                        {
                            var transferUserGift = new TransferJoy
                            {
                                GiftId = currentGift.Id,
                                UserId = request.CurrentUserId,
                                TranferedJoys = currentGift.Joys,
                                CreatedByUserId = request.CurrentUserId,
                                ModifiedByUserId = request.CurrentUserId
                            };
                            await unitOfWork.GetRepository<TransferJoy>().AddAsync(transferUserGift);
                            currentUser.CurrentJoys -= currentGift.Joys;
                            unitOfWork.GetRepository<User>().Update(currentUser);
                            await unitOfWork.SaveChangesAsync();
                            result.Result = true;
                        }
                        else
                        {
                            return new ResponseDto<bool> { Errors = new List<ErrorDto> { new ErrorDto { Code = 4014, Message = "Not enough Joy" } } };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return result;
        }
    }
}
