using Hrm.Application;
using Hrm.Application.DTOs.EmpJobDetail;
using Hrm.Application.DTOs.EmpRewardPunishment;
using Hrm.Application.Features.EmpRewardPunishments.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpRewardPunishment)]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<EmpRewardPunishmentDto>> GetEmpRewardPunishmentByEmpId(int id)
        {
            var EmpRewardPunishment = await _mediator.Send(new GetEmpRewardPunishmentByEmpIdRequest { EmpId = id });
            return Ok(EmpRewardPunishment);
        }


        //[HttpPost]
        //[Route("save-EmpJobDetails")]
        //public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpJobDetailDto EmpJobDetails)
        //{
        //    var command = new CreateEmpJobDetailCommand { EmpJobDetailDto = EmpJobDetails };
        //    var response = await _mediator.Send(command);
        //    return Ok(response);
        //}


        //[HttpPut]
        //[Route("update-EmpJobDetails/{id}")]
        //public async Task<ActionResult> Put([FromBody] EmpJobDetailDto EmpJobDetails)
        //{
        //    var command = new UpdateEmpJobDetailCommand { EmpJobDetailDto = EmpJobDetails };
        //    var response = await _mediator.Send(command);
        //    return Ok(response);
        //}
    }
}
