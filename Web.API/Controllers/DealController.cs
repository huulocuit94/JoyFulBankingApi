using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Application.Commands.Deals;

namespace Web.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DealController: BaseController
    {
        public DealController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("AddDeal")]
        public async Task<IActionResult> AddDeal([FromForm]AddDealCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
