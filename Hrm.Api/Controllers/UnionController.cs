﻿using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.District;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Thana.Requests.Queries;
using Hrm.Application.Features.Union.Requests.Commands;
using Hrm.Application.Features.Union.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Union)]
    [ApiController]
    [Authorize]
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
        [HttpGet]
        [Route("get-unionByThanaId/{thanaId}")]
        public async Task<ActionResult<List<SelectedModel>>> GetUnionByThanaId(int thanaId)
        {

            var thanas = await _mediator.Send(new GetUnionByThanaIdRequest
            {
                ThanaId = thanaId
            });
            return Ok(thanas);

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
        [Route("delete-union/{id}")]
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

        [HttpGet]
        [Route("get-unionbyid/{id}")]
        public async Task<ActionResult<UnionDto>> Get(int id)
        {
            var Union = await _mediator.Send(new GetUnionByIdRequest { UnionId = id });
            return Ok(Union);

        }

        [HttpGet]
        [Route("get-selectedUnion")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedUnion()
        {
            var Union = await _mediator.Send(new GetSelectedUnionRequest { });
            return Ok(Union);
        }


    }
}
