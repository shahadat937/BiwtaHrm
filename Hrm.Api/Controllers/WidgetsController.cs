﻿using Hrm.Application;
using Hrm.Application.Features.Dashboards.Requests.Queries;
using Hrm.Application.Features.Year.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Widgets)]
    [ApiController]
    [Authorize]
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


        [HttpGet]
        [Route("get-userWidgets")]
        public async Task<ActionResult> GetUserWidgets()
        {
            var widgets = await _mediator.Send(new GetDahsboardUserInfoRequest { });
            return Ok(widgets);
        }


        [HttpGet]
        [Route("get-gendersChart")]
        public async Task<ActionResult> GetGendersChart()
        {
            var widgets = await _mediator.Send(new GetDashboardGenderInfoRequest { });
            return Ok(widgets);
        }

        [HttpGet]
        [Route("get-fieldUnfieldChart")]
        public async Task<ActionResult> GetFieldUnfieldChart()
        {
            var widgets = await _mediator.Send(new GetDashboardFieldUnfieldInfoRequest { });
            return Ok(widgets);
        }

        [HttpGet]
        [Route("get-transferWidgets")]
        public async Task<ActionResult> GetTransferWidgets()
        {
            var widgets = await _mediator.Send(new GetDashboardTransferInfoRequest { });
            return Ok(widgets);
        }

        [HttpGet]
        [Route("get-promotionWidgets")]
        public async Task<ActionResult> GetPromotionWidgets()
        {
            var widgets = await _mediator.Send(new GetDashboardPromotionInfoRequest { });
            return Ok(widgets);
        }
    }
}
