using System.Security.Cryptography.Xml;
using Hrm.Application;
using Hrm.Application.DTOs.AttDevice;
using Hrm.Application.Features.Attendance.Requests.Commands;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
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

        [Route("add-User")]
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> AddUser([FromForm] AddUserDeviceDto User)
        {
            var commnad = new AddUserDeviceCommand { AddUserDeviceDto = User };
            var response = await _mediator.Send(commnad);
            return Ok(response);
        }

        [Route("add-CustomCommand")]
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CustomCommand([FromForm] int DeviceId, [FromForm] string Command)
        {
            var command = new AddCustomCommandCommand { DeviceId = DeviceId, Command = Command };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Route("enroll-Fingerprint")]
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> EnrollFinger([FromForm] int? EmpId, [FromForm] string? IdCardNo, [FromForm] int DeviceId, [FromForm] int? FID)
        {
            var command = new EnrollFingerprintCommand { EmpId = EmpId, IdCardNo = IdCardNo, DeviceId = DeviceId, FID = FID };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Route("reboot-Device/{DeviceId}")]
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> RebootDevice(int DeviceId)
        {
            var command = new RebootDeviceCommand { DeviceId = DeviceId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Route("add-Device")]
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> AddDevice([FromBody] CreateAttDeviceDto Device)
        {
            var command = new AddDeviceCommand { Device = Device };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Route("get-Device")]
        [HttpGet]
        public async Task<ActionResult> GetDevice()
        {
            var command = new GetDeviceRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Route("delete-Device/{DeviceId}")]
        [HttpDelete]
        public async Task<ActionResult<BaseCommandResponse>> DeleteDevice(int DeviceId)
        {
            var command = new DeleteDeviceCommand { DeviceId = DeviceId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Route("delete-User/{EmpId}/{DeviceId}")]
        [HttpDelete]
        public async Task<ActionResult<BaseCommandResponse>> DeleteUser(int EmpId, int DeviceId)
        {
            var command = new DeleteUserDeviceCommand { EmpId = EmpId, DeviceId = DeviceId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
