using Hrm.Application;
using Hrm.Application.DTOs.DesignationSetup;
using Hrm.Application.Features.DesignationSetups.Requests.Commands;
using Hrm.Application.Features.DesignationSetups.Requests.Queries;
using Hrm.Application.Features.DesignationSetups.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.DesignationSetup)]
    [ApiController]
    [Authorize]
    public class DesignationSetupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DesignationSetupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-DesignationSetup")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDesignationSetupDto DesignationSetup)
        {
            var command = new CreateBloodCommand { DesignationSetupDto = DesignationSetup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-DesignationSetup")]
        public async Task<ActionResult> Get()
        {
            var DesignationSetup = await _mediator.Send(new GetDesignationSetupRequest { });
            return Ok(DesignationSetup);
        }
        [HttpGet]
        [Route("get-DesignationSetupDetail/{id}")]
        public async Task<ActionResult<DesignationSetupDto>> Get(int id)
        {
            var DesignationSetups = await _mediator.Send(new GetDesignationSetupDetailRequest { DesignationSetupId = id });
            return Ok(DesignationSetups);
        }
        [HttpGet]
        [Route("get-selectedDesignationSetups")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDesignationSetup()
        {
            var DesignationSetup = await _mediator.Send(new GetSelectedDesignationSetupRequest { });
            return Ok(DesignationSetup);
        }

        [HttpPut]
        [Route("update-DesignationSetup/{id}")]
        public async Task<ActionResult> Put([FromBody] DesignationSetupDto DesignationSetup)
        {
            var command = new UpdateDesignationSetupCommand { DesignationSetupDto = DesignationSetup };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [Route("delete-DesignationSetup/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDesignationSetupCommand { DesignationSetupId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
