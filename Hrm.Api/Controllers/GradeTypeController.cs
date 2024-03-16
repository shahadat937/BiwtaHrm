using Hrm.Application;
using Hrm.Application.DTOs.Country;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Features.Country.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.GradeType)]
    [ApiController]
    public class GradeTypeController : ControllerBase
    {

        private readonly IMediator _mediator;
        public GradeTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("save-gradeType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGradeTypeDto gradeType)
        {
            var command = new CreateGradeTypeCommand { GradeTypeDto = gradeType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
