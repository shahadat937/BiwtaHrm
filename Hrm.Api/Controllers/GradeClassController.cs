using Hrm.Application;
using Hrm.Application.DTOs.GradeClass;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Features.GradeClass.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.GradeClass)]
    [ApiController]
    public class GradeClassController : ControllerBase
    {

        private readonly IMediator _mediator;
        public GradeClassController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("save-gradeClass")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGradeClassDto gradeClass)
        {
            var command = new CreateGradeClassCommand { GradeClassDto = gradeClass };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
