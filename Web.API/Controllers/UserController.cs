using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Application.Commands.Users;

namespace Web.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> AddUser(AddUserCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
