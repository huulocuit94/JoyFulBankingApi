using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Queries;

namespace Web.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RankController: BaseController
    {
        public RankController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("QueryRankUsers")]
        public async Task<IActionResult> AddGroup(RankUserQueriesCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("QueryRankGroups")]
        public async Task<IActionResult> QueryRankGroups(RankGroupQueriesCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
