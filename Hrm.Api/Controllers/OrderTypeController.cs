using Hrm.Application.DTOs.OrderType;
using Hrm.Application.Features.OrderTypes.Requests.Commands;
using Hrm.Application.Features.OrderTypes.Requests.Queries;
using Hrm.Application;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.OrderType)]
    [ApiController]
    [Authorize]
    public class OrderTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-OrderTypes")]
        public async Task<ActionResult<List<OrderTypeDto>>> Get()
        {
            var OrderTypes = await _mediator.Send(new GetOrderTypeListRequest { });
            return Ok(OrderTypes);
        }


        [HttpGet]
        [Route("get-selectedOrderTypes")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedOrderType()
        {
            var OrderTypes = await _mediator.Send(new GetSelectedOrderTypeRequest { });
            return Ok(OrderTypes);
        }


        [HttpGet]
        [Route("get-OrderTypeDetail/{id}")]
        public async Task<ActionResult<OrderTypeDto>> Get(int id)
        {
            var OrderType = await _mediator.Send(new GetOrderTypeDetailRequest { Id = id });
            return Ok(OrderType);
        }

        [HttpPost]
        [Route("save-OrderType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOrderTypeDto OrderType)
        {
            var command = new CreateOrderTypeCommand { OrderTypeDto = OrderType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-OrderType/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateOrderTypeDto OrderType)
        {
            var command = new UpdateOrderTypeCommand { OrderTypeDto = OrderType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesDefaultResponseType]
        [Route("delete-OrderType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOrderTypeCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }

}