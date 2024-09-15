using Hrm.Application;
using Hrm.Application.DTOs.EmpJobDetail;
using Hrm.Application.DTOs.EmpRewardPunishment;
using Hrm.Application.Features.EmpRewardPunishments.Requests.Commands;
using Hrm.Application.Features.EmpRewardPunishments.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpRewardPunishment)]
    [ApiController]
    public class EmpRewardPunishmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpRewardPunishmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-allEmpRewardPunishment")]
        public async Task<ActionResult> GetAllEmpRewardPunishment()
        {
            var EmpRewardPunishment = await _mediator.Send(new GetAllEmpRewardPunishmentRequest {  });
            return Ok(EmpRewardPunishment);
        }


        [HttpGet]
        [Route("get-empRewardPunishmentById/{id}")]
        public async Task<ActionResult<EmpRewardPunishmentDto>> GetEmpRewardPunishmentById(int id)
        {
            var EmpRewardPunishment = await _mediator.Send(new GetEmpRewardPunishmentDetailsRequest { Id = id });
            return Ok(EmpRewardPunishment);
        }

        [HttpGet]
        [Route("get-empRewardPunishmentByEmpId/{id}")]
        public async Task<ActionResult> GetEmpRewardPunishmentByEmpId(int id)
        {
            var EmpRewardPunishment = await _mediator.Send(new GetEmpRewardPunishmentByEmpIdRequest { EmpId = id });
            return Ok(EmpRewardPunishment);
        }


        [HttpPost]
        [Route("save-EmpRewardPunishment")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpRewardPunishmentDto EmpRewardPunishment)
        {
            var command = new CreateEmpRewardPunishmentCommand { EmpRewardPunishmentDto = EmpRewardPunishment };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpRewardPunishment/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateEmpRewardPunishmentDto EmpRewardPunishment)
        {
            var command = new UpdateEmpRewardPunishmentCommand { EmpRewardPunishmentDto = EmpRewardPunishment };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("update-EmpRewardPunishment/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpRewardPunishmentCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
