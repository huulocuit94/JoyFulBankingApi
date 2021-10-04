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

namespace Web.Application.Commands
{
    public class AddCompaignCommand: BaseCommand, IRequest<ResponseDto<Entity>>
    {
        public string Name { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
    }
}
