using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMediator mediator;
        public BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        protected Guid CurrentUserId
        {
            get
            {
                return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }
    }
}
