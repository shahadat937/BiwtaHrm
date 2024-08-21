using Hrm.Application;
using Hrm.Application.DTOs.FormField;
using Hrm.Application.Features.FormField.Requests.Commands;
using Hrm.Application.Features.FormField.Requests.Queries;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.FormField)]
    [ApiController]
    public class FormFieldController: Controller
    {
        private readonly IMediator _mediator;
        
        public FormFieldController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Route("get-FormField")]
        public async Task<ActionResult> GetFormField()
        {
            var command = new GetFormFieldRequest();
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Route("get-FormFieldById/{id}")]
        public async Task<ActionResult> GetFormFieldById(int id)
        {
            var command = new GetFormFieldByIdRequest { FieldId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Route("get-SelectedFormField")]
        public async Task<ActionResult> GetSelectedFormField()
        {
            var command = new GetSelectedFormFieldRequest();
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-FormField")]
        public async Task<ActionResult<BaseCommandResponse>> SaveFormField([FromBody] CreateFormFieldDto formFieldDto)
        {
            var command = new CreateFormFieldCommand { formFieldDto = formFieldDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-FormField")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateFormField([FromBody] FormFieldDto formFieldDto)
        {
            var command = new UpdateFormFieldCommand { formFieldDto = formFieldDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-FormField/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteFormField(int id)
        {
            var command = new DeleteFormFieldCommand { FieldId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
