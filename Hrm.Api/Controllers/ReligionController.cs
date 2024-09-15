using Hrm.Application;
using Hrm.Application.DTOs.Religion;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Religion.Requests.Commands;
using Hrm.Application.Features.Religion.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Religion)]
    [ApiController]
    [Authorize]
    public class ReligionController : Controller
    {
        private readonly IMediator _mediator;
        public ReligionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-religion")]
        public async Task<ActionResult> GetReligion()
        {
            var Religion = await _mediator.Send(new GetReligionRequest { });
            return Ok(Religion);
        }
        [HttpGet]
        [Route("get-religionById/{id}")]
        public async Task<ActionResult<ReligionDto>> Get(int id)
        {
            var Religion = await _mediator.Send(new GetReligionByIdRequest { ReligionId = id });
            return Ok(Religion);
        }

        [HttpGet]
        [Route("get-selectedReligions")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedReligion()
        {
            var Religion = await _mediator.Send(new GetSelectedReligionRequest { });
            return Ok(Religion);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-religion")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateReligionDto Religion)
        {
            var command = new CreateReligionCommand { ReligionDto = Religion };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-religion/{id}")]
        public async Task<ActionResult> Put([FromBody] ReligionDto Religion)
        {
            var command = new UpdateReligionCommand { ReligionDto = Religion };
           var response = await _mediator.Send(command);
            return Ok(response); 
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-religion/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteReligionCommand { ReligionId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
