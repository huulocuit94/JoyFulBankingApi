using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Deals;
using Web.Application.Shared.Enums;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;
using Web.Data.Models.IdentityUser;

namespace Web.Application.Handlers.Deals
{
    public class CompleteDealCommandHandler : IRequestHandler<CompleteDealCommand, ResponseDto<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration config;
        public CompleteDealCommandHandler(IUnitOfWork unitOfWork, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
        }
        public async Task<ResponseDto<bool>> Handle(CompleteDealCommand request, CancellationToken cancellationToken)
        {
            try
            {

            }catch(Exception ex)
            {
                Log.Error(ex.Message);
            }
            var currentDeal = await unitOfWork.GetRepository<Deal>().FirstOrDefaultAsync(x => x.Code == request.Code, include: source => source.Include(y => y.Compaign));
            if (currentDeal != null)
            {
                if (currentDeal.Compaign.ExpiredDate < DateTimeOffset.Now)
                {
                    return new ResponseDto<bool> { Errors = new List<ErrorDto> { new ErrorDto { Code = 410, Message = "Expired Date" } } };
                }
                else
                {
                    var currentDealUserMapping = await unitOfWork.GetRepository<DealUserMapping>().FirstOrDefaultAsync(x => x.DealId == currentDeal.Id && x.UserId == request.CurrentUserId, include: source => source.Include(y => y.Deal));
                    if (currentDealUserMapping != null)
                    {
                        currentDealUserMapping.UserDealStatus = Shared.Enums.UserDealStatus.Used;
                        currentDealUserMapping.TransactionId = Guid.NewGuid();
                        // Tích điểm cá nhân
                        unitOfWork.GetRepository<DealUserMapping>().Update(currentDealUserMapping);
                        var currentUser = await unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x => x.Id == request.CurrentUserId);
                        if (currentUser != null)
                        {
                            currentUser.CurrentJoys += currentDealUserMapping.Deal.Joys;
                            currentUser.TotalJoys += currentDealUserMapping.Deal.Joys;
                            unitOfWork.GetRepository<User>().Update(currentUser);
                        }
                        if (currentDealUserMapping.FromGroupId.HasValue)
                        {
                            var currentGroup = await unitOfWork.GetRepository<Group>().FirstOrDefaultAsync(x => x.Id == currentDealUserMapping.FromGroupId);
                            if (currentGroup != null)
                            {
                                currentGroup.Joys += currentDealUserMapping.Deal.Joys;
                                currentGroup.Rank = TransferJoyToRank(currentGroup.Joys);
                                if (currentUser != null)
                                    currentUser.Rank = currentGroup.Rank;
                                unitOfWork.GetRepository<Group>().Update(currentGroup);
                                unitOfWork.GetRepository<User>().Update(currentUser);
                            }
                        }
                        await unitOfWork.SaveChangesAsync();
                        return new ResponseDto<bool> { Result = true };
                    }
                    else
                    {
                        return new ResponseDto<bool> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
                    }
                }
            }
            else
            {
                return new ResponseDto<bool> { Errors = new List<ErrorDto> { new ErrorDto { Code = 404, Message = "Not Found Information" } } };
            }

        }
        private Rank TransferJoyToRank(long joys)
        {
            if (joys <= int.Parse(config["Rank:YoyWarm"]))
            {
                return Rank.JoyWarm;
            }
            else if (joys <= int.Parse(config["Rank:JoyHappy"]))
            {
                return Rank.JoyHappy;
            }
            else if (joys <= int.Parse(config["Rank:JoyFun"]))
            {
                return Rank.JoyFun;
            }
            else if (joys >= int.Parse(config["Rank:JoyFul"]))
            {
                return Rank.JoyFul;
            }
            return Rank.JoyWarm;
        }
    }
}

