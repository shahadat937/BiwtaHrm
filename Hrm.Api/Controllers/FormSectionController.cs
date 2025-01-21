using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Hrm.Application;
using Hrm.Application.DTOs.FormSection;
using Hrm.Application.Features.FormSection.Requests.Commands;
using Hrm.Application.Features.FormSection.Requests.Queries;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.FormSection)]
    [ApiController]
    public class FormSectionController : Controller
    {
        private readonly IMediator _mediator;

        public FormSectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-FormSection")]
        public async Task<ActionResult> GetFormSection()
        {
            var command = new GetFormSectionRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("save-FormSection")]
        public async Task<ActionResult<BaseCommandResponse>> SaveFormSection([FromBody] CreateFormSectionDto FormSection)
        {
            var command = new CreateFormSectionCommand { FormSection = FormSection };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-FormSection")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateFormSection([FromBody] GetFormSectionDto FormSection)
        {
            var command = new UpdateFormSectionCommand { FormSection = FormSection };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-FormSection/{FormSectionId}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteFormSection(int FormSectionId)
        {
            var command = new DeleteFormSectionByIdCommand { FormSectionId = FormSectionId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
