using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Dtos;
using Web.Core.Infrastructures;

namespace Web.Application.Commands.Gifts
{
    public class AddGiftCommand : BaseCommandDto, IRequest<ResponseDto<Entity>>
    {
        public IFormFile Picture { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Joys { get; set; }
        [Required]
        public DateTimeOffset ExpiredDate { get; set; }
    }
}
