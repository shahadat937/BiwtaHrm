using Hrm.Application;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Reward.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Domain;
using Hrm.Persistence;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.Features.Scale.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Scale)]
    [ApiController]
    [Authorize]
    public class ScaleController : Controller
    {
        private readonly IMediator _mediator;
        public ScaleController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpGet]
        [Route("get-scale")]
        public async Task<ActionResult> Get()
        {
            var Scale = await _mediator.Send(new GetScaleRequest { });
            return Ok(Scale);
        }
        [HttpGet]
        [Route("get-ScaleDetail/{id}")]
        public async Task<ActionResult<ScaleDto>> Get(int id)
        {
            var Scales = await _mediator.Send(new GetScaleDetailRequest { ScaleId = id });
            return Ok(Scales);
        }

        [HttpGet]
        [Route("get-selectedScale/{id}")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedScale(int id)
        {
            var Scales = await _mediator.Send(new GetSelectScaleRequest { GradeId = id });
            return Ok(Scales);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
