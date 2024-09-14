using Hrm.Application;
using Hrm.Application.DTOs.Year;
using Hrm.Application.Features.Year.Requests.Commands;
using Hrm.Application.Features.Year.Requests.Commands;
using Hrm.Application.Features.Year.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Year)]
    [ApiController]
    [Authorize]
    public class YearController : ControllerBase
    {
        private readonly IMediator _mediator;
        public YearController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-year")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateYearDto Year)
        {
            var command = new CreateYearCommand { YearDto = Year };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-year")]
        public async Task<ActionResult> Get()
        {
            var Year = await _mediator.Send(new GetYearRequest { });
            return Ok(Year);
        }
        [HttpGet]
        [Route("get-yearDetail/{id}")]
        public async Task<ActionResult<YearDto>> Get(int id)
        {
            var Years = await _mediator.Send(new GetYearDetailRequest { YearId = id });
            return Ok(Years);
        }
        [HttpGet]
        [Route("get-selectedyears")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedYear()
        {
            var Year = await _mediator.Send(new GetSelectedYearRequest { });
            return Ok(Year);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-year/{id}")]
        public async Task<ActionResult> Put([FromBody] YearDto Year)
        {
            var command = new UpdateYearCommand { YearDto = Year };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-year/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteYearCommand { YearId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}

