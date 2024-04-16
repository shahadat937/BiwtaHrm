using Hrm.Application;
using Hrm.Application.DTOs.Subject;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Subject.Requests.Commands;
using Hrm.Application.Features.Subject.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.DTOs.Subject;
using Hrm.Application.Features.Subject.Requests.Queries;
using Hrm.Shared.Models;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Subject)]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly IMediator _mediator;
        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Subject")]
        public async Task<ActionResult> GetSubject()
        {
            var Subject = await _mediator.Send(new GetSubjectRequest { });
            return Ok(Subject);
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


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-Subject/{id}")]
        public async Task<ActionResult> Put([FromBody] SubjectDto Subject)
        {
            var command = new UpdateSubjectCommand { SubjectDto = Subject };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Subject/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSubjectCommand { SubjectId = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [Route("get-subjectbyid/{id}")]
        public async Task<ActionResult<SubjectDto>> Get(int id)
        {
            var Subject = await _mediator.Send(new GetSubjectByIdRequest { SubjectId = id });
            return Ok(Subject);

        }

        [HttpGet]
        [Route("get-selectedsubject")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedSubject()
        {
            var subject = await _mediator.Send(new GetSelectedSubjectRequest { });
            return Ok(subject);
        }
    }
}
