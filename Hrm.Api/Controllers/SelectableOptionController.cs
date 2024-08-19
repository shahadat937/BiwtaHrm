using Hrm.Application;
using Hrm.Application.DTOs.SelectableOption;
using Hrm.Application.Features.SelectableOption.Requests.Commands;
using Hrm.Application.Features.SelectableOption.Requests.Queries;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.SelectableOption)]
    [ApiController]
    public class SelectableOptionController: Controller
    {
        private readonly IMediator _mediator;

        public SelectableOptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Option")]
        public async Task<ActionResult> GetOption()
        {
            var command = new GetOptionRequest();
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-OptionById/{id}")]
        public async Task<ActionResult> GetOptionById(int id)
        {
            var command = new GetOptionByIdRequest { OptionId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-SelectedOption")]
        public async Task<ActionResult> GetSelectedOption()
        {
            var command = new GetSelectedOptionRequest();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Option")]
        public async Task<ActionResult> SaveOption([FromBody] CreateSelectableOptionDto OptionDto)
        {
            var command = new CreateOptionCommand { OptionDto = OptionDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-Option")]
        public async Task<ActionResult> UpdateOption([FromBody] SelectableOptionDto optionDto)
        {
            var command = new UpdateOptionCommand { OptionDto = optionDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-Option/{id}")]

        public async Task<ActionResult> DeleteOption(int id)
        {
            var command = new DeleteOptionCommand { OptionId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
