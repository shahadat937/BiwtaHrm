using Hrm.Application;
using Hrm.Application.DTOs.Employee;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.Features.Employee.Requests.Commands;
using Hrm.Application.Features.Employee.Requests.Queries;
using Hrm.Application.Features.EmployeeType.Handlers.Queries;
using Hrm.Application.Features.EmployeeType.Requests.Commands;
using Hrm.Application.Features.EmployeeType.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Employee)]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-allEmployee")]
        public async Task<ActionResult> GetEmployee()
        {
            var Employee = await _mediator.Send(new GetAllEmployeeRequest { });
            return Ok(Employee);
        }

        [HttpGet]
        [Route("get-employeeById/{id}")]
        public async Task<ActionResult<EmployeesDto>> GetEmployeeById(int id)
        {
            var Employee = await _mediator.Send(new GetEmployeeByIdRequest { EmpId = id });
            return Ok(Employee);
        }

        [HttpGet]
        [Route("get-employeeByAspNetUserId/{id}")]
        public async Task<ActionResult<EmployeesDto>> GetEmployeeByAspNetUserId(string id)
        {
            var Employee = await _mediator.Send(new GetEmployeeByAspNetUserIdRequest { AspNetUserId = id });
            return Ok(Employee);
        }

        [HttpPost]
        [Route("save-employee")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmployeesDto Employee)
        {
            var command = new CreateEmployeeCommand { EmployeeDto = Employee };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-employee/{id}")]
        public async Task<ActionResult> Put([FromBody] EmployeesDto Employee)
        {
            var command = new UpdateEmployeeCommand { EmployeesDto = Employee };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
