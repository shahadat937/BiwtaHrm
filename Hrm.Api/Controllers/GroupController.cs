using Hrm.Application;
using Hrm.Application.DTOs.Group;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Group.Requests.Commands;
using Hrm.Application.Features.Group.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Group)]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-group")]
        public async Task<ActionResult> GetGroup()
        {
            var Group = await _mediator.Send(new GetGroupRequest { });
            return Ok(Group);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-group")]
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
        [Route("update-group/{id}")]
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
        [Route("delete-group/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGroupCommand { GroupId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
