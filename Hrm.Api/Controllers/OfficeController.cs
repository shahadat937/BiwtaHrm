using Hrm.Application;
using Hrm.Application.DTOs.Office;
using Hrm.Application.Features.Office.Requests.Commands;
using Hrm.Application.Features.Office.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Office)]
    [ApiController]
    [Authorize]
    public class Office : Controller
    {
        private readonly IMediator _mediator;
        public Office(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-office")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOfficeDto Office)
        {
            var command = new CreateOfficeCommand { OfficeDto = Office };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-office")]
        public async Task<ActionResult> Get()
        {
            var Office = await _mediator.Send(new GetOfficeRequest { });
            return Ok(Office);
        }


        [HttpGet]
        [Route("get-oneOffice")]
        public async Task<ActionResult> GetOneOffice()
        {
            var Office = await _mediator.Send(new GetOneOfficeRequest { });
            return Ok(Office);
        }

        [HttpGet]
        [Route("get-officebyid/{id}")]
        public async Task<ActionResult<OfficeDto>> Get(int id)
        {
            var Office = await _mediator.Send(new GetOfficeByIdRequest { OfficeId = id });
            return Ok(Office);

        }

        [HttpGet]
        [Route("get-selectedoffice")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedOffice()
        {
            var office = await _mediator.Send(new GetSelectedOfficeRequest { });
            return Ok(office);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-office/{id}")]
        public async Task<ActionResult> Put([FromBody] OfficeDto Office)
        {
            var command = new UpdateOfficeCommand { OfficeDto = Office };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-office/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOfficeCommand { OfficeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
