using Hrm.Application;
using Hrm.Application.Features.AspNetUsers.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.AspNetUsers)]
    [ApiController]
    public class AspNetUsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AspNetUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-users")]
        public async Task<ActionResult> Get()
        {
            var Users = await _mediator.Send(new GetUserListRequest { });
            return Ok(Users);
        }
    }
}
