using Hrm.Application;
using Hrm.Application.DTOs.EmpEducationInfo;
using Hrm.Application.Features.EmpEducationInfos.Requests.Commands;
using Hrm.Application.Features.EmpEducationInfos.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpEducationInfo)]
    [ApiController]
    public class EmpEducationInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpEducationInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpEducationInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpEducationInfoDto> EmpEducationInfo)
        {
            var command = new CreateEmpEducationInfoCommand { EmpEducationInfoDto = EmpEducationInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpEducationInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpEducationInfoDto>> Get(int id)
        {
            var EmpEducationInfos = await _mediator.Send(new GetEmpEducationInfoByEmpIdRequest { Id = id });
            return Ok(EmpEducationInfos);
        }

        [HttpDelete]
        [Route("delete-EmpEducationInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpEducationInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
