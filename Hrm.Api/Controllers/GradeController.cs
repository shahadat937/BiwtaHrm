using Hrm.Application;
using Hrm.Application.DTOs.Grade;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Features.Grade.Requests.Commands;
using Hrm.Application.Features.Grade.Requests.Queries;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Queries;
using Hrm.Shared.Models;
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


        [HttpGet]
        [Route("get-grade")]
        public async Task<ActionResult> Get()
        {
            var country = await _mediator.Send(new GetGradeRequest { });
            return Ok(country);
        }

        [HttpGet]
        [Route("get-selectedGrade")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedGrade()
        {
            var grades = await _mediator.Send(new GetSelectGradeRequest { });
            return Ok(grades);
        }

        [HttpPost]
        [Route("save-grade")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGradeDto grade)
        {
            var command = new CreateGradeCommand { GradeDto = grade };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-grade/{id}")]
        public async Task<ActionResult> Put([FromBody] GradeDto grade)
        {
            var command = new UpdateGradeCommand { GradeDto = grade };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [Route("delete-grade/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGradeCommand { GradeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
