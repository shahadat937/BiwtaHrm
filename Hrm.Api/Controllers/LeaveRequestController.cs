using Hrm.Application;

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

}
