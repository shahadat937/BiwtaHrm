using Hrm.Application;
using Hrm.Application.DTOs.EmployeeType;
using Hrm.Application.Features.EmployeeType.Requests.Commands;
using Hrm.Application.Responses;
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
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-employeeType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmployeeTypeDto EmployeeType)
        {
            var command = new CreateEmployeeCommand { EmployeeTypeDto = EmployeeType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
