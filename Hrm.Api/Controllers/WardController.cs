using Hrm.Application;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.Ward.Request.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Ward)]
    [ApiController]
    public class WardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-ward")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateWardDto ward)
        {
            var command = new CreateWardCommand { WardDto = ward };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
