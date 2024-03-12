using Hrm.Application;
using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Features.PromotionType.Request.Commands;
using Hrm.Application.Features.TrainingType.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Promotion_Type)]
    [ApiController]
    public class PromotionTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PromotionTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("save-promotionType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePromotionTypeDto promotionType)
        {
            var command = new CreatePromotionTypeCommand { PromotionTypeDto = promotionType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
