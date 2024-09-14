using Hrm.Application;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Leave.Requests.Commands;
using Hrm.Application.Features.Leave.Requests.Queries;
using Hrm.Application.Features.Leaves.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Leave)]
    [ApiController]
    [Authorize]
    public class LeaveController : Controller
    {
        private readonly IMediator _mediator;
        public LeaveController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpGet]
        [Route("get-Leave")]
        public async Task<ActionResult> Get()
        {
            var Leave = await _mediator.Send(new GetLeaveRequest { });
            return Ok(Leave);
        }
        [HttpGet]
        [Route("get-LeaveDetail/{id}")]
        public async Task<ActionResult<LeaveDto>> Get(int id)
        {
            var Leaves = await _mediator.Send(new GetLeaveDetailRequest { LeaveId = id });
            return Ok(Leaves);
        }
        [HttpGet]
        [Route("get-selectedLeaves")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedLeave()
        {
            var Leave = await _mediator.Send(new GetSelectedLeaveRequest { });
            return Ok(Leave);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-Leave/{id}")]
        public async Task<ActionResult> Put([FromBody] LeaveDto Leave)
        {
            var command = new UpdateLeaveCommand { LeaveDto = Leave };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Leave/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveCommand { LeaveId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
