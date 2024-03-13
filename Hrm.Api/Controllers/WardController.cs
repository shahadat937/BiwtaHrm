using Hrm.Application;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Ward.Request.Commands;
using Hrm.Application.Features.Ward.Request.Queries;
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



        [HttpGet]
        [Route("get-ward")]
        public async Task<ActionResult> Get()
        {
            var Ward = await _mediator.Send(new GetWardRequest { });
            return Ok(Ward);
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
