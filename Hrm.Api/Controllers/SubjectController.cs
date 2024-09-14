using Hrm.Application;
using Hrm.Application.DTOs.Subject;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Subject.Requests.Commands;
using Hrm.Application.Features.Subject.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Subject)]
    [ApiController]
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly IMediator _mediator;
        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Subject")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSubjectDto Subject)
        {
            var command = new CreateSubjectCommand { SubjectDto = Subject };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-Subject")]
        public async Task<ActionResult> Get()
        {
            var Subject = await _mediator.Send(new GetSubjectRequest { });
            return Ok(Subject);
        }
        [HttpGet]
        [Route("get-SubjectDetail/{id}")]
        public async Task<ActionResult<SubjectDto>> Get(int id)
        {
            var Subjects = await _mediator.Send(new GetSubjectByIdRequest  { SubjectId = id });
            return Ok(Subjects);
        }
        [HttpGet]
        [Route("get-selectedSubjects")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedSubject()
        {
            var Subject = await _mediator.Send(new GetSelectedSubjectRequest { });
            return Ok(Subject);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-Subject/{id}")]
        public async Task<ActionResult> Put([FromBody] SubjectDto Subject)
        {
            var command = new UpdateSubjectCommand { SubjectDto = Subject };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Subject/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSubjectCommand { SubjectId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
