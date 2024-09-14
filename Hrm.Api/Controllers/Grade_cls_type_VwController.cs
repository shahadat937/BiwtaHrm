using Hrm.Application;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.Features.Grade_cls_type_Vw.Requests.Queries;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Grade_cls_type_Vw)]
    [ApiController]
    [Authorize]
    public class Grade_cls_type_VwController : Controller
    {
        private readonly IMediator _mediator;
        public Grade_cls_type_VwController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Grade_cls_type_Vw")]
        public async Task<ActionResult> GetGrade_cls_type_Vw()
        {
            var Scale = await _mediator.Send(new GetGrade_cls_type_VwRequest { });
            return Ok(Scale);
        }
     

    }
}
