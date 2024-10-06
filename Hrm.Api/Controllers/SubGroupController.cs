using Hrm.Application;
using Hrm.Application.DTOs.Group;
using Hrm.Application.DTOs.Group;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Group.Requests.Queries;
using Hrm.Application.Features.Group.Requests.Commands;
using Hrm.Application.Features.Group.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.SubGroup)]
    [ApiController]
    [Authorize]
    public class SubGroupController : Controller
    {
        private readonly IMediator _mediator;
        public SubGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-subgroup")]
        public async Task<ActionResult> GetGroup()
        {
            var Group = await _mediator.Send(new GetGroupRequest { });
            return Ok(Group);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-subgroup")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGroupDto Group)
        {
            var command = new CreateGroupCommand { GroupDto = Group };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-subgroup/{id}")]
        public async Task<ActionResult> Put([FromBody] GroupDto Group)
        {
            var command = new UpdateGroupCommand { GroupDto = Group };
           var response= await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-subgroup/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGroupCommand { GroupId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-subgroupbyid/{id}")]
        public async Task<ActionResult<GroupDto>> Get(int id)
        {
            var Group = await _mediator.Send(new GetGroupByIdRequest { GroupId = id });
            return Ok(Group);

        }

        [HttpGet]
        [Route("get-selectedsubgroupByExamType/{id}")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedGroup(int id)
        {
            var group = await _mediator.Send(new GetSelectedGroupRequest { Id = id });
            return Ok(group);
        }
    }
}
