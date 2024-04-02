using Hrm.Application;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.Features.ScaleGradeView.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.ScaleGradeView)]
    [ApiController]
    public class ScaleGradeViewController : Controller
    {
        private readonly IMediator _mediator;
        public ScaleGradeViewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-scaleGradeView")]
        public async Task<ActionResult> GetScaleGradeView()
        {
            var Scale = await _mediator.Send(new GetScaleGradeViewRequest { });
            return Ok(Scale);
        }
     

    }
}
