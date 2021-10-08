using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Commands.Gifts;
using Web.Application.Queries;

namespace Web.API.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class GiftController : BaseController
    {
        public GiftController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("AddGift")]
        public async Task<IActionResult> AddGroup([FromForm] AddGiftCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("QueryGifts")]
        public async Task<IActionResult> QueryCompaigns(GiftQueriesCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("TransferGift")]
        public async Task<IActionResult> TransferGift(TransferGiftCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
