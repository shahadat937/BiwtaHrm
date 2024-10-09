using Hrm.Application;
using Hrm.Application.DTOs.Form;
using Hrm.Application.DTOs.FormRecord;
using Hrm.Application.Features.FormRecord.Requests.Commands;
using Hrm.Application.Features.FormRecord.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.FormRecord)]
    [ApiController]
    [Authorize]
    public class FormRecordController: Controller
    {
        private readonly IMediator _mediator;

        public FormRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-FormRecord")]
        public async Task<ActionResult> GetFormRecord([FromQuery] FormRecordFilterDto filters)
        {
            var command = new GetFormRecordRequest { Filters = filters};
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-FormRecordByFormId/{id}")]
        public async Task<ActionResult> GetFormRecordByFormId(int id)
        {
            var command = new GetFormRecordByFormIdRequest { FormId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-FormRecord")]
        public async Task<ActionResult<BaseCommandResponse>> CreateFormRecord([FromBody] CreateFormRecordDto formRecordDto)
        {
            var command = new CreateFormRecordCommand { FormRecordDto = formRecordDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-FormRecord")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateFormRecord([FromBody] FormRecordDto formRecordDto)
        {
            var command = new UpdateFormRecordCommand { FormRecordDto = formRecordDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-FormRecord/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteFormRecord(int id)
        {
            var command = new DeleteFormRecordCommand { RecordId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

    }
}
