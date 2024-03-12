using Hrm.Application;
using Hrm.Application.DTOs.Result;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Result.Requests.Commands;
using Hrm.Application.Features.Result.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Result)]
    [ApiController]
    public class ResultController : Controller
    {
        private readonly IMediator _mediator;
        public ResultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-result")]
        public async Task<ActionResult> GetResult()
        {
            var Result = await _mediator.Send(new GetResultRequest { });
            return Ok(Result);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-result")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateResultDto Result)
        {
            var command = new CreateResultCommand { ResultDto = Result };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-result/{id}")]
        public async Task<ActionResult> Put([FromBody] ResultDto Result)
        {
            var command = new UpdateResultCommand { ResultDto = Result };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-result/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteResultCommand { ResultId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
