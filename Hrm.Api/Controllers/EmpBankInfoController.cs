using Hrm.Application;
using Hrm.Application.DTOs.EmpBankInfo;
using Hrm.Application.Features.EmpBankInfos.Requests.Commands;
using Hrm.Application.Features.EmpBankInfos.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpBankInfo)]
    [ApiController]
    public class EmpBankInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpBankInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpBankInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpBankInfoDto> EmpBankInfo)
        {
            var command = new CreateEmpBankInfoCommand { EmpBankInfoDto = EmpBankInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpBankInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpBankInfoDto>> Get(int id)
        {
            var EmpBankInfos = await _mediator.Send(new GetEmpBankInfoByEmpIdRequest { Id = id });
            return Ok(EmpBankInfos);
        }

        [HttpDelete]
        [Route("delete-EmpBankInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpBankInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}

