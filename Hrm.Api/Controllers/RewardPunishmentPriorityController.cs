using Hrm.Application;
using Hrm.Application.DTOs.RewardPunishmentPriority;
using Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Commands;
using Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.RewardPunishmentPriority)]
    [ApiController]
    public class RewardPunishmentPriorityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RewardPunishmentPriorityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-RewardPunishmentPriority")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRewardPunishmentPriorityDto RewardPunishmentPriority)
        {
            var command = new CreateRewardPunishmentPriorityCommand { RewardPunishmentPriorityDto = RewardPunishmentPriority };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-RewardPunishmentPriority")]
        public async Task<ActionResult> Get()
        {
            var RewardPunishmentPriority = await _mediator.Send(new GetRewardPunishmentPriorityRequest { });
            return Ok(RewardPunishmentPriority);
        }
        [HttpGet]
        [Route("get-RewardPunishmentPriorityDetail/{id}")]
        public async Task<ActionResult<RewardPunishmentPriorityDto>> Get(int id)
        {
            var RewardPunishmentPrioritys = await _mediator.Send(new GetRewardPunishmentPriorityDetailRequest { RewardPunishmentPriorityId = id });
            return Ok(RewardPunishmentPrioritys);
        }
        [HttpGet]
        [Route("get-selectedRewardPunishmentPrioritys")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedRewardPunishmentPriority()
        {
            var RewardPunishmentPriority = await _mediator.Send(new GetSelectedRewardPunishmentPriorityRequest { });
            return Ok(RewardPunishmentPriority);
        }

        [HttpPut]
        [Route("update-RewardPunishmentPriority/{id}")]
        public async Task<ActionResult> Put([FromBody] RewardPunishmentPriorityDto RewardPunishmentPriority)
        {
            var command = new UpdateRewardPunishmentPriorityCommand { RewardPunishmentPriorityDto = RewardPunishmentPriority };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [Route("delete-RewardPunishmentPriority/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteRewardPunishmentPriorityCommand { RewardPunishmentPriorityId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}