using Hrm.Application;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.Features.Reward.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;
using Hrm.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Scale)]
    [ApiController]
    public class ScaleController : Controller
    {
        private readonly IMediator _mediator;
        private readonly HrmDbContext _contex;
        public ScaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-scale")]
        public async Task<ActionResult> GetScale()
        {
            var Reward = await _mediator.Send(new GetScaleRequest { });
            return Ok(Reward);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-scale")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateScaleDto Scale)
        {
            var command = new CreateScaleCommand { ScaleDto = Scale };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-scale/{id}")]
        public async Task<ActionResult> Put([FromBody] ScaleDto Scale)
        {
            var command = new UpdateScaleCommand { ScaleDto = Scale };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-scale/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteScaleCommand { ScaleId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
