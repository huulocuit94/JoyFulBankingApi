using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Application.Commands.Deals;
using Web.Application.Queries;

namespace Web.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DealController : BaseController
    {
        public DealController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("AddDeal")]
        public async Task<IActionResult> AddDeal([FromForm] AddDealCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("QueryDeals")]
        public async Task<IActionResult> QueryCompaigns(DealQueriesCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("ShareDeal")]
        public async Task<IActionResult> QueryCompaigns(ShareDealCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("PickDeal")]
        public async Task<IActionResult> PickDeal(PickDealCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("GetMyDeals")]
        public async Task<IActionResult> GetMyDeals(UserDealQueriesCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("UseDeal")]
        public async Task<IActionResult> UseDeal(UseDealCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("CompleteDeal")]
        public async Task<IActionResult> CompleteDeal(CompleteDealCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("GetDealGroupInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDealGroupInfo(DealGroupQueryInfoCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
