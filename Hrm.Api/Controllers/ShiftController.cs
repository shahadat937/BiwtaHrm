using Hrm.Application;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Shift.Requests.Commands;
using Hrm.Application.Features.Shift.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.DTOs.Shift;
using Hrm.Application.Features.Shift.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Shift)]
    [ApiController]
    [Authorize]
    public class ShiftController : Controller
    {
        private readonly IMediator _mediator;
        public ShiftController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Shift")]
        public async Task<ActionResult> GetShift()
        {
            var Shift = await _mediator.Send(new GetShiftRequest { });
            return Ok(Shift);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Shift")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateShiftDto Shift)
        {
            var command = new CreateShiftCommand { ShiftDto = Shift };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-Shift/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateShiftDto Shift)
        {
            var command = new UpdateShiftCommand { ShiftDto = Shift };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Shift/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteShiftCommand { ShiftId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-shiftbyid/{id}")]
        public async Task<ActionResult<ShiftDto>> Get(int id)
        {
            var Shift = await _mediator.Send(new GetShiftByIdRequest { ShiftId = id });
            return Ok(Shift);

        }

        [HttpGet]
        [Route("get-selectedshift")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedShift()
        {
            var shift = await _mediator.Send(new GetSelectedShiftRequest { });
            return Ok(shift);
        }
    }
}
