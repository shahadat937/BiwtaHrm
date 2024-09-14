using Hrm.Application;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.Union;
using Hrm.Application.DTOs.Ward;
using Hrm.Application.Features.Ward.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Result.Requests.Commands;
using Hrm.Application.Features.Union.Requests.Commands;
using Hrm.Application.Features.Ward.Request.Commands;
using Hrm.Application.Features.Ward.Request.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.Union.Requests.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Ward)]
    [ApiController]
    [Authorize]
    public class WardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WardController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        [Route("get-ward")]
        public async Task<ActionResult> Get()
        {
            var Ward = await _mediator.Send(new GetWardRequest { });
            return Ok(Ward);
        }
        [HttpGet]
        [Route("get-wardByUnionId/{unionId}")]
        public async Task<ActionResult<List<SelectedModel>>> GetWardByUnionId(int unionId)
        {

            var wards = await _mediator.Send(new GetWardByUnionIdRequest
            {
                UnionId = unionId
            });
            return Ok(wards);

        }
        [HttpPost]
        [Route("save-ward")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateWardDto ward)
        {
            var command = new CreateWardCommand { WardDto = ward };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-ward/{id}")]
        public async Task<ActionResult> Put([FromBody] WardDto ward)
        {
            var command = new UpdateWardCommand { WardDto = ward };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [Route("delete-ward/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteWardCommand { WardId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-wardbyid/{id}")]
        public async Task<ActionResult<WardDto>> Get(int id)
        {
            var Ward = await _mediator.Send(new GetWardByIdRequest { WardId = id });
            return Ok(Ward);

        }

        [HttpGet]
        [Route("get-selectedward")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedWard()
        {
            var ward = await _mediator.Send(new GetSelectedWardRequest { });
            return Ok(ward);
        }

    }
}
