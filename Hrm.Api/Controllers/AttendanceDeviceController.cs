﻿using Azure.Core;
using Hrm.Api.ModelBindings;
using Hrm.Application;
using Hrm.Application.DTOs.Devicecmd;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
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
        public async Task<ActionResult> Post([FromQuery] string SN, [FromQuery] string Table, [ModelBinder(BinderType = typeof(RequestBodyBinding))] string RequestBody)
        {
            var command = new DeviceTableDataCommand { SN = SN, Table = Table, RequestBody = RequestBody };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("cdata")]
        public async Task<ActionResult> GetCData([FromQuery] string SN, [FromQuery] string Options, [FromQuery] string DeviceType, [FromQuery] string PushOptionsFlag)
        {
            //using (var reader = new StreamReader(Request.Body))
            //{
            //    var data = await reader.ReadToEndAsync();
            //    return Ok("OK");
            //}

            var command = new PairDeviceRequest { SN = SN, DeviceType = DeviceType };
            var response = await _mediator.Send(command);
            return Ok(response);
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
        public async Task<ActionResult> GetPing([FromQuery] string SN)
        {
            var command = new GetDevicePingRequest { SN = SN };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("devicecmd")]
        public async Task<ActionResult> Devicemd([FromQuery] string SN, [ModelBinder(BinderType = typeof(DevicecmdBinding))] DeviceCmdResponse devicecmd)
        {
            var command = new DeviceCmdResponseCommand { SN = SN, CommandResponse = devicecmd };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
