using Hrm.Application;
using Hrm.Application.DTOs.EmpPersonalInfo;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpPersonalInfo)]
    [ApiController]
    public class EmpPersonalInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpPersonalInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-allEmpPersonalInfo")]
        public async Task<ActionResult> GetEmpPersonalInfos()
        {
            var EmpPersonalInfos = await _mediator.Send(new GetAllEmpPersonalInfoRequest { });
            return Ok(EmpPersonalInfos);
        }

        [HttpGet]
        [Route("get-EmpPersonalInfosById/{id}")]
        public async Task<ActionResult<EmpPersonalInfoDto>> GetEmpPersonalInfosById(int id)
        {
            var EmpPersonalInfos = await _mediator.Send(new GetEmpPersonalInfoByIdRequest { Id = id });
            return Ok(EmpPersonalInfos);
        }


        [HttpPost]
        [Route("save-EmpPersonalInfos")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpPersonalInfoDto EmpPersonalInfos)
        {
            var command = new CreateEmpPersonalInfoCommand { EmpPersonalInfoDto = EmpPersonalInfos };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpPersonalInfos/{id}")]
        public async Task<ActionResult> Put([FromBody] EmpPersonalInfoDto EmpPersonalInfos)
        {
            var command = new UpdateEmpPersonalInfoCommand { EmpPersonalInfoDto = EmpPersonalInfos };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}