using Hrm.Application;
using Hrm.Application.DTOs.Bank;
using Hrm.Application.Features.Bank.Requests.Commands;
using Hrm.Application.Features.Bank.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Bank)]
    [ApiController]
    public class Bank : Controller
    {
        private readonly IMediator _mediator;
        public Bank(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-bank")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBankDto Bank)
        {
            var command = new CreateBankCommand { BankDto = Bank };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-bank")]
        public async Task<ActionResult> Get()
        {
            var Bank = await _mediator.Send(new GetBankRequest { });
            return Ok(Bank);
        }

        [HttpGet]
        [Route("get-bankbyid/{id}")]
        public async Task<ActionResult<BankDto>> Get(int id)
        {
            var Bank = await _mediator.Send(new GetBankByIdRequest { BankId = id });
            return Ok(Bank);

        }

        [HttpGet]
        [Route("get-selectedbank")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedBank()
        {
            var bank = await _mediator.Send(new GetSelectedBankRequest { });
            return Ok(bank);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-bank/{id}")]
        public async Task<ActionResult> Put([FromBody] BankDto Bank)
        {
            var command = new UpdateBankCommand { BankDto = Bank };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-bank/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBankCommand { BankId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
