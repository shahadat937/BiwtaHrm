using Hrm.Application.DTOs.OfficeOrder;
using Hrm.Application.Features.OfficeOrders.Requests.Commands;
using Hrm.Application.Features.OfficeOrders.Requests.Queries;
using Hrm.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.OfficeOrder)]
    [ApiController]
    [Authorize]
    public class OfficeOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-OfficeOrders")]
        public async Task<ActionResult<List<OfficeOrderDto>>> Get()
        {
            var OfficeOrders = await _mediator.Send(new GetOfficeOrderListRequest { });
            return Ok(OfficeOrders);
        }


        [HttpGet]
        [Route("get-OfficeOrderDetail/{id}")]
        public async Task<ActionResult<OfficeOrderDto>> Get(int id)
        {
            var OfficeOrder = await _mediator.Send(new GetOfficeOrderDetailsRequest { Id = id });
            return Ok(OfficeOrder);
        }

        [HttpPost]
        [Route("save-OfficeOrder")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateOfficeOrderDto OfficeOrder)
        {
            var command = new CreateOfficeOrderCommand { OfficeOrderDto = OfficeOrder };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-OfficeOrder/{id}")]
        public async Task<ActionResult> Put([FromForm] CreateOfficeOrderDto OfficeOrder)
        {
            var command = new UpdateOfficeOrderCommand { OfficeOrderDto = OfficeOrder };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesDefaultResponseType]
        [Route("delete-OfficeOrder/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOfficeOrderCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}

