using Hrm.Application;
using Hrm.Application.DTOs.EmpPsiTrainingInfo;
using Hrm.Application.Features.EmpPsiTrainingInfos.Requests.Commands;
using Hrm.Application.Features.EmpPsiTrainingInfos.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpPsiTrainingInfo)]
    [ApiController]
    public class EmpPsiTrainingInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpPsiTrainingInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpPsiTrainingInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpPsiTrainingInfoDto> EmpPsiTrainingInfo)
        {
            var command = new CreateEmpPsiTrainingInfoCommand { EmpPsiTrainingInfoDto = EmpPsiTrainingInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpPsiTrainingInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpPsiTrainingInfoDto>> Get(int id)
        {
            var EmpPsiTrainingInfos = await _mediator.Send(new GetEmpPsiTrainingInfoByEmpIdRequest { Id = id });
            return Ok(EmpPsiTrainingInfos);
        }

        [HttpDelete]
        [Route("delete-EmpPsiTrainingInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpPsiTrainingInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
