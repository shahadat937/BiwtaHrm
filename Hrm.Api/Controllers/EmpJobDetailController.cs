using Hrm.Application;
using Hrm.Application.DTOs.EmpJobDetail;
using Hrm.Application.Features.EmpJobDetails.Requests.Commands;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpJobDetail)]
    [ApiController]
    [Authorize]
    public class EmpJobDetailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpJobDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-EmpJobDetailByEmpId/{id}")]
        public async Task<ActionResult<EmpJobDetailDto>> GetEmpJobDetailsById(int id)
        {
            var EmpJobDetails = await _mediator.Send(new GetEmpJobDetailByIdRequest { Id = id });
            return Ok(EmpJobDetails);
        }


        [HttpPost]
        [Route("save-EmpJobDetails")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpJobDetailDto EmpJobDetails)
        {
            var command = new CreateEmpJobDetailCommand { EmpJobDetailDto = EmpJobDetails };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpJobDetails/{id}")]
        public async Task<ActionResult> Put([FromBody] EmpJobDetailDto EmpJobDetails)
        {
            var command = new UpdateEmpJobDetailCommand { EmpJobDetailDto = EmpJobDetails };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
