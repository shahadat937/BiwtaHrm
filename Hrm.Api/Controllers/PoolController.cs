using Hrm.Application;
using Hrm.Application.DTOs.Pool;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Pool.Requests.Commands;
using Hrm.Application.Features.Pool.Requests.Queries;
using Hrm.Application.Features.Pools.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Pool)]
    [ApiController]
    [Authorize]
    public class PoolController : Controller
    {
        private readonly IMediator _mediator;
        public PoolController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-pool")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePoolDto Pool)
        {
            var command = new CreatePoolCommand { PoolDto = Pool };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-pool")]
        public async Task<ActionResult> Get()
        {
            var Pool = await _mediator.Send(new GetPoolRequest { });
            return Ok(Pool);
        }
        [HttpGet]
        [Route("get-poolDetail/{id}")]
        public async Task<ActionResult<PoolDto>> Get(int id)
        {
            var Pools = await _mediator.Send(new GetPoolDetailRequest { PoolId = id });
            return Ok(Pools);
        }
        [HttpGet]
        [Route("get-selectedPools")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedPool()
        {
            var Pool = await _mediator.Send(new GetSelectedPoolRequest { });
            return Ok(Pool);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-pool/{id}")]
        public async Task<ActionResult> Put([FromBody] PoolDto Pool)
        {
            var command = new UpdatePoolCommand { PoolDto = Pool };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-pool/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeletePoolCommand { PoolId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
