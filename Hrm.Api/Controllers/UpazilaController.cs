using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Upazila;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Upazila.Requests.Commands;
using Hrm.Application.Features.Upazila.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Upazila)]
    [ApiController]
    public class UpazilaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UpazilaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-upazila")]
        public async Task<ActionResult> Get()
        {
            var Upazila = await _mediator.Send(new GetUpazilaRequest { });
            return Ok(Upazila);
        }

        [HttpPost]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        [Route("save-upazila")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateUpazilaDto Upazila)
        {
            var command = new CreateUpazilaCommand { UpazilaDto = Upazila };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-upazila")]
        public async Task<ActionResult>Delete (int id)
        {
            var command = new DeleteUpazilaCommand { UpazilaId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-upazila/{id}")]
        public async Task<ActionResult> Put([FromBody] UpazilaDto Upazila)
        {
            var command = new UpdateUpazilaCommand { UpazilaDto = Upazila };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpGet]
        [Route("get-upazilabyid/{id}")]
        public async Task<ActionResult<UpazilaDto>> Get(int id)
        {
            var Upazila = await _mediator.Send(new GetUpazilaByIdRequest { UpazilaId = id });
            return Ok(Upazila);

        }

        [HttpGet]
        [Route("get-selectedUpazila")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedUpazila()
        {
            var Upazila = await _mediator.Send(new GetSelectedUpazilaRequest { });
            return Ok(Upazila);
        }



    }
}
