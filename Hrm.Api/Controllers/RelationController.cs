﻿using Hrm.Application;
using Hrm.Application.DTOs.Relation;
using Hrm.Application.Features.Relation.Requests.Commands;
using Hrm.Application.Features.Relation.Requests.Queries;
using Hrm.Application.Features.Relations.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Relation)]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRelationDto Relation)
        {
            var command = new CreateRelationCommand { RelationDto = Relation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-relation")]
        public async Task<ActionResult> Get()
        {
            var country = await _mediator.Send(new GetRelationRequest { });
            return Ok(country);
        }
        [HttpGet]
        [Route("get-relationDetail/{id}")]
        public async Task<ActionResult<RelationDto>> Get(int id)
        {
            var Relations = await _mediator.Send(new GetRelationDetailRequest { RelationId = id });
            return Ok(Relations);
        }

        [HttpGet]
        [Route("get-selectedRelation")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedRelation()
        {
            var Relations = await _mediator.Send(new GetSelectRelationRequest { });
            return Ok(Relations);
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
