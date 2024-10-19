using Hrm.Application;
using Hrm.Application.DTOs.JobDetailsSetup;
using Hrm.Application.Features.JobDetailsSetups.Requests.Commands;
using Hrm.Application.Features.JobDetailsSetups.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.JobDetailsSetup)]
    [ApiController]
    [Authorize]
    public class JobDetailsSetupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobDetailsSetupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-JobDetailsSetups")]
        public async Task<ActionResult<List<JobDetailsSetupDto>>> Get()
        {
            var JobDetailsSetups = await _mediator.Send(new GetJobDetailsSetupListRequest { });
            return Ok(JobDetailsSetups);
        }

        [HttpGet]
        [Route("get-ActiveJobDetailsSetups")]
        public async Task<ActionResult<JobDetailsSetupDto>> GetActive()
        {
            var JobDetailsSetups = await _mediator.Send(new GetActiveJobDetailsSetupRequest { });
            return Ok(JobDetailsSetups);
        }

        [HttpGet]
        [Route("get-JobDetailsSetupDetail/{id}")]
        public async Task<ActionResult<JobDetailsSetupDto>> Get(int id)
        {
            var JobDetailsSetup = await _mediator.Send(new GetJobDetailsSetupDetailRequest { Id = id });
            return Ok(JobDetailsSetup);
        }

        [HttpPost]
        [Route("save-JobDetailsSetup")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateJobDetailsSetupDto JobDetailsSetup)
        {
            var command = new CreateJobDetailsSetupCommand { JobDetailsSetupDto = JobDetailsSetup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesDefaultResponseType]
        [Route("update-JobDetailsSetup/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateJobDetailsSetupDto JobDetailsSetup)
        {
            var command = new UpdateJobDetailsSetupCommand { JobDetailsSetupDto = JobDetailsSetup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("delete-JobDetailsSetup/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteJobDetailsSetupCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }

}

