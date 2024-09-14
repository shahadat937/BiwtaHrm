using Hrm.Application;
using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Features.PromotionType.Request.Commands;
using Hrm.Application.Features.PromotionType.Request.Queries;
using Hrm.Application.Features.PromotionTypes.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Features.TrainingType.Requests.Commands;
using Hrm.Application.Features.TrainingType.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.PromotionType)]
    [ApiController]
    [Authorize]
    public class PromotionTypeController : Controller
    {
        private readonly IMediator _mediator;
        public PromotionTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-promotionType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePromotionTypeDto PromotionType)
        {
            var command = new CreatePromotionTypeCommand { PromotionTypeDto = PromotionType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-promotionType")]
        public async Task<ActionResult> Get()
        {
            var PromotionType = await _mediator.Send(new GetPromotionTypeRequest { });
            return Ok(PromotionType);
        }
        [HttpGet]
        [Route("get-promotionTypeDetail/{id}")]
        public async Task<ActionResult<PromotionTypeDto>> Get(int id)
        {
            var PromotionTypes = await _mediator.Send(new GetPromotionTypeDetailRequest { PromotionTypeId = id });
            return Ok(PromotionTypes);
        }
        [HttpGet]
        [Route("get-selectedPromotionTypes")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedPromotionType()
        {
            var PromotionType = await _mediator.Send(new GetSelectedPromotionTypeRequest { });
            return Ok(PromotionType);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-promotionType/{id}")]
        public async Task<ActionResult> Put([FromBody] PromotionTypeDto PromotionType)
        {
            var command = new UpdatePromotionTypeCommand { PromotionTypeDto = PromotionType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-promotionType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeletePromotionTypeCommand { PromotionTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}

