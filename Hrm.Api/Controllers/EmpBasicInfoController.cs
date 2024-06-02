using Hrm.Application;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Commands;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpBasicInfo)]
    [ApiController]
    public class EmpBasicInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpBasicInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-allEmpBasicInfo")]
        public async Task<ActionResult> GetEmpBasicInfos()
        {
            var EmpBasicInfos = await _mediator.Send(new GetAllEmpBasicInfoRequest { });
            return Ok(EmpBasicInfos);
        }

        [HttpGet]
        [Route("get-EmpBasicInfosById/{id}")]
        public async Task<ActionResult<EmpBasicInfoDto>> GetEmpBasicInfosById(int id)
        {
            var EmpBasicInfos = await _mediator.Send(new GetEmpBasicInfoByIdRequest { Id = id });
            return Ok(EmpBasicInfos);
        }


        [HttpPost]
        [Route("save-EmpBasicInfos")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpBasicInfoDto EmpBasicInfos)
        {
            var command = new CreateEmpBasicInfoCommand { EmpBasicInfoDto = EmpBasicInfos };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpBasicInfos/{id}")]
        public async Task<ActionResult> Put([FromBody] EmpBasicInfoDto EmpBasicInfos)
        {
            var command = new UpdateEmpBasicInfoCommand { EmpBasicInfoDto = EmpBasicInfos };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}