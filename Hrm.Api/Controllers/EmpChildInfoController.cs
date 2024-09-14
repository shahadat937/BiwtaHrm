using Hrm.Application;
using Hrm.Application.DTOs.EmpChildInfo;
using Hrm.Application.Features.EmpChildInfos.Requests.Commands;
using Hrm.Application.Features.EmpChildInfos.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpChildInfo)]
    [ApiController]
    [Authorize]
    public class EmpChildInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpChildInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpChildInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpChildInfoDto> EmpChildInfo)
        {
            var command = new CreateEmpChildInfoCommand { EmpChildInfoDto = EmpChildInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpChildInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpChildInfoDto>> Get(int id)
        {
            var EmpChildInfos = await _mediator.Send(new GetEmpChildInfoByEmpIdRequest { Id = id });
            return Ok(EmpChildInfos);
        }

        [HttpDelete]
        [Route("delete-EmpChildInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpChildInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
