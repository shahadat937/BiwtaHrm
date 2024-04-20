using Hrm.Application;
using Hrm.Application.DTOs.Occupation;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Occupation.Requests.Commands;
using Hrm.Application.Features.Occupation.Requests.Queries;
using Hrm.Application.Features.Occupations.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Occupation)]
    [ApiController]
    public class OccupationController : Controller
    {
        private readonly IMediator _mediator;
        public OccupationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-occupation")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOccupationDto Occupation)
        {
            var command = new CreateBloodCommand { OccupationDto = Occupation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-occupation")]
        public async Task<ActionResult> Get()
        {
            var Occupation = await _mediator.Send(new GetOccupationRequest { });
            return Ok(Occupation);
        }
        [HttpGet]
        [Route("get-occupationDetail/{id}")]
        public async Task<ActionResult<OccupationDto>> Get(int id)
        {
            var Occupations = await _mediator.Send(new GetOccupationDetailRequest { OccupationId = id });
            return Ok(Occupations);
        }
        [HttpGet]
        [Route("get-selectedOccupations")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedOccupation()
        {
            var Occupation = await _mediator.Send(new GetSelectedOccupationRequest { });
            return Ok(Occupation);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-occupation/{id}")]
        public async Task<ActionResult> Put([FromBody] OccupationDto Occupation)
        {
            var command = new UpdateOccupationCommand { OccupationDto = Occupation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-occupation/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOccupationCommand { OccupationId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
