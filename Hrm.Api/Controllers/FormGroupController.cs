using System.Runtime.InteropServices;
using Hrm.Application;
using Hrm.Application.DTOs.FormGroup;
using Hrm.Application.Features.FormGroup.Requests.Commands;
using Hrm.Application.Features.FormGroup.Requests.Queries;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.FormGroup)]
    [ApiController]
    public class FormGroupController : Controller
    {
        private readonly IMediator _mediator;

        public FormGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-FormGroup")]
        public async Task<ActionResult> GetFormGroup()
        {
            var command = new GetFormGroupRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("save-FormGroup")]
        public async Task<ActionResult<BaseCommandResponse>> SaveFormGroup([FromBody] CreateFormGroupDto FormGroup)
        {
            var command = new CreateFormGroupCommand { FormGroup = FormGroup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-FormGroup")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateFormGroup([FromBody] FormGroupDto FormGroup)
        {
            var command = new UpdateFormGroupByIdCommand { FormGroup = FormGroup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-FormGrou/{FromGroupId}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteFormGroup(int FromGroupId)
        {
            var command = new DeleteFormGroupByIdCommand { FormGroupId = FromGroupId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
