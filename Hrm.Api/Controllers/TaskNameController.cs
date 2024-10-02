using Hrm.Application;
using Hrm.Application.DTOs.TaskName;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Features.TaskName.Requests.Commands;
using Hrm.Application.Features.TaskName.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.TaskName)]

    [ApiController]
    [Authorize]
    public class TaskNameController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaskNameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-taskname")]
        public async Task<ActionResult> GetTaskName()
        {
            var TaskName = await _mediator.Send(new GetTaskNameRequest { });
            return Ok(TaskName);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-taskname")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTaskNameDto TaskName)
        {
            var command = new CreateTaskNameCommand { TaskNameDto = TaskName };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-taskname/{id}")]
        public async Task<ActionResult> Put([FromBody] TaskNameDto TaskName)
        {
            var command = new UpdateTaskNameCommand { TaskNameDto = TaskName };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-taskname/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteTaskNameCommand { TaskNameId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }
}
