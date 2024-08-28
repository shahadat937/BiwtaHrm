using Hrm.Application;
using Hrm.Application.DTOs.EmpShiftAssign;
using Hrm.Application.Features.EmpShiftAssigns.Requests.Commands;
using Hrm.Application.Features.EmpShiftAssigns.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpShiftAssign)]
    [ApiController]
    public class EmpShiftAssignController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpShiftAssignController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-allEmpShiftAssign")]
        public async Task<ActionResult> GetEmpShiftAssigns()
        {
            var EmpShiftAssigns = await _mediator.Send(new GetAllEmpShiftAssignRequest { });
            return Ok(EmpShiftAssigns);
        }

        [HttpGet]
        [Route("get-EmpShiftAssignByEmpId/{id}")]
        public async Task<ActionResult<EmpShiftAssignDto>> GetEmpShiftAssignsById(int id)
        {
            var EmpShiftAssigns = await _mediator.Send(new GetEmpShiftAssignByIdRequest { Id = id });
            return Ok(EmpShiftAssigns);
        }


        [HttpPost]
        [Route("save-EmpShiftAssigns")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpShiftAssignDto EmpShiftAssigns)
        {
            var command = new CreateEmpShiftAssignCommand { EmpShiftAssignDto = EmpShiftAssigns };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpShiftAssigns/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateEmpShiftAssignDto EmpShiftAssigns)
        {
            var command = new UpdateEmpShiftAssignCommand { EmpShiftAssignDto = EmpShiftAssigns };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
