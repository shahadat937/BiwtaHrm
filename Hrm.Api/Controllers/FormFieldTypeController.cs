using Hrm.Application;
using Hrm.Application.DTOs.FormFieldType;
using Hrm.Application.Features.FormFieldType.Requests.Commands;
using Hrm.Application.Features.FormFieldType.Requests.Queries;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.FormFieldType)]
    [ApiController]
    public class FormFieldTypeController: Controller
    {
        private readonly IMediator _mediator;

        public FormFieldTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-FormFieldType")]
        public async Task<ActionResult> GetFormFieldType()
        {
            var command = new GetFormFieldTypeRequest();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-FormFieldTypeById/{id}")]
        public async Task<ActionResult> GetFormFieldTypeById(int id)
        {
            var command = new GetFormFieldTypeByIdRequest { FieldTypeId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-SelectedFormFieldType")]
        public async Task<ActionResult> GetSelectedFormFieldType()
        {
            var command = new GetSelectedFormFieldTypeRequest();
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-FormFieldType")]
        public async Task<ActionResult<BaseCommandResponse>> Handle([FromBody] CreateFormFieldTypeDto formFieldTypeDto)
        {
            var command = new CreateFormFieldTypeCommand { formFieldTypeDto = formFieldTypeDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-FormFieldType")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateFormFieldType([FromBody] FormFieldTypeDto formFieldTypeDto)
        {
            var command = new UpdateFormFieldTypeCommand { FormFieldTypeDto = formFieldTypeDto };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-FormFieldType/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteFormFieldType(int id)
        {
            var command = new DeleteFormFieldTypeCommand { FieldTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
