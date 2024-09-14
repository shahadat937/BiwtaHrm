using Hrm.Application;
using Hrm.Application.DTOs.MaritalStatus;

using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Hrm.Domain;
using Hrm.Application.DTOs.EmpTnsferPostingJoin;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Commands;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Queries;
using Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Queries;
using Microsoft.EntityFrameworkCore;
using Hrm.Persistence;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.EmpTnsferPostingJoin)]
    [ApiController]
    [Authorize]
    public class EmpTnsferPostingJoinController : Controller
    {
        private readonly IMediator _mediator;
        private readonly HrmDbContext _dbContext;
        public EmpTnsferPostingJoinController(IMediator mediator, HrmDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-EmpTnsferPostingJoin")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpTnsferPostingJoinDto EmpTnsferPostingJoin)
        {
            if (EmpTnsferPostingJoin.ApproveBy == "0")
            {
                return NoContent();
            }
            else
            {
                var PostingOrderInfo = await _dbContext.PostingOrderInfo.ToListAsync();
                int maxId = PostingOrderInfo.Max(x => x.PostingOrderInfoId);
               // int maxId = postingOrderInfos.Max(x => x.PostingOrderInfoId);
                EmpTnsferPostingJoin .PostingOrderInfoId = maxId;
                var command = new CreateEmpTnsferPostingJoinCommand { EmpTnsferPostingJoinDto = EmpTnsferPostingJoin };
                var response = await _mediator.Send(command);
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("get-EmpTnsferPostingJoin")]
        public async Task<ActionResult> Get()
        {

            var EmpTnsferPostingJoin = await _mediator.Send(new GetEmpTnsferPostingJoinRequest  { });
            return Ok(EmpTnsferPostingJoin);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-EmpTnsferPostingJoin/{id}")]
        public async Task<ActionResult> Put([FromBody] EmpTnsferPostingJoinDto EmpTnsferPostingJoin)
        {
            var command = new UpdateEmpTnsferPostingJoinCommand { EmpTnsferPostingJoinDto = EmpTnsferPostingJoin };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-EmpTnsferPostingJoin/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpTnsferPostingJoinCommand { EmpTnsferPostingJoinId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpGet]
        [Route("get-EmpTnsferPostingJoinbyid/{id}")]
        public async Task<ActionResult<EmpTnsferPostingJoinDto>> Get(int id)
        {
            var EmpTnsferPostingJoin = await _mediator.Send(new GetEmpTnsferPostingJoinByIdRequest { EmpTnsferPostingJoinId = id });
            return Ok(EmpTnsferPostingJoin);

        }
        [HttpGet]
        [Route("get-EmpTnsferPostingJoinByEmployeeId/{id}")]
        public async Task<ActionResult<List<EmpTnsferPostingJoinDto>>> GetByEmployeeId(int id)
        {
            //var EmpTnsferPostingJoin = await _mediator.Send(new GetEmpTnsferPostingJoinByCountryIdRequest { CountryId = id });
            //return Ok(EmpTnsferPostingJoin);
            var EmpTnsferPostingJoinsByEmployeeId = await _mediator.Send(new GetEmpTnsferPostingJoinByEmployeeIdRequest
            {
                EmpId = id
            });
            return Ok(EmpTnsferPostingJoinsByEmployeeId);

        }
        [HttpGet]
        [Route("get-selectedEmpTnsferPostingJoin")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedEmpTnsferPostingJoin()
        {
            var EmpTnsferPostingJoin = await _mediator.Send(new GetSelectedEmpTnsferPostingJoinRequest { });
            return Ok(EmpTnsferPostingJoin);
        }
        [HttpGet]
        [Route("get-AllEmpTnsferPostingJoin")]
        public async Task<ActionResult> Gets()
        {
            var EmpTnsferPostingJoin = await _mediator.Send(new GetEmpTnsferPostingJoinRequest { });
            return Ok(EmpTnsferPostingJoin);
        }

    }
}
