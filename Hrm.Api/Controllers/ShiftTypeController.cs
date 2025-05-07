using Hrm.Application.DTOs.ShiftType;
using Hrm.Application.Features.ShiftTypes.Requests.Commands;
using Hrm.Application.Features.ShiftTypes.Requests.Queries;
using Hrm.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Shared.Models;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.ShiftType)]
    [ApiController]
    [Authorize]
    public class ShiftTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShiftTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-ShiftTypes")]
        public async Task<ActionResult<List<ShiftTypeDto>>> Get()
        {
            var ShiftTypes = await _mediator.Send(new GetShiftTypeListRequest { });
            return Ok(ShiftTypes);
        }

        [HttpGet]
        [Route("get-TreeShiftTypes")]
        public async Task<ActionResult<List<TreeShiftTypeDto>>> GetTreeShiftType()
        {
            var ShiftTypes = await _mediator.Send(new GetTreeShiftTypeRequest { });
            return Ok(ShiftTypes);
        }

        [HttpGet]
        [Route("get-selectedShiftTypes")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedShiftType()
        {
            var ShiftTypes = await _mediator.Send(new GetSelectedShiftTypeRequest { });
            return Ok(ShiftTypes);
        }

        [HttpGet]
        [Route("get-ActiveShiftTypes")]
        public async Task<ActionResult<ShiftTypeDto>> GetActive()
        {
            var ShiftTypes = await _mediator.Send(new GetActiveShiftTypeRequest { });
            return Ok(ShiftTypes);
        }

        [HttpGet]
        [Route("get-ShiftTypeDetail/{id}")]
        public async Task<ActionResult<ShiftTypeDto>> Get(int id)
        {
            var ShiftType = await _mediator.Send(new GetShiftTypeDetailRequest { Id = id });
            return Ok(ShiftType);
        }

        [HttpPost]
        [Route("save-ShiftType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateShiftTypeDto ShiftType)
        {
            var command = new CreateShiftTypeCommand { ShiftTypeDto = ShiftType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-ShiftType/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateShiftTypeDto ShiftType)
        {
            var command = new UpdateShiftTypeCommand { ShiftTypeDto = ShiftType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesDefaultResponseType]
        [Route("delete-ShiftType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteShiftTypeCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }

}


