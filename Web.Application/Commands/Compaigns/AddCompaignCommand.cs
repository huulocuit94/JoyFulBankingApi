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

namespace Web.Application.Commands
{
    public class AddCompaignCommand: BaseCommandDto, IRequest<ResponseDto<Entity>>
    {
        public string Name { get; set; }
        [Required]
        public long YoysAchievement { get; set; }
        [Required]
        public DateTimeOffset ExpiredDate { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
    }
}
