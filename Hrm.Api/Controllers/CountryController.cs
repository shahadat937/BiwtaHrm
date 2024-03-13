using Hrm.Application;
using Hrm.Application.DTOs.Country;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.Features.Country.Requests.Commands;
using Hrm.Application.Features.Country.Requests.Queries;
using Hrm.Application.Features.Ward.Request.Commands;
using Hrm.Application.Features.Ward.Request.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Country)]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpGet]
        [Route("get-country")]
        public async Task<ActionResult> Get()
        {
            var Ward = await _mediator.Send(new GetCountryRequest { });
            return Ok(Ward);
        }

        [HttpPost]
        [Route("save-country")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCountryDto country)
        {
            var command = new CreateCountryCommand { CountryDto = country };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
