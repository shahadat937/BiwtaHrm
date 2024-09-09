using Hrm.Application;
using Hrm.Application.Features.Dashboards.Requests.Queries;
using Hrm.Application.Features.Year.Requests.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Widgets)]
    [ApiController]
    public class WidgetsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public WidgetsController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        [Route("get-widgets")]
        public async Task<ActionResult> Get()
        {
            var widgets = await _mediator.Send(new GetDashboardWidgetsRequest { });
            return Ok(widgets);
        }
    }
}
