using Hrm.Application;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Overall_EV_Promotion)]
    [ApiController]
    public class Overall_EV_PromotionController : Controller
    {
        private readonly IMediator _mediator;
        public Overall_EV_PromotionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Overall_EV_Promotion")]
        public async Task<ActionResult> GetOverall_EV_Promotion()
        {
            var Overall_EV_Promotion = await _mediator.Send(new GetOverall_EV_PromotionRequest { });
            return Ok(Overall_EV_Promotion);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Overall_EV_Promotion")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOverall_EV_PromotionDto Overall_EV_Promotion)
        {
            var command = new CreateOverall_EV_PromotionCommand { Overall_EV_PromotionDto = Overall_EV_Promotion };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-Overall_EV_Promotion/{id}")]
        public async Task<ActionResult> Put([FromBody] Overall_EV_PromotionDto Overall_EV_Promotion)
        {
            var command = new UpdateOverall_EV_PromotionCommand { Overall_EV_PromotionDto = Overall_EV_Promotion };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Overall_EV_Promotion/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOverall_EV_PromotionCommand { Overall_EV_PromotionId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
