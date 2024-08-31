using Hrm.Application;
using Hrm.Application.DTOs.RewardPunishmentType;
using Hrm.Application.Features.RewardPunishmentTypes.Requests.Commands;
using Hrm.Application.Features.RewardPunishmentTypes.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.RewardPunishmentType)]
    [ApiController]
    public class RewardPunishmentTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RewardPunishmentTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-RewardPunishmentType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRewardPunishmentTypeDto RewardPunishmentType)
        {
            var command = new CreateRewardPunishmentTypeCommand { RewardPunishmentTypeDto = RewardPunishmentType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-RewardPunishmentType")]
        public async Task<ActionResult> Get()
        {
            var RewardPunishmentType = await _mediator.Send(new GetRewardPunishmentTypeRequest { });
            return Ok(RewardPunishmentType);
        }
        [HttpGet]
        [Route("get-RewardPunishmentTypeDetail/{id}")]
        public async Task<ActionResult<RewardPunishmentTypeDto>> Get(int id)
        {
            var RewardPunishmentTypes = await _mediator.Send(new GetRewardPunishmentTypeDetailRequest { RewardPunishmentTypeId = id });
            return Ok(RewardPunishmentTypes);
        }
        [HttpGet]
        [Route("get-selectedRewardPunishmentTypes")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedRewardPunishmentType()
        {
            var RewardPunishmentType = await _mediator.Send(new GetSelectedRewardPunishmentTypeRequest { });
            return Ok(RewardPunishmentType);
        }

        [HttpPut]
        [Route("update-RewardPunishmentType/{id}")]
        public async Task<ActionResult> Put([FromBody] RewardPunishmentTypeDto RewardPunishmentType)
        {
            var command = new UpdateRewardPunishmentTypeCommand { RewardPunishmentTypeDto = RewardPunishmentType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [Route("delete-RewardPunishmentType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteRewardPunishmentTypeCommand { RewardPunishmentTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}