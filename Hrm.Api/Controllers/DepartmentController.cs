using Hrm.Application;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.Department.Requests.Queries;
using Hrm.Application.Features.Department.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
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


        [HttpGet]
        [Route("get-departmentbyid/{id}")]
        public async Task<ActionResult<DepartmentDto>> Get(int id)
        {
            var Department = await _mediator.Send(new GetDepartmentByIdRequest { DepartmentId = id });
            return Ok(Department);

        }

        [HttpGet]
        [Route("get-selecteddepartment")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDepartment()
        {
            var department = await _mediator.Send(new GetSelectedDepartmentRequest { });
            return Ok(department);
        }

    }
}
