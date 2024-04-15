using Hrm.Application;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.ChildStatus.Requests.Queries;
using Hrm.Application.Features.EmployeeType.Requests.Queries;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Features.Gender.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Gender)]
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IMediator _mediator;
        public GenderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-gender")]
        public async Task<ActionResult> GetGender()
        {
            var Gender = await _mediator.Send(new GetGenderRequest { });
            return Ok(Gender);
        }
        [HttpGet]
        [Route("get-genderById/{id}")]
        public async Task<ActionResult<GenderDto>> Get(int id)
        {
            var Gender = await _mediator.Send(new GetGenderByIdRequest { GenderId = id });
            return Ok(Gender);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-gender")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGenderDto Gender)
        {
            var command = new CreateGenderCommand { GenderDto = Gender };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-gender/{id}")]
        public async Task<ActionResult> Put([FromBody] GenderDto Gender)
        {
            var command = new UpdateGenderCommand { GenderDto = Gender };
           var response= await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-gender/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGenderCommand { GenderId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
