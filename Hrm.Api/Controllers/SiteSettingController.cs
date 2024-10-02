using Hrm.Application;
using Hrm.Application.DTOs.SiteSetting;
using Hrm.Application.Enum;
using Hrm.Application.Features.SiteSettings.Requests.Commands;
using Hrm.Application.Features.SiteSettings.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.SiteSetting)]
    [ApiController]
    //[Authorize]
    public class SiteSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SiteSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-SiteSettings")]
        public async Task<ActionResult<List<SiteSettingDto>>> Get()
        {
            var SiteSettings = await _mediator.Send(new GetSiteSettingListRequest { });
            return Ok(SiteSettings);
        }

        [HttpGet]
        [Route("get-ActiveSiteSettings")]
        public async Task<ActionResult<SiteSettingDto>> GetActive()
        {
            var SiteSettings = await _mediator.Send(new GetActiveSiteSettingRequest { });
            return Ok(SiteSettings);
        }

        [HttpGet]
        [Route("get-SiteSettingDetail/{id}")]
        public async Task<ActionResult<SiteSettingDto>> Get(int id)
        {
            var SiteSetting = await _mediator.Send(new GetSiteSettingDetailRequest { Id = id });
            return Ok(SiteSetting);
        }

        [HttpPost]
        [Route("save-SiteSetting")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateSiteSettingDto SiteSetting)
        {
            var command = new CreateSiteSettingCommand { SiteSettingDto = SiteSetting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesDefaultResponseType]
        [Route("update-SiteSetting/{id}")]
        public async Task<ActionResult> Put([FromForm] CreateSiteSettingDto SiteSetting)
        {
            var command = new UpdateSiteSettingCommand { SiteSettingDto = SiteSetting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("delete-SiteSetting/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSiteSettingCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }

}

