using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Union.Requests.Commands;
using Hrm.Application.Features.Union.Requests.Queries;
using Hrm.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Union)]
    [ApiController]
    public class UnionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UnionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-union")]
        public async Task<ActionResult> Get()
        {
            var Union = await _mediator.Send(new GetUnionRequest { });
            return Ok(Union);
        }

        [HttpPost]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        [Route("save-union")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateUnionDto Union)
        {
            var command = new CreateUnionCommand { UnionDto = Union };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-union")]
        public async Task<ActionResult>Delete (int id)
        {
            var command = new DeleteUnionCommand { UnionId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-union/{id}")]
        public async Task<ActionResult> Put([FromBody] UnionDto Union)
        {
            var command = new UpdateUnionCommand { UnionDto = Union };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }
}
