using Hrm.Application;
using Hrm.Application.DTOs.District;
using Hrm.Application.Features.District.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.District)]
    [ApiController]
    public class DistrictController : Controller
    {
        private readonly IMediator _mediator;
        public DistrictController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-district")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDistrictDto District)
        {
            var command = new CreateDistrictCommand { DistrictDto = District };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
