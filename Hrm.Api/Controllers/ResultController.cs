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
using Hrm.Application.DTOs.Result;
using Hrm.Application.Features.Result.Requests.Commands;
using Hrm.Application.Features.Result.Requests.Queries;
using Hrm.Application.Features.Results.Requests.Queries;
using Hrm.Shared.Models;
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

        [HttpGet]
        [Route("get-result")]
        public async Task<ActionResult> Get()
        {
            var Result = await _mediator.Send(new GetResultRequest { });
            return Ok(Result);
        }
        [HttpGet]
        [Route("get-resultDetail/{id}")]
        public async Task<ActionResult<ResultDto>> Get(int id)
        {
            var Results = await _mediator.Send(new GetResultDetailRequest { ResultId = id });
            return Ok(Results);
        }
        [HttpGet]
        [Route("get-selectedResults")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedResult()
        {
            var Result = await _mediator.Send(new GetSelectedResultRequest { });
            return Ok(Result);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-result/{id}")]
        public async Task<ActionResult> Put([FromBody] ResultDto Result)
        {
            var command = new UpdateResultCommand { ResultDto = Result };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-result/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteResultCommand { ResultId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
