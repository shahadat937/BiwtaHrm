using Hrm.Application;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.Department.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Department)]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-department")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDepartmentDto Department)
        {
            var command = new CreateDepartmentCommand { DepartmentDto = Department };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-department")]
        public async Task<ActionResult> Get()
        {
            var Department = await _mediator.Send(new GetDepartmentRequest { });
            return Ok(Department);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-department/{id}")]
        public async Task<ActionResult> Put([FromBody] DepartmentDto Department)
        {
            var command = new UpdateDepartmentCommand { DepartmentDto = Department };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-department/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDepartmentCommand { DepartmentId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
