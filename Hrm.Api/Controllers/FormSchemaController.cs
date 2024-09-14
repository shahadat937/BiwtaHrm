using Hrm.Application;
using Hrm.Application.DTOs.FormSchema;
using Hrm.Application.Features.FormSchema.Requests.Commands;
using Hrm.Application.Features.FormSchema.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.FormSchema)]
    [ApiController]
    [Authorize]
    public class FormSchemaController: Controller
    {
        private readonly IMediator _mediator;

        public FormSchemaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-FormSchema")]
        public async Task<ActionResult> GetFormSchema()
        {
            var commnad = new GetFormSchemaRequest();
            var response = await _mediator.Send(commnad);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-FormSchemaByFilter")]
        public async Task<ActionResult> GetFormSchemaByFilter([FromQuery] FormSchemaFilterDto filters)
        {
            var command = new GetFormSchemaByFilterRequest { filters = filters };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-FormSchemaById/{id}")]
        public async Task<ActionResult> GetFormSchemaById(int id)
        {
            var command = new GetFormSchemaByIdRequest { SchemaId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-FormSchema")]
        public async Task<ActionResult<BaseCommandResponse>> SaveFormSchema([FromBody] CreateFormSchemaDto formSchemaDto)
        {
            var command = new CreateFormSchemaCommand { FormSchemaDto = formSchemaDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-FormSchema")]
        public async Task<ActionResult<BaseCommandResponse>> UpateFormSchema([FromBody] FormSchemaDto formSchemaDto)
        {
            var command = new UpdateFormSchemaCommand { FormSchemaDto = formSchemaDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-FormSchema/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteFormSchema(int id)
        {
            var command = new DeleteFormSchemaCommand { SchemaId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }
}
