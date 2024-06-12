using Hrm.Application;
using Hrm.Application.DTOs.EmpForeignTourInfo;
using Hrm.Application.Features.EmpForeignTourInfos.Requests.Commands;
using Hrm.Application.Features.EmpForeignTourInfos.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpForeigTourInfo)]
    [ApiController]
    public class EmpForeignTourInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpForeignTourInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpForeignTourInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpForeignTourInfoDto> EmpForeignTourInfo)
        {
            var command = new CreateEmpForeignTourInfoCommand { EmpForeignTourInfoDto = EmpForeignTourInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpForeignTourInfoByEmpId/{id}")]
        public async Task<ActionResult<EmpForeignTourInfoDto>> Get(int id)
        {
            var EmpForeignTourInfos = await _mediator.Send(new GetEmpForeignTourInfoByEmpIdRequest { Id = id });
            return Ok(EmpForeignTourInfos);
        }

        [HttpDelete]
        [Route("delete-EmpForeignTourInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpForeignTourInfoCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
