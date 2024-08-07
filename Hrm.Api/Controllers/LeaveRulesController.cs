using Hrm.Application;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.DTOs.LeaveRules;
using Hrm.Application.Features.Leave.Requests.Commands;
using Hrm.Application.Features.LeaveRules.Requests.Commands;
using Hrm.Application.Features.LeaveRules.Requests.Queries;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.LeaveRules)]
    [ApiController]
    public class LeaveRulesController: Controller
    {
        private readonly IMediator _mediator;
        public LeaveRulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-LeaveRules")]
        public async Task<ActionResult> GetLeaveRules()
        {
            var command = new GetLeaveRulesRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-LeaveRules/{id}")]
        public async Task<ActionResult> GetLeaveRulesById(int id)
        {
            var command = new GetLeaveRulesByIdRequest { LeaveRulesId = id};
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-LeaveRulesByLeaveTypeId/{id}")]
        public async Task<ActionResult> GetLeaveRulesByLeaveTypeId(int id)
        {
            var command = new GetLeaveRulesByLeaveTypeIdRequest { LeaveTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-SelectedLeaveRulesName")]
        public async Task<ActionResult> GetSelectedLeaveRulesName()
        {
            var command = new GetSelectedLeaveRulesRequest { };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-LeaveRule")]
        public async Task<ActionResult<BaseCommandResponse>> saveLeaveRule([FromBody] CreateLeaveRulesDto leaveRuleDto)
        {
            var command = new CreateLeaveRuleCommand { createleaveRuleDto = leaveRuleDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-LeaveRule")]
        public async Task<ActionResult<BaseCommandResponse>> updateLeaveRule([FromBody] CreateLeaveRulesDto leaveRuleDto)
        {
            var command = new UpdateLeaveRuleCommand { updateLeaveRuleDto = leaveRuleDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-LeaveRule/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> deleteLeaveRule(int id)
        {
            var command = new DeleteLeaveRuleCommand { LeaveRuleId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
