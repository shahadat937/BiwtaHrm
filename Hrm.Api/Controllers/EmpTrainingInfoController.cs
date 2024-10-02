using Hrm.Application;
using Hrm.Application.DTOs.EmpTrainingInfo;
using Hrm.Application.Features.EmpTrainingInfos.Requests.Commands;
using Hrm.Application.Features.EmpTrainingInfos.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpTrainingInfo)]
    [ApiController]
    [Authorize]
    public class EmpTrainingInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpTrainingInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpTrainingInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpTrainingInfoDto> EmpTrainingInfo)
        {
            var command = new CreateEmpTrainingInfoCommand { EmpTrainingInfoDto = EmpTrainingInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpTrainingInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpTrainingInfoDto>> Get(int id)
        {
            var EmpTrainingInfos = await _mediator.Send(new GetEmpTrainingInfoByEmpIdRequest { Id = id });
            return Ok(EmpTrainingInfos);
        }

        [HttpDelete]
        [Route("delete-EmpTrainingInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpTrainingInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}

