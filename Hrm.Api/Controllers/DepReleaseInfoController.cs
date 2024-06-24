using Hrm.Application;
using Hrm.Application.DTOs.DepReleaseInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.DepReleaseInfo.Requests.Commands;
using Hrm.Application.Features.DepReleaseInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Persistence;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.DepReleaseInfo)]
    [ApiController]
    public class DepReleaseInfoController : Controller
    {
        private readonly HrmDbContext _dbContext;
        private readonly IMediator _mediator;
        public DepReleaseInfoController(IMediator mediator,HrmDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;

        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-depReleaseInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDepReleaseInfoDto DepReleaseInfo)
        {
            if (DepReleaseInfo.ApproveBy == 0)
            {
                return NoContent();
            }
            else
            {
                var PostingOrderInfo = await _dbContext.PostingOrderInfo.ToListAsync();
                //int maxId = PostingOrderInfo.Max(x => x.PostingOrderInfoId);
                int maxId = PostingOrderInfo.Max(x => x.PostingOrderInfoId);
                DepReleaseInfo.PostingOrderInfoId = maxId;

                var command = new CreateDepReleaseInfoCommand { DepReleaseInfoDto = DepReleaseInfo };
                var response = await _mediator.Send(command);
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("get-depReleaseInfo")]
        public async Task<ActionResult> Get()
        {
            var DepReleaseInfo = await _mediator.Send(new GetDepReleaseInfoRequest { });
            return Ok(DepReleaseInfo);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-depReleaseInfo/{id}")]
        public async Task<ActionResult> Put([FromBody] DepReleaseInfoDto DepReleaseInfo)
        {
            var command = new UpdateDepReleaseInfoCommand { DepReleaseInfoDto = DepReleaseInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-depReleaseInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDepReleaseInfoCommand { DepReleaseInfoId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpGet]
        [Route("get-depReleaseInfobyid/{id}")]
        public async Task<ActionResult<DepReleaseInfoDto>> Get(int id)
        {
            var DepReleaseInfo = await _mediator.Send(new GetDepReleaseInfoByIdRequest { DepReleaseInfoId = id });
            return Ok(DepReleaseInfo);

        }
        [HttpGet]
        [Route("get-depReleaseInfoByEmployeeId/{id}")]
        public async Task<ActionResult<List<DepReleaseInfoDto>>> GetByEmployeeId(int id)
        {
            //var DepReleaseInfo = await _mediator.Send(new GetDepReleaseInfoByCountryIdRequest { CountryId = id });
            //return Ok(DepReleaseInfo);
            var DepReleaseInfosByEmployeeId = await _mediator.Send(new GetDepReleaseInfoByEmployeeIdRequest
            {
                EmpId = id
            });
            return Ok(DepReleaseInfosByEmployeeId);

        }
        [HttpGet]
        [Route("get-selectedDepReleaseInfo")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDepReleaseInfo()
        {
            var DepReleaseInfo = await _mediator.Send(new GetSelectedDepReleaseInfoRequest { });
            return Ok(DepReleaseInfo);
        }
        //[HttpPost]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[Route("save-AnoterdepReleaseInfo")]
        //public async Task<ActionResult<BaseCommandResponse>> DepPost([FromBody] CreateDepReleaseInfoDto DepReleaseInfo)
        //{


        //        var command = new CreateDepReleaseInfoCommand { DepReleaseInfoDto = DepReleaseInfo };
        //        var response = await _mediator.Send(command);
        //        return Ok(response);
        //}
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-AnoterdepReleaseInfo")]
        public async Task<ActionResult<BaseCommandResponse>> DepPost([FromBody] CreateDepReleaseInfoDto depReleaseInfoDto)
        {
            var command = new CreateDepReleaseInfoCommand { DepReleaseInfoDto = depReleaseInfoDto };
            var response = await _mediator.Send(command);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

