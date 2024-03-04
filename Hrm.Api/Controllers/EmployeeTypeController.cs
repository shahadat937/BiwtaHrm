using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.EmployeeType.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmployeeType)]
    [ApiController]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-employeeType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmployeeTypeDto employeeType)
        {
            var command = new CreateEmployeeCommand { EmployeeTypeDto = employeeType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
