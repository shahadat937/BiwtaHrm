using Hrm.Application;
using Hrm.Application.DTOs.EmpPromotionIncrement;
using Hrm.Application.Features.EmpPromotionIncrements.Requests.Commands;
using Hrm.Application.Features.EmpPromotionIncrements.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpPromotionIncrement)]
    [ApiController]
    [Authorize]
    public class EmpPromotionIncrementController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpPromotionIncrementController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpPromotionIncrement")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpPromotionIncrementDto EmpPromotionIncrement)
        {
            var command = new CreateEmpPromotionIncrementCommand { EmpPromotionIncrementDto = EmpPromotionIncrement };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-EmpPromotionIncrement/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] CreateEmpPromotionIncrementDto EmpPromotionIncrement)
        {
            var command = new UpdateEmpPromotionIncrementCommand { EmpPromotionIncrementDto = EmpPromotionIncrement };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-allEmpPromotionIncrement")]
        public async Task<ActionResult> Get()
        {
            var command = new GetAllEmpPromotionIncrementRequest { };
            var EmpPromotionIncrement = await _mediator.Send(command);
            return Ok(EmpPromotionIncrement);
        }

        [HttpGet]
        [Route("get-EmpPromotionIncrementById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var EmpPromotionIncrement = await _mediator.Send(new GetEmpPromotionIncrementByIdRequest { Id = id });
            return Ok(EmpPromotionIncrement);
        }

        [HttpGet]
        [Route("get-EmpPromotionIncrementByEmpId/{id}")]
        public async Task<ActionResult> GetByEmpId(int id)
        {
            var EmpPromotionIncrement = await _mediator.Send(new GetEmpPromotionIncrementByEmpIdRequest { Id = id });
            return Ok(EmpPromotionIncrement);
        }

        [HttpGet]
        [Route("get-AllEmpPromotionIncrementApproveInfo")]
        public async Task<ActionResult> GetAllEmpPromotionIncrementApproveInfo()
        {
            var EmpPromotionIncrement = await _mediator.Send(new GetEmpPromotionIncrementApprovalListRequest { });
            return Ok(EmpPromotionIncrement);
        }
    }
}
