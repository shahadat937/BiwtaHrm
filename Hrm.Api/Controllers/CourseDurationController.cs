using Hrm.Application;
using Hrm.Application.DTOs.CourseDuration;
using Hrm.Application.Features.CourseDurations.Requests.Commands;
using Hrm.Application.Features.CourseDurations.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.CourseDuration)]
    [ApiController]
    [Authorize]
    public class CourseDurationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CourseDurationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-CourseDuration")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCourseDurationDto CourseDuration)
        {
            var command = new CreateCourseDurationCommand { CourseDurationDto = CourseDuration };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-CourseDuration")]
        public async Task<ActionResult> Get()
        {
            var CourseDuration = await _mediator.Send(new GetCourseDurationRequest { });
            return Ok(CourseDuration);
        }
        [HttpGet]
        [Route("get-CourseDurationDetail/{id}")]
        public async Task<ActionResult<CourseDurationDto>> Get(int id)
        {
            var CourseDurations = await _mediator.Send(new GetCourseDurationDetailRequest { CourseDurationId = id });
            return Ok(CourseDurations);
        }
        [HttpGet]
        [Route("get-selectedCourseDurations")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedCourseDuration()
        {
            var CourseDuration = await _mediator.Send(new GetSelectedCourseDurationRequest { });
            return Ok(CourseDuration);
        }

        [HttpPut]
        [Route("update-CourseDuration/{id}")]
        public async Task<ActionResult> Put([FromBody] CourseDurationDto CourseDuration)
        {
            var command = new UpdateCourseDurationCommand { CourseDurationDto = CourseDuration };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [Route("delete-CourseDuration/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCourseDurationCommand { CourseDurationId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}