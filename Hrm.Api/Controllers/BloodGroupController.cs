using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.BloodGroup.Requests.Queries;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.BloodGroup)]
    [ApiController]
    public class BloodGroupController : Controller
    {
        private readonly IMediator _mediator;
        public BloodGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-bloodGroup")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBloodGroupDto bloodGroup)
        {
            var command = new CreateBloodCommand { BloodGroupDto = bloodGroup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-bloodGroup")]
        public async Task<ActionResult> Get()
        {
            var BloodGroup = await _mediator.Send(new GetBloodGroupRequest { });
            return Ok(BloodGroup);
        }
        [HttpGet]
        [Route("get-bloodGroupDetail/{id}")]
        public async Task<ActionResult<BloodGroupDto>> Get(int id)
        {
            var BloodGroups = await _mediator.Send(new GetBloodGroupDetailRequest { BloodGroupId = id });
            return Ok(BloodGroups);
        }
        [HttpGet]
        [Route("get-selectedBloodGroups")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedBloodGroup()
        {
            var bloodgroup = await _mediator.Send(new GetSelectedBloodGroupRequest { });
            return Ok(bloodgroup);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-bloodGroup/{id}")]
        public async Task<ActionResult> Put([FromBody] BloodGroupDto bloodGroup)
        {
            var command = new UpdateBloodGroupCommand { BloodGroupDto = bloodGroup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-bloodGroup/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBloodGroupCommand { BloodGroupId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
