using Hrm.Application;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Leave.Requests.Commands;
using Hrm.Application.Features.Leave.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Leave)]
    [ApiController]
    public class LeaveController : Controller
    {
        private readonly IMediator _mediator;
        public LeaveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Leave")]
        public async Task<ActionResult> GetLeave()
        {
            var Leave = await _mediator.Send(new GetLeaveRequest { });
            return Ok(Leave);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Leave")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveDto Leave)
        {
            var command = new CreateLeaveCommand { LeaveDto = Leave };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-Leave/{id}")]
        public async Task<ActionResult> Put([FromBody] LeaveDto Leave)
        {
            var command = new UpdateLeaveCommand { LeaveDto = Leave };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Leave/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveCommand { LeaveId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
