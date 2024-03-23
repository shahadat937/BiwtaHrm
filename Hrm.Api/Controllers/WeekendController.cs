using Hrm.Application;
using Hrm.Application.DTOs.Weekend;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.Features.Weekend.Handlers.Queries;
using Hrm.Application.Features.Weekend.Requests.Commands;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Weekend)]
    [ApiController]
    public class WeekendController : Controller
    {
        private readonly IMediator _mediator;
        public WeekendController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-weekend")]
        public async Task<ActionResult> GetWeekend()
        {
            var Weekend = await _mediator.Send(new GetWeekendRequest { });
            return Ok(Weekend);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-weekend")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateWeekendDto Weekend)
        {
            var command = new CreateEmployeeCommand { WeekendDto = Weekend };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-weekend/{id}")]
        public async Task<ActionResult> Put([FromBody] WeekendDto Weekend)
        {
            var command = new UpdateWeekendCommand { WeekendDto = Weekend };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-weekend/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteWeekendCommand { WeekendId = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
