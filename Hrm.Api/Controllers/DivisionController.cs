using Hrm.Application;
using Hrm.Application.DTOs.Division;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Division;
using Hrm.Application.Features.Division.Requests.Commands;
using Hrm.Application.Features.Division.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Division.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Hrm.Domain;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Division)]
    [ApiController]
    public class DivisionController : Controller
    {
        private readonly IMediator _mediator;
        public DivisionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-division")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDivisionDto Division)
        {
            var command = new CreateDivisionCommand { DivisionDto = Division };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-division")]
        public async Task<ActionResult> Get()
        {
            var Division = await _mediator.Send(new GetDivisionRequest { });
            return Ok(Division);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-division/{id}")]
        public async Task<ActionResult> Put([FromBody] DivisionDto Division)
        {
            var command = new UpdateDivisionCommand { DivisionDto = Division };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-division/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDivisionCommand { DivisionId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpGet]
        [Route("get-divisionbyid/{id}")]
        public async Task<ActionResult<DivisionDto>> Get(int id)
        {
            var Division = await _mediator.Send(new GetDivisionByIdRequest { DivisionId = id });
            return Ok(Division);

        }
        [HttpGet]
        [Route("get-divisionByCountryId")]
        public async Task<ActionResult<List<DivisionDto>>> GetByCountryId(int countryId)
        {
            //var Division = await _mediator.Send(new GetDivisionByCountryIdRequest { CountryId = id });
            //return Ok(Division);
            var DivisionsByCountryId = await _mediator.Send(new GetDivisionByCountryIdRequest
            {
                CountryId = countryId
            });
            return Ok(DivisionsByCountryId);

        }
        [HttpGet]
        [Route("get-selecteddivision")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDivision()
        {
            var Division = await _mediator.Send(new GetSelectedDivisionRequest { });
            return Ok(Division);
        }
    }
}
