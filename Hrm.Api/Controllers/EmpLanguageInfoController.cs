using Hrm.Application;
using Hrm.Application.DTOs.EmpLanguageInfo;
using Hrm.Application.Features.EmpLanguageInfos.Requests.Commands;
using Hrm.Application.Features.EmpLanguageInfos.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpLanguageInfo)]
    [ApiController]
    public class EmpLanguageInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpLanguageInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpLanguageInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpLanguageInfoDto> EmpLanguageInfo)
        {
            var command = new CreateEmpLanguageInfoCommand { EmpLanguageInfoDto = EmpLanguageInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpLanguageInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpLanguageInfoDto>> Get(int id)
        {
            var EmpLanguageInfos = await _mediator.Send(new GetEmpLanguageInfoByEmpIdRequest { Id = id });
            return Ok(EmpLanguageInfos);
        }

        [HttpDelete]
        [Route("delete-EmpLanguageInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpLanguageInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
