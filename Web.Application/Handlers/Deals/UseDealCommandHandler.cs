using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Deals;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Deals
{
    public class UseDealCommandHandler : IRequestHandler<UseDealCommand, ResponseDto<string>>
    {
        private readonly IUnitOfWork unitOfWork;
        public UseDealCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<string>> Handle(UseDealCommand request, CancellationToken cancellationToken)
        {
            var currentDealUserMapping = await unitOfWork.GetRepository<DealUserMapping>().FirstOrDefaultAsync(x => x.DealId == request.DealId && x.UserId == request.CurrentUserId, include: source => source.Include(y => y.Deal));
            if (currentDealUserMapping != null)
            {
                var code = currentDealUserMapping.Deal.Code;
                return new ResponseDto<string> { Result = code };
            }
            else
            {
                return new ResponseDto<string> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
            }
        }
    }
}
