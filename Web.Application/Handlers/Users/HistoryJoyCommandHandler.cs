using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Queries;
using Web.Application.Shared.Dtos.Users;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Users
{
    public class HistoryJoyCommandHandler : IRequestHandler<HistoryJoyCommand, ResponseDto<List<HistoryJoyDto>>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public HistoryJoyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<List<HistoryJoyDto>>> Handle(HistoryJoyCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto<List<HistoryJoyDto>>();
            try
            {
                var result = new List<HistoryJoyDto>();
                // Sử dụng ưu đãi
                var usedDeals = await unitOfWork.GetRepository<DealUserMapping>().Query(x => x.UserId == request.CurrentUserId && x.UserDealStatus == Shared.Enums.UserDealStatus.Used, include: source => source.Include(y => y.Deal));
                if (usedDeals.Any())
                {
                    foreach (var deal in usedDeals)
                    {
                        result.Add(new HistoryJoyDto
                        {
                            Date = deal.ModifiedDate,
                            Content = $"Đã sử dụng ưu đãi: {deal.Deal.Title}",
                            Joy = $"+{deal.Deal.Joys}"
                        });
                    }
                }
                // Đổi quà
                var transferGifts = await unitOfWork.GetRepository<TransferJoy>().Query(x => x.UserId == request.CurrentUserId, include: source => source.Include(y => y.Gift));
                if (transferGifts.Any())
                {
                    foreach (var deal in transferGifts)
                    {
                        result.Add(new HistoryJoyDto
                        {
                            Date = deal.CreatedDate,
                            Content = $"Đã đổi quà: {deal.Gift.Name}",
                            Joy = $"-{deal.Gift.Joys}"
                        });
                    }
                }
                if (result.Any())
                    result = result.OrderByDescending(x => x.Date).Take(request.Top).ToList();
                response.Result = result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return response;
            }
            return response;
        }
    }
}
