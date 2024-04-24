using Hrm.Application;
using Hrm.Application.DTOs.BankBranch;
using Hrm.Application.DTOs.BankBranch;
using Hrm.Application.Features.BankBranch.Requests.Commands;
using Hrm.Application.Features.BankBranch.Requests.Queries;
using Hrm.Application.Features.BankBranch.Requests.Commands;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.BankBranch)]
    [ApiController]
    public class BankBranch : Controller
    {
        private readonly IMediator _mediator;
        public BankBranch(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-bankBranch")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBankBranchDto BankBranch)
        {
            var command = new CreateBankBranchCommand { BankBranchDto = BankBranch };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-bankBranch")]
        public async Task<ActionResult> Get()
        {
            var BankBranch = await _mediator.Send(new GetBankBranchRequest { });
            return Ok(BankBranch);
        }

        [HttpGet]
        [Route("get-bankBranchbyid/{id}")]
        public async Task<ActionResult<BankBranchDto>> Get(int id)
        {
            var BankBranch = await _mediator.Send(new GetBankBranchByIdRequest { BankBranchId = id });
            return Ok(BankBranch);

        }

        [HttpGet]
        [Route("get-selectedbankBranch")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedBankBranch()
        {
            var bankBranch = await _mediator.Send(new GetSelectedBankBranchRequest { });
            return Ok(bankBranch);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-bankBranch/{id}")]
        public async Task<ActionResult> Put([FromBody] BankBranchDto BankBranch)
        {
            var command = new UpdateBankBranchCommand { BankBranchDto = BankBranch };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-bankBranch/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBankBranchCommand { BankBranchId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
