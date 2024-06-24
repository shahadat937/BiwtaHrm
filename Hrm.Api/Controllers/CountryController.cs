using Hrm.Application;
using Hrm.Application.DTOs.Country;
using Hrm.Application.DTOs.Country;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.Features.Country.Requests.Commands;
using Hrm.Application.Features.Country.Requests.Queries;
using Hrm.Application.Features.Countrys.Requests.Queries;
using Hrm.Application.Features.Country.Requests.Commands;
using Hrm.Application.Features.Country.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Features.Ward.Request.Commands;
using Hrm.Application.Features.Ward.Request.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Country)]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-country")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCountryDto Country)
        {
            var command = new CreateCountryCommand { CountryDto = Country };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-country")]
        public async Task<ActionResult> Get()
        {
            var Country = await _mediator.Send(new GetCountryRequest { });
            return Ok(Country);
        }
        [HttpGet]
        [Route("get-countryDetail/{id}")]
        public async Task<ActionResult<CountryDto>> Get(int id)
        {
            var Countrys = await _mediator.Send(new GetCountryDetailRequest { CountryId = id });
            return Ok(Countrys);
        }
        [HttpGet]
        [Route("get-selectedCountrys")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedCountry()
        {
            var Country = await _mediator.Send(new GetSelectedCountryRequest { });
            return Ok(Country);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-country/{id}")]
        public async Task<ActionResult> Put([FromBody] CountryDto Country)
        {
            var command = new UpdateCountryCommand { CountryDto = Country };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-country/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCountryCommand { CountryId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
