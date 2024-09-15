using Hrm.Application;
using Hrm.Application.DTOs.AttendanceStatus;
using Hrm.Application.Features.AttendanceStatus.Requests.Commands;
using Hrm.Application.Features.AttendanceStatus.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.AttendanceStatus)]
    [ApiController]
    [Authorize]
    public class AttendanceStatusController:Controller
    {
        private readonly IMediator _mediator;
        public AttendanceStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-AttendanceStatus")]
        public async Task<ActionResult> GetAttendanceStatus()
        {
            var command = new GetAttendanceStatusRequest { };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-SelectedAttendanceStatus")]
        public async Task<ActionResult> SelectedGetAttendanceStatus()
        {
            var command = new GetSelectedAttendanceStatusRequest { };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [Route("save-AttendanceStatus")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateAttendanceStatusDto AttendanceStatusdto)
        {
            var command = new CreateAttendanceStatusCommand { AttendanceStatusDto = AttendanceStatusdto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
