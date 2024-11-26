using Hrm.Application;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.DeviceUserController)]
    [ApiController]
    public class AttendanceDeviceUserController : Controller
    {
        private readonly IMediator _mediator;

        public AttendanceDeviceUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("get-PendingDevice")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetPendingDevice()
        {
            var command = new GetPendingDevicesRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
