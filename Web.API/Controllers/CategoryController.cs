using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Commands.Categories;
using Web.Application.Queries;

namespace Web.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController:BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddGroup([FromForm]AddCategoryCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("QueryCategories")]
        public async Task<IActionResult> QueryCompaigns(CategoryQueriesCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
