using Hrm.Application;
using Hrm.Application.DTOs.EmpWorkHistory;
using Hrm.Application.Features.EmpWorkHistories.Requests.Commands;
using Hrm.Application.Features.EmpWorkHistories.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpWorkHistory)]
    [ApiController]
    //[Authorize]
    public class EmpWorkHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpWorkHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpWorkHistory")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateEmpWorkHistoryDto> EmpWorkHistory)
        {
            var command = new CreateEmpWorkHistoryCommand { EmpWorkHistoryDto = EmpWorkHistory };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpWorkHistoryByEmpId/{id}")]
        public async Task<ActionResult<EmpWorkHistoryDto>> Get(int id)
        {
            var EmpWorkHistories = await _mediator.Send(new GetEmpWorkHistoryByEmpIdRequest { Id = id });
            return Ok(EmpWorkHistories);
        }

        [HttpDelete]
        [Route("delete-EmpWorkHistory/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpWorkHistoryCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
