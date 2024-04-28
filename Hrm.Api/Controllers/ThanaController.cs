using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Thana;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.Thana.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Thana.Requests.Commands;
using Hrm.Application.Features.Thana.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.DTOs.District;
using Hrm.Application.Features.District.Requests.Queries;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Thana)]
    [ApiController]
    public class ThanaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ThanaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-Thana")]
        public async Task<ActionResult<List<ThanaDto>>> Get()
        {
            var Thana = await _mediator.Send(new GetThanaRequest { });
            return Ok(Thana);
        }
        [HttpGet]
        [Route("get-thanaByUpazilaId")]
        public async Task<ActionResult<List<ThanaDto>>> GetThanaByUpazilaId(int upazilaId)
        {

            var thanas = await _mediator.Send(new GetThanaByUpazilaIdRequest
            {
                UpazilaId = upazilaId
            });
            return Ok(thanas);

        }
        [HttpPost]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        [Route("save-Thana")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateThanaDto Thana)
        {
            var command = new CreateThanaCommand { ThanaDto = Thana };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-thana")]
        public async Task<ActionResult>Delete (int id)
        {
            var command = new DeleteThanaCommand { ThanaId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-thana/{id}")]
        public async Task<ActionResult> Put([FromBody] ThanaDto Thana)
        {
            var command = new UpdateThanaCommand { ThanaDto = Thana };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpGet]
        [Route("get-thanabyid/{id}")]
        public async Task<ActionResult<ThanaDto>> Get(int id)
        {
            var Thana = await _mediator.Send(new GetThanaByIdRequest { ThanaId = id });
            return Ok(Thana);

        }

        [HttpGet]
        [Route("get-selectedThana")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedThana()
        {
            var Thana = await _mediator.Send(new GetSelectedThanaRequest { });
            return Ok(Thana);
        }


    }
}
