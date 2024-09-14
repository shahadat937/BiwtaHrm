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
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.SiteVisit)]
    [ApiController]
    [Authorize]
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

        [HttpGet]
        [Route("get-SiteVisitByFilter")]
        public async Task<ActionResult> GetSiteVisitByFilter([FromQuery] SiteVisitFilterDto filter)
        {
            var command = new GetSiteVisitByFilterRequest { filters = filter };
            var response = await _mediator.Send(command);
            return Ok(response);
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
        public async Task<ActionResult> Put([FromBody] CreateSiteVisitDto SiteVisit)
        {
            var command = new UpdateSiteVisitCommand { SiteVisitDto = SiteVisit };
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

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("approve-SiteVisit/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Approve(int id)
        {
            var command = new ApproveSiteVisitCommand { SiteVisitId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("decline-SiteVisit/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Decline(int id)
        {
            var command = new DeclineSiteVisitCommand { SiteVisitId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
