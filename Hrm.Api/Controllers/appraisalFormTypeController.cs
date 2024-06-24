using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.appraisalFormType;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.appraisalFormType.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.appraisalFormType.Requests.Queries;
using Hrm.Application.DTOs.appraisalFormType;
using Hrm.Application.Features.appraisalFormType.Requests.Commands;
using Hrm.Application.Features.Stores.Requests.Commands;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.appraisalFormType)]
    [ApiController]
    public class appraisalFormTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public appraisalFormTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        [Route("save-appraisalFormType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateappraisalFormTypeDto appraisalFormType)
        {
            var command = new CreateappraisalFormTypeCommand { appraisalFormTypeDto = appraisalFormType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-appraisalFormType")]
        public async Task<ActionResult> Get()
        {
            var appraisalFormType = await _mediator.Send(new GetappraisalFormTypeRequest { });
            return Ok(appraisalFormType);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-appraisalFormType/{id}")]
        public async Task<ActionResult> Put([FromBody] appraisalFormTypeDto appraisalFormType)
        {
            var command = new UpdateappraisalFormTypeCommand { appraisalFormTypeDto = appraisalFormType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-appraisalFormType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteappraisalFormTypeCommand { appraisalFormTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
