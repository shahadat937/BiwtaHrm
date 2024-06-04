using Hrm.Application;
using Hrm.Application.DTOs.TransferApproveInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.TransferApproveInfo.Requests.Commands;
using Hrm.Application.Features.TransferApproveInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Hrm.Domain;
using Hrm.Application.Features.PostingOrderInfo.Requests.Queries;
using Hrm.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.TransferApproveInfo)]
    [ApiController]
    public class TransferApproveInfoController : Controller
    {
        private readonly IMediator _mediator;
        private readonly HrmDbContext _dbContext;
        public TransferApproveInfoController(IMediator mediator, HrmDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-transferApproveInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTransferApproveInfoDto TransferApproveInfo)
        {

           

            if (TransferApproveInfo.ApproveBy  == "0")
            {
                return NoContent();
            }
            else
            {
                var PostingOrderInfo = await _dbContext.PostingOrderInfo.ToListAsync();
                int maxId = PostingOrderInfo.Max(x => x.PostingOrderInfoId);
                TransferApproveInfo.PostingOrderInfoId = maxId;

                var command = new CreateTransferApproveInfoCommand { TransferApproveInfoDto = TransferApproveInfo };
                var response = await _mediator.Send(command);
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("get-transferApproveInfo")]
        public async Task<ActionResult> Get()
        {
            var TransferApproveInfo = await _mediator.Send(new GetTransferApproveInfoRequest { });
            return Ok(TransferApproveInfo);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-transferApproveInfo/{id}")]
        public async Task<ActionResult> Put([FromBody] TransferApproveInfoDto TransferApproveInfo)
        {

            if (TransferApproveInfo.ApproveBy == "0")
            {
                return NoContent();
            }
            else
            {
                var PostingOrderInfo = await _dbContext.PostingOrderInfo.ToListAsync();
                int maxId = PostingOrderInfo.Max(x => x.PostingOrderInfoId);

                TransferApproveInfo.PostingOrderInfoId = maxId;

                var command = new UpdateTransferApproveInfoCommand { TransferApproveInfoDto = TransferApproveInfo };
                var response = await _mediator.Send(command);
                return Ok(response);
            }

        }
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-transferApproveInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteTransferApproveInfoCommand { TransferApproveInfoId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpGet]
        [Route("get-transferApproveInfobyid/{id}")]
        public async Task<ActionResult<TransferApproveInfoDto>> Get(int id)
        {
            var TransferApproveInfo = await _mediator.Send(new GetTransferApproveInfoByIdRequest { TransferApproveInfoId = id });
            return Ok(TransferApproveInfo);

        }
        [HttpGet]
        [Route("get-transferApproveInfoByEmployeeId/{id}")]
        public async Task<ActionResult<List<TransferApproveInfoDto>>> GetByEmployeeId(int id)
        {
            //var TransferApproveInfo = await _mediator.Send(new GetTransferApproveInfoByCountryIdRequest { CountryId = id });
            //return Ok(TransferApproveInfo);
            var TransferApproveInfosByEmployeeId = await _mediator.Send(new GetTransferApproveInfoByEmployeeIdRequest
            {
                EmpId = id
            });
            return Ok(TransferApproveInfosByEmployeeId);

        }
        [HttpGet]
        [Route("get-selectedTransferApproveInfo")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedTransferApproveInfo()
        {
            var TransferApproveInfo = await _mediator.Send(new GetSelectedTransferApproveInfoRequest { });
            return Ok(TransferApproveInfo);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-transferApprove")]
        public async Task<ActionResult<BaseCommandResponse>> ApprovePost([FromBody] CreateTransferApproveInfoDto TransferApproveInfo)
        {
            //TransferApproveInfo.ApproveBy = "1";
            var command = new CreateTransferApproveInfoCommand { TransferApproveInfoDto = TransferApproveInfo };
            var response = await _mediator.Send(command);
            return Ok(response);

        }
    }
}
