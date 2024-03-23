using Hrm.Application;
using Hrm.Application.DTOs.Relation; 
using Hrm.Application.Features.Relation.Requests.Commands;
using Hrm.Application.Features.Relation.Requests.Queries;
using Hrm.Application.Responses;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Relation)]
    [ApiController]
    public class RelationController : Controller
    {
        private readonly IMediator _mediator;
        public RelationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Relation")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRelationDto relationDto)
        {
            var command = new CreateRelationCommand { RelationDto = relationDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-Relation")]
        public async Task<ActionResult> Get()
        {
            var Relation = await _mediator.Send(new GetRelationRequest { });
            return Ok(Relation);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-Relation/{id}")]
        public async Task<ActionResult> Put([FromBody] RelationDto Relation)
        {
            var command = new UpdateRelationCommand { RelationDto = Relation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Relation/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteRelationCommand { RelationId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
