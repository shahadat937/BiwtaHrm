using Hrm.Application;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Features.EmpTransferPostings.Requests.Commands;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpTransferPosting)]
    [ApiController]
    public class EmpTransferPostingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpTransferPostingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpTransferPosting")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpTransferPostingDto EmpTransferPosting)
        {
            var command = new CreateEmpTransferPostingRequest { EmpTransferPostingDto = EmpTransferPosting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-allEmpTransferPosting")]
        public async Task<ActionResult> Get()
        {
            var command = new GetAllEmpTransferPostingRequest { };
            var EmpTransferPosting = await _mediator.Send(command);
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-EmpTransferPostingById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var EmpTransferPosting = await _mediator.Send(new GetEmpTransferPostingByEmpIdRequest { Id = id });
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-EmpTransferPostingDeptApprove")]
        public async Task<ActionResult> EmpTransferPostingDeptApprove()
        {
            var command = new GetEmpTransferPostingDeptApprovalRequest { };
            var EmpTransferPosting = await _mediator.Send(command);
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-EmpTransferPostingJoiningInfo")]
        public async Task<ActionResult> EmpTransferPostingJoiningInfo()
        {
            var command = new GetEmpTransferPostingJoiningInfoRequest { };
            var EmpTransferPosting = await _mediator.Send(command);
            return Ok(EmpTransferPosting);
        }
    }
}
