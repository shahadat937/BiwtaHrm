using Hrm.Application;

using Hrm.Application.Features.Attendance.Requests.Queries;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Attendance)]
    public class AttendanceController:Controller
    {
        private readonly IMediator _mediator;
        
        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Attendance")]
        public async Task<ActionResult> Get()
        {
            var command = new GetAttendanceRequest { };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
