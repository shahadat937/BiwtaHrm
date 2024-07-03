using Hrm.Application;
using Hrm.Application.DTOs.EmpNomineeInfo;
using Hrm.Application.Features.EmpNomineeInfos.Requests.Commands;
using Hrm.Application.Features.EmpNomineeInfos.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpNomineeInfo)]
    [ApiController]
    public class EmpNomineeInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpNomineeInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpNomineeInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] List<CreateEmpNomineeInfoDto> EmpNomineeInfo)
        {
            var command = new CreateEmpNomineeInfoCommand { EmpNomineeInfoDto = EmpNomineeInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpNomineeInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpNomineeInfoDto>> Get(int id)
        {
            var EmpNomineeInfos = await _mediator.Send(new GetEmpNomineeInfoByEmpIdRequest { Id = id });
            return Ok(EmpNomineeInfos);
        }

        [HttpDelete]
        [Route("delete-EmpNomineeInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpNomineeInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
