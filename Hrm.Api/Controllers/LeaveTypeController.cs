using Hrm.Application;
using Hrm.Application.Contracts.Identity;
using Hrm.Application.DTOs.LeaveType;
using Hrm.Application.Features.LeaveType.Requests.Commands;
using Hrm.Application.Features.LeaveType.Requests.Queries;
using Microsoft.AspNetCore.Authorization;


namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.LeaveType)]
    [ApiController]
    [Authorize]
    public class LeaveTypeController: Controller
    {
        private readonly IAuthService _authenticationService;
        private readonly IMediator _mediator;
        public LeaveTypeController(IAuthService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-LeaveType")]
        public async Task<ActionResult> GetLeaveType([FromQuery] bool? ShowReport)
        {
            var command = new GetLeaveTypeRequest { ShowReport = ShowReport };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-SelectedLeaveType")]
        public async Task<ActionResult> GetSelectedLeaveType()
        {
            var command = new GetSelectedLeaveTypeRequest { };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-LeaveTypeById/{id}")]
        public async Task<ActionResult> GetLeaveTypeById(int id)
        {
            var command = new GetLeaveTypeByIdRequest { LeaveTypeId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [Route("save-LeaveType")]
        public async Task<ActionResult<BaseCommandResponse>> createLeaveType([FromBody] CreateLeaveTypeDto leaveTypeDto)
        {
            var command = new CreateLeaveTypeCommand { createLeaveTypeDto = leaveTypeDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-LeaveType/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> deleteLeaveType(int id)
        {
            var command = new DeleteLeaveTypeCommand { LeaveTypeId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Route("update-LeaveType")]
        public async Task<ActionResult<BaseCommandResponse>> updateLeaveType([FromBody] CreateLeaveTypeDto leaveTypeDto)
        {
            var command = new UpdateLeaveTypeCommand { leaveTypeDto = leaveTypeDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
