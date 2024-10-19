using Hrm.Application;
using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Features.EmpTransferPostings.Handlers.Queries;
using Hrm.Application.Features.EmpTransferPostings.Requests.Commands;
using Hrm.Application.Features.EmpTransferPostings.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpTransferPosting)]
    [ApiController]
    [Authorize]
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
            var command = new CreateEmpTransferPostingCommand { EmpTransferPostingDto = EmpTransferPosting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-EmpTransferPosting/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Update([FromBody] CreateEmpTransferPostingDto EmpTransferPosting)
        {
            var command = new UpdateEmpTransferPostingInfoCommand { UpdateEmpTransferPostingDto = EmpTransferPosting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-EmpTransferPostingStatus/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateStatus([FromBody] CreateEmpTransferPostingDto EmpTransferPosting)
        {
            var command = new UpdateEmpTransferPostingStatusCommand { UpdateEmpTransferPostingDto = EmpTransferPosting };
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
            var EmpTransferPosting = await _mediator.Send(new GetEmpTransferPostingByIdRequest { Id = id });
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-EmpTransferPostingByEmpId/{id}")]
        public async Task<ActionResult> GetByEmpId(int id)
        {
            var EmpTransferPosting = await _mediator.Send(new GetEmpTransferPostingByEmpIdRequest { Id = id });
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-AllEmpTransferPostingByEmpId/{id}")]
        public async Task<ActionResult> GetAllByEmpId(int id)
        {
            var EmpTransferPosting = await _mediator.Send(new GetAllEmpTransferPostingByEmpIdRequest { Id = id });
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-AllEmpTransferPostingApproveInfo")]
        public async Task<ActionResult> GetAllEmpTransferPostingApproveInfo()
        {
            var EmpTransferPosting = await _mediator.Send(new GetEmpTransferPostingApprovalListRequest { });
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-EmpTransferPostingDeptApprove/{id}")]
        public async Task<ActionResult> EmpTransferPostingDeptApprove(int id)
        {
            var command = new GetEmpTransferPostingDeptApprovalRequest { Id = id };
            var EmpTransferPosting = await _mediator.Send(command);
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-EmpTransferPostingJoiningInfo/{id}")]
        public async Task<ActionResult> EmpTransferPostingJoiningInfo(int id)
        {
            var command = new GetEmpTransferPostingJoiningInfoRequest { Id = id };
            var EmpTransferPosting = await _mediator.Send(command);
            return Ok(EmpTransferPosting);
        }

        [HttpGet]
        [Route("get-currentDeptJoinDateByEmpId/{id}")]
        public async Task<ActionResult> CurrentDeptJoinDateByEmpId(int id)
        {
            var lastDate = await _mediator.Send(new GetCurrentDeptJoinDateByEmpIdRequest { EmpId = id });
            return Ok(lastDate);
        }
    }
}
