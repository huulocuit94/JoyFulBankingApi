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

namespace Web.Application.Commands.Categories
{
    public class AddCategoryCommand: BaseCommandDto, IRequest<ResponseDto<Entity>>
    {
        [Required]
        public string Name { get; set; }
        public IFormFile Icon { get; set; }
        public string Description { get; set; }
    }
}
