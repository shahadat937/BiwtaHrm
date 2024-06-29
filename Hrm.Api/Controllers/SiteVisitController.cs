using Hrm.Application;
using Hrm.Shared.Models;
using Hrm.Application.DTOs.SiteVisit;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.SiteVisit.Requests.Queries;
using Hrm.Application.Features.Shift.Requests.Queries;
using Hrm.Application.Features.SiteVisit.Requests.Commands;
using Hrm.Application.Features.Shift.Requests.Commands;
using Hrm.Domain;
using Hrm.Application.DTOs.Shift;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.SiteVisit)]
    [ApiController]
    public class SiteVisitController : Controller
    {
        private readonly IMediator _mediator;
        public SiteVisitController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-SiteVisit")]
        public async Task<ActionResult> GeSiteVisitt()
        {
            var SiteVisit = await _mediator.Send(new GetSiteVisitRequest { });
            return Ok(SiteVisit);
        }

        [HttpGet]
        [Route("get-sitevisitbyid/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Get(int id)
        {
            var SiteVisit = await _mediator.Send(new GetSiteVisitByIdRequest { SiteVisitId = id });

            return Ok(SiteVisit);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-SiteVisit")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSiteVisitDto sitevisit)
        {
            var command = new CreateSiteVisitCommand { SiteVisitDto = sitevisit };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-SiteVisit/{id}")]
        public async Task<ActionResult> Put([FromBody] SiteVisitDto Shift)
        {
            var command = new UpdateSiteVisitCommand { SiteVisitDto = Shift };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-SiteVisit/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSiteVisitCommand { SiteVisitId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
