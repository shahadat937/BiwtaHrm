using Hrm.Application;
using Hrm.Application.DTOs.CancelledWeekend;
using Hrm.Application.Features.CancelledWeekend.Requests.Commands;
using Hrm.Application.Features.CancelledWeekend.Requests.Queries;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.CancelledWeekend)]
    [ApiController]
    public class CancelledWeekendController: Controller
    {
        private readonly IMediator _mediator;

        public CancelledWeekendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-CancelledWeekendByFilter")]
        public async Task<ActionResult> GetCancelledWeekendByFilter([FromQuery] GetCancelledWeekendFilterDto filters)
        {
            var command = new GetCancelledWeekendByFilterRequest { filters = filters };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-CancelledWeekend")]
        public async Task<ActionResult<BaseCommandResponse>> CreateCancelledWeekend([FromBody] CreateCancelledWeekendDto cancelledWeekend)
        {
            var command = new CreateCancelledWeekendCommand { CancelledWeekend = cancelledWeekend };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-CancelledWeekend")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateCancelledWeekend([FromBody] CancelledWeekendDto cancelledWeekend)
        {
            var command = new UpdateCancelledWeekendCommand { cancelledWeekend = cancelledWeekend };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-CancelledWeekend/{Id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteCancelledWeekend(int Id)
        {
            var command = new DeleteCancelledWeekendByIdCommand { Id = Id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
