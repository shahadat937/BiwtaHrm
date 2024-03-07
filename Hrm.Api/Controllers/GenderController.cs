using Hrm.Application;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Features.Gender.Requests.Queries;
using Hrm.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Common;
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
        [Route("get-courses")]
        public async Task<ActionResult<List<GenderDto>>> Get([FromQuery] QueryParams queryParams)
        {
            var Courses = await _mediator.Send(new GetGenderListRequest { QueryParams = queryParams });
            return Ok(Courses);
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
    }
}
