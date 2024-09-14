using Hrm.Application;
using Hrm.Application.DTOs.EmpSpouseInfo;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Commands;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpSpouseInfo)]
    [ApiController]
    [Authorize]
    public class EmpSpouseInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpSpouseInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpSpouseInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpSpouseInfoDto> EmpSpouseInfo)
        {
            var command = new CreateEmpSpouseInfoCommand { EmpSpouseInfoDto = EmpSpouseInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        
        [HttpGet]
        [Route("get-EmpSpouseInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpSpouseInfoDto>> Get(int id)
        {
            var EmpSpouseInfos = await _mediator.Send(new GetEmpSpouseInfoByEmpIdRequest { Id = id });
            return Ok(EmpSpouseInfos);
        }

        [HttpDelete]
        [Route("delete-EmpSpouseInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpSpouseInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
 