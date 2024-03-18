using Hrm.Application;
using Hrm.Application.DTOs.Grade;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Features.Grade.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Grade)]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GradeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("save-grade")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGradeDto grade)
        {
            var command = new CreateGradeCommand { GradeDto = grade };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
