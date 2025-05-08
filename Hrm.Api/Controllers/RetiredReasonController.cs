using Hrm.Application.DTOs.RetiredReason;
using Hrm.Application.Features.RetiredReasons.Requests.Commands;
using Hrm.Application.Features.RetiredReasons.Requests.Queries;
using Hrm.Application;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.RetiredReason)]
    [ApiController]
    [Authorize]
    public class RetiredReasonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RetiredReasonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-RetiredReasons")]
        public async Task<ActionResult<List<RetiredReasonDto>>> Get()
        {
            var RetiredReasons = await _mediator.Send(new GetRetiredReasonListRequest { });
            return Ok(RetiredReasons);
        }


        [HttpGet]
        [Route("get-selectedRetiredReasons")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedRetiredReason()
        {
            var RetiredReasons = await _mediator.Send(new GetSelectedRetiredReasonRequest { });
            return Ok(RetiredReasons);
        }


        [HttpGet]
        [Route("get-RetiredReasonDetail/{id}")]
        public async Task<ActionResult<RetiredReasonDto>> Get(int id)
        {
            var RetiredReason = await _mediator.Send(new GetRetiredReasonDetailRequest { Id = id });
            return Ok(RetiredReason);
        }

        [HttpPost]
        [Route("save-RetiredReason")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRetiredReasonDto RetiredReason)
        {
            var command = new CreateRetiredReasonCommand { RetiredReasonDto = RetiredReason };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-RetiredReason/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateRetiredReasonDto RetiredReason)
        {
            var command = new UpdateRetiredReasonCommand { RetiredReasonDto = RetiredReason };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesDefaultResponseType]
        [Route("delete-RetiredReason/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteRetiredReasonCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }

}