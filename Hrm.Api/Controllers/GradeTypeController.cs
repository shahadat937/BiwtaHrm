using Hrm.Application;
using Hrm.Application.DTOs.Country;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Features.Country.Requests.Commands;
using Hrm.Application.Features.Country.Requests.Queries;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Queries;
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


        [HttpGet]
        [Route("get-gradeType")]
        public async Task<ActionResult> Get()
        {
            var country = await _mediator.Send(new GetGradeTypeReuqest { });
            return Ok(country);
        }

        [HttpPost]
        [Route("save-gradeType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGradeTypeDto gradeType)
        {
            var command = new CreateGradeTypeCommand { GradeTypeDto = gradeType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-gradeType/{id}")]
        public async Task<ActionResult> Put([FromBody] GradeTypeDto gradeType)
        {
            var command = new UpdateGradeTypeCommand { GradeTypeDto = gradeType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [Route("delete-gradeType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGradeTypeCommand { GradeTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
