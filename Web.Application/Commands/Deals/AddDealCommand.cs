using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Code { get; set; }
        [Required]
        public int Joys { get; set; }
        public string SourceLink { get; set; }
        public string Rules { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public DealStatus Status { get; set; }
        [Required]
        public Guid CompaignId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
