using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Commands;

namespace Web.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CompaignController: BaseController
    {
        public CompaignController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("AddCompaign")]
        public async Task<IActionResult> AddGroup(AddCompaignCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
