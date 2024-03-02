using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Responses;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.BloodGroup)]
    [ApiController]
    public class BloodGroupController : Controller
    {
        private readonly IMediator _mediator;
        public BloodGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-bloodGroup")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBloodGroupDto bloodGroup)
        {
            var command = new CreateBloodCommand { BloodGroupDto = bloodGroup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
