using Hrm.Application;
using Hrm.Application.DTOs.DayType;
using Hrm.Application.Features.DayType.Requests.Commands;
using Hrm.Application.Features.DayType.Requests.Queries;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.DayType)]
    [ApiController]
    [Authorize]
    public class DayTypeController:Controller
    {
        private readonly IMediator _mediator;
        public DayTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-DayType")]
        public async Task<ActionResult> GetDayType()
        {
            var command = new GetDayTypeRequest { };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-SelectedDayType")]
        public async Task<ActionResult> GetSelectedDayType()
        {
            var command = new GetSelectedDayTypeRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-DayType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDayTypeDto dayTypeDto)
        {
            var command = new CreateDayTypeCommand { DayTypeDto = dayTypeDto };
            var response = await _mediator.Send(command);

            return Ok(command);
        }

    }
}
