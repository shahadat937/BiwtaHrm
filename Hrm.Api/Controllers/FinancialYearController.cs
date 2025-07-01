using Hrm.Application.DTOs.FinancialYear;
using Hrm.Application.Features.FinancialYears.Requests.Commands;
using Hrm.Application.Features.FinancialYears.Requests.Queries;
using Hrm.Application;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.FinancialYear)]
    [ApiController]
    [Authorize]
    public class FinancialYearController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialYearController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-FinancialYears")]
        public async Task<ActionResult<List<FinancialYearDto>>> Get()
        {
            var FinancialYears = await _mediator.Send(new GetFinancialYearListRequest { });
            return Ok(FinancialYears);
        }


        [HttpGet]
        [Route("get-selectedFinancialYears")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedFinancialYear()
        {
            var FinancialYears = await _mediator.Send(new GetSelectedFinancialYearRequest { });
            return Ok(FinancialYears);
        }


        [HttpGet]
        [Route("get-FinancialYearDetail/{id}")]
        public async Task<ActionResult<FinancialYearDto>> Get(int id)
        {
            var FinancialYear = await _mediator.Send(new GetFinancialYearDetailRequest { Id = id });
            return Ok(FinancialYear);
        }

        [HttpPost]
        [Route("save-FinancialYear")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFinancialYearDto FinancialYear)
        {
            var command = new CreateFinancialYearCommand { FinancialYearDto = FinancialYear };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-FinancialYear/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateFinancialYearDto FinancialYear)
        {
            var command = new UpdateFinancialYearCommand { FinancialYearDto = FinancialYear };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesDefaultResponseType]
        [Route("delete-FinancialYear/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteFinancialYearCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }

}