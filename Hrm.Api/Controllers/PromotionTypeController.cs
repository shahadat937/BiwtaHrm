using Hrm.Application;
using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Features.PromotionType.Request.Commands;
using Hrm.Application.Features.PromotionType.Request.Queries;
using Hrm.Application.Features.TrainingType.Requests.Commands;
using Hrm.Application.Features.TrainingType.Requests.Queries;
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


        [HttpGet]
        [Route("get-promotionType")]
        public async Task<ActionResult> Get()
        {
            var PromotionType = await _mediator.Send(new GetPromotionTypeRequest { });
            return Ok(PromotionType);
        }

        [HttpPost]
        [Route("save-promotionType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePromotionTypeDto promotionType)
        {
            var command = new CreatePromotionTypeCommand { PromotionTypeDto = promotionType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpPut]
        [Route("update-promotionType/{id}")]
        public async Task<ActionResult> Put([FromBody] PromotionTypeDto promotionTypeDto)
        {
            var command = new UpdatePromotionTypeCommand { PromotionTypeDto = promotionTypeDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-promotionType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeletePromotionTypeCommand { PromotionTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
