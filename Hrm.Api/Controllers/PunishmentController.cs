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
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.Features.Punishment.Requests.Commands;
using Hrm.Application.Features.Punishment.Requests.Queries;
using Hrm.Application.Features.Punishments.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Punishment)]
    [ApiController]
    [Authorize]
    public class PunishmentController : Controller
    {
        private readonly IMediator _mediator;
        public PunishmentController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpGet]
        [Route("get-punishment")]
        public async Task<ActionResult> Get()
        {
            var Punishment = await _mediator.Send(new GetPunishmentRequest { });
            return Ok(Punishment);
        }
        [HttpGet]
        [Route("get-punishmentDetail/{id}")]
        public async Task<ActionResult<PunishmentDto>> Get(int id)
        {
            var Punishments = await _mediator.Send(new GetPunishmentDetailRequest { PunishmentId = id });
            return Ok(Punishments);
        }
        [HttpGet]
        [Route("get-selectedPunishments")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedPunishment()
        {
            var Punishment = await _mediator.Send(new GetSelectedPunishmentRequest { });
            return Ok(Punishment);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-punishment/{id}")]
        public async Task<ActionResult> Put([FromBody] PunishmentDto Punishment)
        {
            var command = new UpdatePunishmentCommand { PunishmentDto = Punishment };
            var response = await _mediator.Send(command);
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

