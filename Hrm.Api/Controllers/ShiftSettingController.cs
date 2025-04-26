using Hrm.Application.DTOs.ShiftSetting;
using Hrm.Application.Features.ShiftSettings.Requests.Commands;
using Hrm.Application.Features.ShiftSettings.Requests.Queries;
using Hrm.Application;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.ShiftSetting)]
    [ApiController]
    [Authorize]
    public class ShiftSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShiftSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-ShiftSettings")]
        public async Task<ActionResult<List<ShiftSettingDto>>> Get()
        {
            var ShiftSettings = await _mediator.Send(new GetShiftSettingListRequest { });
            return Ok(ShiftSettings);
        }

        [HttpGet]
        [Route("get-selectedShiftSettings")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedShiftSetting()
        {
            var ShiftSettings = await _mediator.Send(new GetSelectedShiftSettingRequest { });
            return Ok(ShiftSettings);
        }

        [HttpGet]
        [Route("get-ActiveShiftSettings")]
        public async Task<ActionResult<ShiftSettingDto>> GetActive()
        {
            var ShiftSettings = await _mediator.Send(new GetActiveShiftSettingRequest { });
            return Ok(ShiftSettings);
        }

        [HttpGet]
        [Route("get-ShiftSettingDetail/{id}")]
        public async Task<ActionResult<ShiftSettingDto>> Get(int id)
        {
            var ShiftSetting = await _mediator.Send(new GetShiftSettingDetailRequest { Id = id });
            return Ok(ShiftSetting);
        }

        [HttpPost]
        [Route("save-ShiftSetting")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateShiftSettingDto ShiftSetting)
        {
            var command = new CreateShiftSettingCommand { ShiftSettingDto = ShiftSetting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-ShiftSetting/{id}")]
        public async Task<ActionResult> Put([FromForm] CreateShiftSettingDto ShiftSetting)
        {
            var command = new UpdateShiftSettingCommand { ShiftSettingDto = ShiftSetting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesDefaultResponseType]
        [Route("delete-ShiftSetting/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteShiftSettingCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }

}