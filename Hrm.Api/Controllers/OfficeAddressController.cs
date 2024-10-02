using Hrm.Application;
using Hrm.Application.DTOs.OfficeAddress;
using Hrm.Application.DTOs.OfficeAddress;
using Hrm.Application.Features.OfficeAddress.Requests.Commands;
using Hrm.Application.Features.OfficeAddress.Requests.Queries;
using Hrm.Application.Features.OfficeAddress.Requests.Commands;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.OfficeAddress)]
    [ApiController]
    [Authorize]
    public class OfficeAddress : Controller
    {
        private readonly IMediator _mediator;
        public OfficeAddress(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-officeAddress")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOfficeAddressDto OfficeAddress)
        {
            var command = new CreateOfficeAddressCommand { OfficeAddressDto = OfficeAddress };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-officeAddress")]
        public async Task<ActionResult> Get()
        {
            var OfficeAddress = await _mediator.Send(new GetOfficeAddressRequest { });
            return Ok(OfficeAddress);
        }

        [HttpGet]
        [Route("get-officeAddressbyid/{id}")]
        public async Task<ActionResult<OfficeAddressDto>> Get(int id)
        {
            var OfficeAddress = await _mediator.Send(new GetOfficeAddressByIdRequest { OfficeAddressId = id });
            return Ok(OfficeAddress);

        }

        [HttpGet]
        [Route("get-selectedOfficeAddress")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedOfficeAddress()
        {
            var OfficeAddress = await _mediator.Send(new GetSelectedOfficeAddressRequest { });
            return Ok(OfficeAddress);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-officeAddress/{id}")]
        public async Task<ActionResult> Put([FromBody] OfficeAddressDto OfficeAddress)
        {
            var command = new UpdateOfficeAddressCommand { OfficeAddressDto = OfficeAddress };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-officeAddress/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOfficeAddressCommand { OfficeAddressId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
