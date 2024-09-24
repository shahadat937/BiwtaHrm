using Hrm.Application;
using Hrm.Application.DTOs.ResponsibilityType;
using Hrm.Application.Features.ResponsibilityType.Requests.Commands;
using Hrm.Application.Features.ResponsibilityType.Requests.Queries;
using Hrm.Application.Features.ResponsibilityTypes.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.ResponsibilityType)]
    [ApiController]
    [Authorize]
    public class ResponsibilityTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResponsibilityTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-ResponsibilityType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateResponsibilityTypeDto ResponsibilityType)
        {
            var command = new CreateResponsibilityTypeCommand { ResponsibilityTypeDto = ResponsibilityType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-ResponsibilityType")]
        public async Task<ActionResult> Get()
        {
            var ResponsibilityType = await _mediator.Send(new GetResponsibilityTypeRequest { });
            return Ok(ResponsibilityType);
        }
        [HttpGet]
        [Route("get-ResponsibilityTypeDetail/{id}")]
        public async Task<ActionResult<ResponsibilityTypeDto>> Get(int id)
        {
            var ResponsibilityTypes = await _mediator.Send(new GetResponsibilityTypeDetailRequest { ResponsibilityTypeId = id });
            return Ok(ResponsibilityTypes);
        }
        [HttpGet]
        [Route("get-selectedResponsibilityTypes")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedResponsibilityType()
        {
            var ResponsibilityType = await _mediator.Send(new GetSelectedResponsibilityTypeRequest { });
            return Ok(ResponsibilityType);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-ResponsibilityType/{id}")]
        public async Task<ActionResult> Put([FromBody] ResponsibilityTypeDto ResponsibilityType)
        {
            var command = new UpdateResponsibilityTypeCommand { ResponsibilityTypeDto = ResponsibilityType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-ResponsibilityType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteResponsibilityTypeCommand { ResponsibilityTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}