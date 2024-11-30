using System.Security.Cryptography.Xml;
using Hrm.Application;
using Hrm.Application.DTOs.AttDevice;
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
        public async Task<ActionResult<BaseCommandResponse>> AddUser([FromForm]AddUserDeviceDto User)
        {
            var commnad = new AddUserDeviceCommand { AddUserDeviceDto = User };
            var response = await _mediator.Send(commnad);
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
