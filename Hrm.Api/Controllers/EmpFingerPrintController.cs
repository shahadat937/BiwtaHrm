using Hrm.Application.DTOs.EmpFingerPrint;
using Hrm.Application.Features.EmpFingerPrints.Requests.Commands;
using Hrm.Application.Features.EmpFingerPrints.Requests.Queries;
using Hrm.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpFingerPrint)]
    [ApiController]
    [Authorize]
    public class EmpFingerPrintController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpFingerPrintController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-EmpFingerPrintByEmpId/{id}")]
        public async Task<ActionResult<EmpFingerPrintDto>> GetEmpFingerPrintsById(int id)
        {
            var EmpFingerPrints = await _mediator.Send(new GetEmpFingerPrintByIdRequest { Id = id });
            return Ok(EmpFingerPrints);
        }


        [HttpPost]
        [Route("save-EmpFingerPrints")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateEmpFingerPrintDto EmpFingerPrints)
        {
            var command = new CreateEmpFingerPrintCommand { EmpFingerPrintDto = EmpFingerPrints };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpFingerPrints/{id}")]
        public async Task<ActionResult> Put([FromForm] CreateEmpFingerPrintDto EmpFingerPrints)
        {
            var command = new UpdateEmpFingerPrintCommand { EmpFingerPrintDto = EmpFingerPrints };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
