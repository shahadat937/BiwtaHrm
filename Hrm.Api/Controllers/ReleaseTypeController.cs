using Hrm.Application;
using Hrm.Application.DTOs.ReleaseType;
using Hrm.Application.Features.ReleaseTypes.Requests.Commands;
using Hrm.Application.Features.ReleaseTypes.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.ReleaseType)]
    [ApiController]
    [Authorize]
    public class ReleaseTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReleaseTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-ReleaseType")]
        public async Task<ActionResult> GetReleaseType()
        {
            var ReleaseType = await _mediator.Send(new GetReleaseTypeRequest { });
            return Ok(ReleaseType);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-ReleaseType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateReleaseTypeDto ReleaseType)
        {
            var command = new CreateReleaseTypeCommand { ReleaseTypeDto = ReleaseType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-ReleaseType/{id}")]
        public async Task<ActionResult> Put([FromBody] ReleaseTypeDto ReleaseType)
        {
            var command = new UpdateReleaseTypeCommand { ReleaseTypeDto = ReleaseType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-ReleaseType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteReleaseTypeCommand { ReleaseTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-ReleaseTypebyid/{id}")]
        public async Task<ActionResult<ReleaseTypeDto>> Get(int id)
        {
            var ReleaseType = await _mediator.Send(new GetReleaseTypeByIdRequest { ReleaseTypeId = id });
            return Ok(ReleaseType);

        }

        [HttpGet]
        [Route("get-selectedReleaseType")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedReleaseType()
        {
            var ReleaseType = await _mediator.Send(new GetSelectedReleaseTypeRequest { });
            return Ok(ReleaseType);
        }
    }
}
