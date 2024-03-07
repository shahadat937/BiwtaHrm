using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.MaritalStatus)]
    [ApiController]
    public class MaritalStatusController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MaritalStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        [Route("save-maritalStatus")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateMaritalStatusDto maritalStatus)
        {
            var command = new CreateMaritalStatusCommand { MaritalStatusDto = maritalStatus };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-maritalStatus")]
        public async Task<ActionResult> GetMaritalStatus()
        {
            var MaritalStatus = await _mediator.Send(new GetMaritalStatusRequest { });
            return Ok(MaritalStatus);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-maritalStatus/{id}")]
        public async Task<ActionResult> Put([FromBody] MaritalStatusDto MaritalStatus)
        {
            var command = new UpdateMaritalStatusCommand { MaritalStatusDto = MaritalStatus };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
