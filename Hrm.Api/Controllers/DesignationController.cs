using Hrm.Application;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Features.Designations.Requests.Queries;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Designation)]
    [ApiController]
    public class DesignationController : Controller
    {
        private readonly IMediator _mediator;
        public DesignationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-designationDetail/{id}")]
        public async Task<ActionResult<DesignationDto>> Get(int id)
        {
            var Designations = await _mediator.Send(new GetDesignationDetailRequest { DesignationId = id });
            return Ok(Designations);
        }
        [HttpGet]
        [Route("get-selectedDesignations")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDesignation()
        {
            var Designation = await _mediator.Send(new GetSelectedDesignationRequest { });
            return Ok(Designation);
        }


        [HttpGet]
        [Route("get-designation")]
        public async Task<ActionResult> GetDesignation()
        {
            var Designation = await _mediator.Send(new GetDesignationRequest { });
            return Ok(Designation);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-designation")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDesignationDto Designation)
        {
            var command = new CreateDesignationCommand { DesignationDto = Designation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-Designation/{id}")]
        public async Task<ActionResult> Put([FromBody] DesignationDto Designation)
        {
            var command = new UpdateDesignationCommand { DesignationDto = Designation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Designation/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDesignationCommand { DesignationId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
