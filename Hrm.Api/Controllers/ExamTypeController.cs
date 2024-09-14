using Hrm.Application;
using Hrm.Application.DTOs.ExamType;
using Hrm.Application.Features.ExamType.Requests.Commands;
using Hrm.Application.Features.ExamType.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.ExamType)]
    [ApiController]
    [Authorize]
    public class ExamType : Controller
    {
        private readonly IMediator _mediator;
        public ExamType(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-ExamType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateExamTypeDto ExamType)
        {
            var command = new CreateExamTypeCommand { ExamTypeDto = ExamType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-ExamType")]
        public async Task<ActionResult> Get()
        {
            var ExamType = await _mediator.Send(new GetExamTypeRequest { });
            return Ok(ExamType);
        }

        [HttpGet]
        [Route("get-ExamTypebyid/{id}")]
        public async Task<ActionResult<ExamTypeDto>> Get(int id)
        {
            var ExamType = await _mediator.Send(new GetExamTypeByIdRequest { ExamTypeId = id });
            return Ok(ExamType);

        }

        [HttpGet]
        [Route("get-selectedExamType")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedExamType()
        {
            var ExamType = await _mediator.Send(new GetSelectedExamTypeRequest { });
            return Ok(ExamType);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-ExamType/{id}")]
        public async Task<ActionResult> Put([FromBody] ExamTypeDto ExamType)
        {
            var command = new UpdateExamTypeCommand { ExamTypeDto = ExamType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-ExamType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteExamTypeCommand { ExamTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
