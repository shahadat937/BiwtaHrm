using Hrm.Application;
using Hrm.Application.DTOs.FieldRecord;
using Hrm.Application.Features.FieldRecord.Requests.Commands;
using Hrm.Application.Features.FieldRecord.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.FieldRecord)]
    [ApiController]
    [Authorize]
    public class FieldRecordController: Controller
    {
        private readonly IMediator _mediator;
        
        public FieldRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-FieldRecordById/{id}")]
        public async Task<ActionResult> GetFieldRecordById(int id)
        {
            var command = new GetFieldRecordByIdRequest { FieldRecordId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-FieldRecordByFormRecordId/{id}")]
        public async Task<ActionResult> GetFieldRecordByFormRecordId(int id)
        {
            var command = new GetFieldRecordByFormRecordIdRequest { FormRecordId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-FieldRecord")]
        public async Task<ActionResult<BaseCommandResponse>> SaveFieldRecord([FromBody] CreateFieldRecordDto fieldRecordDto)
        {
            var command = new CreateFieldRecordCommand { FieldRecordDto = fieldRecordDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-FieldRecord")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateFieldRecord([FromBody] FieldRecordDto fieldRecordDto)
        {
            var command = new UpdateFieldRecordCommand { FieldRecordDto = fieldRecordDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-FieldRecordById/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteFieldRecordById(int id)
        {
            var command = new DeleteFieldRecordByIdCommand { FieldRecordId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
