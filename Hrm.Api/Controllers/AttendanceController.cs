using Hrm.Application;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Features.Attendance.Requests.Commands;
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

        [HttpPost]
        [Route("save-AttendanceFromDevice")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateAttendanceDto attendance)
        {
            
            var command = new CreateAttendanceFromDeviceCommand { Attendancedto = attendance };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("save-ManualAttendance")]
        public async Task<ActionResult<BaseCommandResponse>> ManualAttendance([FromBody] CreateAttendanceDto attendance)
        {
            var command = new CreateManualAttendanceCommand { Attendancedto = attendance };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("update-AttendanceById")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateAttendanceById([FromBody] CreateAttendanceDto attendance)
        {
            var command = new UpdateAttendanceByIdCommand { Attendancedto = attendance };
            var response = await _mediator.Send(command);
            return Ok(response);

        }

    }
}
