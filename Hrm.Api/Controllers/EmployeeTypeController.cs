using Hrm.Application;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Application.Features.EmployeeType.Handlers.Queries;
using Hrm.Application.Features.EmployeeType.Requests.Commands;
using Hrm.Application.Features.EmployeeType.Requests.Queries;
using Hrm.Application.Features.ExamType.Requests.Queries;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.EmployeeType)]
    [ApiController]
    public class EmployeeTypeController : Controller
    {
        private readonly IMediator _mediator;
        public EmployeeTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-employeeType")]
        public async Task<ActionResult> GetEmployeeType()
        {
            var EmployeeType = await _mediator.Send(new GetEmployeeTypeRequest { });
            return Ok(EmployeeType);
        }
        [HttpGet]
        [Route("get-employeeTypeById/{id}")]
        public async Task<ActionResult<EmployeeTypeDto>> Get(int id)
        {
            var EmployeeType = await _mediator.Send(new GetEmployeeTypeByIdRequest { EmployeeTypeId = id });
            return Ok(EmployeeType);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-employeeType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmployeeTypeDto EmployeeType)
        {
            var command = new CreateEmployeeTypeCommand { EmployeeTypeDto = EmployeeType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-selectedEmployeeType")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedEmployeeType()
        {
            var EmployeeType = await _mediator.Send(new GetSelectedEmployeeTypeRequest { });
            return Ok(EmployeeType);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-employeeType/{id}")]
        public async Task<ActionResult> Put([FromBody] EmployeeTypeDto EmployeeType)
        {
            var command = new UpdateEmployeeTypeCommand { EmployeeTypeDto = EmployeeType };
          var response=  await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-employeeType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmployeeTypeCommand { EmployeeTypeId = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
