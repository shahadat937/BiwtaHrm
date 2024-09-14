using Hrm.Application;
using Hrm.Application.DTOs.BankAccountType;
using Hrm.Application.Features.BankAccountType.Requests.Commands;
using Hrm.Application.Features.BankAccountType.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.BankAccountType)]
    
    [ApiController]
    [Authorize]
    public class BankAccountType : Controller
    {
        private readonly IMediator _mediator;
        public BankAccountType(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-bankAccountType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBankAccountTypeDto BankAccountType)
        {
            var command = new CreateBankAccountTypeCommand { BankAccountTypeDto = BankAccountType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-bankAccountType")]
        public async Task<ActionResult> Get()
        {
            var BankAccountType = await _mediator.Send(new GetBankAccountTypeRequest { });
            return Ok(BankAccountType);
        }

        [HttpGet]
        [Route("get-bankAccountTypebyid/{id}")]
        public async Task<ActionResult<BankAccountTypeDto>> Get(int id)
        {
            var BankAccountType = await _mediator.Send(new GetBankAccountTypeByIdRequest { BankAccountTypeId = id });
            return Ok(BankAccountType);

        }

        [HttpGet]
        [Route("get-selectedbankAccountType")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedBankAccountType()
        {
            var bankAccountType = await _mediator.Send(new GetSelectedBankAccountTypeRequest { });
            return Ok(bankAccountType);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-bankAccountType/{id}")]
        public async Task<ActionResult> Put([FromBody] BankAccountTypeDto BankAccountType)
        {
            var command = new UpdateBankAccountTypeCommand { BankAccountTypeDto = BankAccountType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-bankAccountType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBankAccountTypeCommand { BankAccountTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
