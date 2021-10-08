using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Commands.Groups;
using Web.Application.Queries;

namespace Web.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class GroupController : BaseController
    {
        public GroupController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroup(AddGroupCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("PickCompaign")]
        public async Task<IActionResult> PickCompaign(PickCompaignCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("QueryGroups")]
        public async Task<IActionResult> QueryGroups(GroupQueriesCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
