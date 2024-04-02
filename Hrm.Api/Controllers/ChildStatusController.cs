using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.ChildStatus.Requests.Commands;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.ChildStatus)]
    [ApiController]
    public class ChildStatusController : Controller
    {
        private readonly IMediator _mediator;
        public ChildStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-ChildStatus")]
        public async Task<ActionResult> GetChildStatus()
        {
            var ChildStatus = await _mediator.Send(new GetChildStatusRequest { });
            return Ok(ChildStatus);
        }
        [HttpGet]
        [Route("get-ChildStatusById/{id}")]
        public async Task<ActionResult<ChildStatusDto>> Get(int id)
        {
            var BloodGroups = await _mediator.Send(new GetChildStatusGetByIdRequest { ChildStatusId = id });
            return Ok(BloodGroups);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-ChildStatus")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateChildStatusDto ChildStatus)
        {
            var command = new CreateChildStatusCommand { ChildStatusDto = ChildStatus };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-ChildStatus/{id}")]
        public async Task<ActionResult> Put([FromBody] ChildStatusDto ChildStatus)
        {
            var command = new UpdateChildStatusCommand { ChildStatusDto = ChildStatus };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-ChildStatus/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteChildStatusCommand { ChildStatusId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
