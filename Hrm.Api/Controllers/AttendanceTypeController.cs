using Hrm.Application;
using Hrm.Application.DTOs.AttendanceType;
using Hrm.Application.Features.AttendanceType.Requests.Commands;
using Hrm.Application.Features.AttendanceType.Requests.Queries;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.AttendanceType)]
    public class AttendanceTypeController:Controller
    {
        private readonly IMediator _mediator;
        public AttendanceTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-AttendanceType")]
        public async Task<ActionResult> GetAttendanceType ()
        {
            var command = new GetAttendanceTypeRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-SelectedAttendanceType")]
        public async Task<ActionResult> Get()
        {
            var command = new GetSelectedAttendanceTypeRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("save-AttendanceType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateAttendanceTypeDto AttendanceTypedto)
        {
            var command = new CreateAttendanceTypeCommand { AttendanceTypeDto = AttendanceTypedto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
