using Hrm.Application;
using Hrm.Application.DTOs.GradeClass;
using Hrm.Application.DTOs.GradeClass;
using Hrm.Application.DTOs.GradeType;
using Hrm.Application.Features.GradeClasss.Requests.Queries;
using Hrm.Application.Features.GradeClass.Requests.Commands;
using Hrm.Application.Features.GradeClass.Requests.Queries;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.GradeClass)]
    [ApiController]
    [Authorize]
    public class GradeClassController : ControllerBase
    {

        private readonly IMediator _mediator;
        public GradeClassController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-gradeClass")]
        public async Task<ActionResult> Get()
        {
            var country = await _mediator.Send(new GetGradeClassRequest { });
            return Ok(country);
        }
        [HttpGet]
        [Route("get-gradeClassDetail/{id}")]
        public async Task<ActionResult<GradeClassDto>> Get(int id)
        {
            var GradeClasss = await _mediator.Send(new GetGradeClassDetailRequest { GradeClassId = id });
            return Ok(GradeClasss);
        }
        [HttpGet]
        [Route("get-selectedGradeClasss")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedGradeClass()
        {
            var GradeClass = await _mediator.Send(new GetSelectGradeClassRequest { });
            return Ok(GradeClass);
        }
        [HttpPost]
        [Route("save-gradeClass")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGradeClassDto gradeClass)
        {
            var command = new CreateGradeClassCommand { GradeClassDto = gradeClass };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-gradeClass/{id}")]
        public async Task<ActionResult> Put([FromBody] GradeClassDto gradeClass)
        {
            var command = new UpdateGradeClassCommand { GradeClassDto = gradeClass };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpDelete]
        [Route("delete-gradeClass/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGradeClassCommand { GradeClassId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
