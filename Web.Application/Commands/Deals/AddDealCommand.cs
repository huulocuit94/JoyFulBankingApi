using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Commands.Deals
{
    public class AddDealCommand : BaseCommandDto, IRequest<ResponseDto<Entity>>
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string SourceLink { get; set; }
        public string Rules { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public DealStatus Status { get; set; }
        public Guid CompaignId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
