using Hrm.Application;
using Hrm.Application.DTOs.JobDetailsSetup;
using Hrm.Application.Features.JobDetailsSetups.Requests.Commands;
using Hrm.Application.Features.JobDetailsSetups.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Office)]
    [ApiController]
    [Authorize]
    public class JobDetailsSetupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public JobDetailsSetupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-JobDetailsSetup")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateJobDetailsSetupDto JobDetailsSetup)
        {
            var command = new CreateJobDetailsSetupCommand { JobDetailsSetupDto = JobDetailsSetup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-oneJobDetailsSetup")]
        public async Task<ActionResult> GetOneJobDetailsSetup()
        {
            var JobDetailsSetup = await _mediator.Send(new GetOneJobDetailsSetupRequest { });
            return Ok(JobDetailsSetup);
        }


        [HttpPut]
        [Route("update-JobDetailsSetup/{id}")]
        public async Task<ActionResult> Put([FromBody] JobDetailsSetupDto JobDetailsSetup)
        {
            var command = new UpdateJobDetailsSetupCommand { JobDetailsSetupDto = JobDetailsSetup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
