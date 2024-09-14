using Hrm.Application;
using Hrm.Application.DTOs.Reward;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Reward.Requests.Commands;
using Hrm.Application.Features.Reward.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Reward)]
    [ApiController]
    [Authorize]
    public class RewardController : Controller
    {
        private readonly IMediator _mediator;
        public RewardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-reward")]
        public async Task<ActionResult> GetReward()
        {
            var Reward = await _mediator.Send(new GetRewardRequest { });
            return Ok(Reward);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-reward")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRewardDto Reward)
        {
            var command = new CreateRewardCommand { RewardDto = Reward };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-reward/{id}")]
        public async Task<ActionResult> Put([FromBody] RewardDto Reward)
        {
            var command = new UpdateRewardCommand { RewardDto = Reward };
           var response= await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-reward/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteRewardCommand { RewardId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
