using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Thana_Upazila;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Thana_Upazila.Requests.Commands;
using Hrm.Application.Features.Thana_Upazila.Requests.Queries;
using Hrm.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Thana_Upazila)]
    [ApiController]
    public class Thana_UpazilaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public Thana_UpazilaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-thana_upazila")]
        public async Task<ActionResult> Get()
        {
            var Thana_Upazila = await _mediator.Send(new GetThana_UpazilaRequest { });
            return Ok(Thana_Upazila);
        }

        [HttpPost]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        [Route("save-thana_upazila")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateThana_UpazilaDto Thana_Upazila)
        {
            var command = new CreateThana_UpazilaCommand { Thana_UpazilaDto = Thana_Upazila };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-thana-upazila")]
        public async Task<ActionResult>Delete (int id)
        {
            var command = new DeleteThana_UpazilaCommand { Thana_UpazilaId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-thana-upazila/{id}")]
        public async Task<ActionResult> Put([FromBody] Thana_UpazilaDto Thana_Upazila)
        {
            var command = new UpdateThana_UpazilaCommand { Thana_UpazilaDto = Thana_Upazila };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }
}
