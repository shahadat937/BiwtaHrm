using Azure.Core;
using Hrm.Application;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.DeviceController)]
    [ApiController]
    public class AttendanceDeviceController : Controller
    {
        private readonly IMediator _mediator;
        public static bool cmdFlag = true;

        public AttendanceDeviceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("cdata")]
        public async Task<ActionResult> Post()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var data = await reader.ReadToEndAsync();
                return Ok("OK");
            }
        }

        [HttpGet]
        [Route("cdata")]
        public async Task<ActionResult> GetCData()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var data = await reader.ReadToEndAsync();
                return Ok("OK");
            }
        }

        [HttpPost]
        [Route("getrequest")]
        public async Task<ActionResult> GetRequest()
        {
            return Ok("OK");
        }

        [HttpGet]
        [Route("getrequest")]
        public async Task<ActionResult> GetRequestGet([FromQuery] string SN,[FromQuery] string? INFO)
        {
            var command = new GetRequestRequest { SN = SN, INFO = INFO};
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("ping")]
        public async Task<ActionResult> GetPing()
        {
            return Ok("OK");
        }

        [HttpPost]
        [Route("devicecmd")]
        public async Task<ActionResult> Devicemd()
        {
            return Ok("OK");
        }
    }
}
