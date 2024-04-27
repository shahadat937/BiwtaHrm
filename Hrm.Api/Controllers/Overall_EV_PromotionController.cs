using Hrm.Application;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.Overall_EV_Promotions.Requests.Queries;
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
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-overall_EV_Promotion")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOverall_EV_PromotionDto Overall_EV_Promotion)
        {
            var command = new CreateOverall_EV_Promotion { Overall_EV_PromotionDto = Overall_EV_Promotion };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-overall_EV_Promotion")]
        public async Task<ActionResult> Get()
        {
            var Overall_EV_Promotion = await _mediator.Send(new GetOverall_EV_PromotionRequest { });
            return Ok(Overall_EV_Promotion);
        }
        [HttpGet]
        [Route("get-overall_EV_PromotionDetail/{id}")]
        public async Task<ActionResult<Overall_EV_PromotionDto>> Get(int id)
        {
            var Overall_EV_Promotions = await _mediator.Send(new GetOverall_EV_PromotionDetailRequest { Overall_EV_PromotionId = id });
            return Ok(Overall_EV_Promotions);
        }
        [HttpGet]
        [Route("get-selectedOverall_EV_Promotions")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedOverall_EV_Promotion()
        {
            var Overall_EV_Promotion = await _mediator.Send(new GetSelectedOverall_EV_PromotionRequest { });
            return Ok(Overall_EV_Promotion);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-overall_EV_Promotion/{id}")]
        public async Task<ActionResult> Put([FromBody] Overall_EV_PromotionDto Overall_EV_Promotion)
        {
            var command = new UpdateOverall_EV_PromotionCommand { Overall_EV_PromotionDto = Overall_EV_Promotion };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-overall_EV_Promotion/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOverall_EV_PromotionCommand { Overall_EV_PromotionId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
