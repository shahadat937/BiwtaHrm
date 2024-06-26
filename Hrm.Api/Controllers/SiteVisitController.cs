using Hrm.Application;
using Hrm.Shared.Models;
using Hrm.Application.DTOs.SiteVisit;
using Microsoft.AspNetCore.Mvc;
using Hrm.Features.SiteVisit.Requests.Commands;
using Hrm.Features.SiteVisit.Requests.Queries;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.SiteVisit)]
    [ApiController]
    public class SiteVisitController: Controller
    {
        private readonly Imediator _imediator;
        public SiteVisitController(IMediator mediator) {
            _imediator= mediator;
        }

        [HttpGet]
        [Route("get-SiteVisit")]
        public async Task<ActionResult> Get()
        {
            var SiteVisit = await _imediator.Send(new GetSiteVisitRequest { });
            return Ok(SiteVisit);
        }
    }
}
