using Hrm.Application;
using Hrm.Application.DTOs.LeaveRequest;
using Hrm.Application.Features.LeaveRequest.Requests.Commands;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;

namespace Hrm.Api;

[Route(HrmRoutePrefix.LeaveRequest)]
[ApiController]
public class LeaveRequestController:Controller
{
    private readonly IMediator _mediator;
    public LeaveRequestController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-LeaveRequest")]
    public async Task<ActionResult> GetLeaveRequest() {
        var command = new GetLeaveRequestRequest{};
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-LeaveRequestById/{id}")]
    public async Task<ActionResult> GetLeaveRequestById(int id) {
        var command = new GetLeaveRequestByIdRequest { LeaveRequestId = id};
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-LeaveRequestByFilter")]
    public async Task<ActionResult> GetLeaveRequestByFilter([FromQuery] LeaveRequestFilterDto filters)
    {
        var command = new GetLeaveRequestByFilterRequest { filterDto = filters };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("get-LeaveAmount")]
    public async Task<ActionResult> GetLeaveAmount([FromQuery] LeaveAmountRequestDto requestDto)
    {
        var command = new GetLeaveAmountRequest {leaveAmountRequestDto = requestDto};
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [Route("get-LeaveStatusOption")]
    public async Task<ActionResult> GetLeaveStatusOption()
    {
        var command = new GetLeaveStatusOptionRequest { };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-leaveRequest")]
    public async Task<ActionResult> SaveLeaveRequest([FromForm] CreateLeaveRequestDto leaveRequestDto, IFormFile? AssociatedFiles)
    {
        var command = new CreateLeaveRequestCommand { createLeaveRequestDto = leaveRequestDto, AssociatedFiles = AssociatedFiles };
        var response = await _mediator.Send(command);
        return Ok(response);
    }



    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("delete-LeaveRequest/{LeaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> DeleteLeaveRequest(int LeaveRequestId)
    {
        var command = new DeleteLeaveRequestCommand { LeaveRequestId = LeaveRequestId };

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("approve-LeaveRequestByReviewer/{leaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> approveLeaveRequestReviewer(int leaveRequestId)
    {
        var command = new ApproveLeaveRequestReviewerCommand { LeaveRequestId =  leaveRequestId };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("deny-LeaveRequestByReviewer/{leaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> denyLeaveRequestReviewer(int leaveRequestId)
    {
        var command = new DenyLeaveRequestReviewerCommand { LeaveRequestId= leaveRequestId };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("approve-LeaveRequestFinal/{leaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> approveLeaveRequestFinal(int leaveRequestId)
    {
        var command = new ApproveLeaveRequestFinalCommand { LeaveRequestId = leaveRequestId };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("deny-LeaveRequestFinal/{leaveRequestId}")]
    public async Task<ActionResult<BaseCommandResponse>> denyLeaveRequestFinal(int leaveRequestId)
    {
        var command = new DenyLeaveRequestFinalCommand { LeaveRequestId = leaveRequestId };
        var response = await _mediator.Send(command);

        return Ok(response);
    }

}
