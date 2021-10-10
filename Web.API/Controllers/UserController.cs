using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Commands.Users;
using Web.Application.Queries;

namespace Web.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {

        public UserController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser(AddUserCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("ViewHistoryJoy")]
        public async Task<IActionResult> HistoryJoy(HistoryJoyCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("GetMyGifts")]
        public async Task<IActionResult> GetMyGifts(MyGiftQueriesCommand command)
        {
            command.CurrentUserId = base.CurrentUserId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers(UserQueriesCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var command = new UserInfoCommand
            {
                CurrentUserId = base.CurrentUserId
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
