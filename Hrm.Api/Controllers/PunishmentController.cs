using Hrm.Application;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Punishment.Requests.Commands;
using Hrm.Application.Features.Punishment.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Punishment)]
    [ApiController]
    public class PunishmentController : Controller
    {
        private readonly IMediator _mediator;
        public PunishmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-punishment")]
        public async Task<ActionResult> GetPunishment()
        {
            var Punishment = await _mediator.Send(new GetPunishmentRequest { });
            return Ok(Punishment);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-punishment")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePunishmentDto Punishment)
        {
            var command = new CreatePunishmentCommand { PunishmentDto = Punishment };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-punishment/{id}")]
        public async Task<ActionResult> Put([FromBody] PunishmentDto Punishment)
        {
            var command = new UpdatePunishmentCommand { PunishmentDto = Punishment };
           var response= await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-punishment/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeletePunishmentCommand { PunishmentId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
