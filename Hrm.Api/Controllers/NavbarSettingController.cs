using Hrm.Application;
using Hrm.Application.DTOs.NavbarSetting;
using Hrm.Application.Features.NavbarSettings.Requests.Commands;
using Hrm.Application.Features.NavbarSettings.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.NavbarSetting)]
    [ApiController]
    [Authorize]
    public class NavbarSettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NavbarSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-NavbarSettings")]
        public async Task<ActionResult<List<NavbarSettingDto>>> Get()
        {
            var NavbarSettings = await _mediator.Send(new GetNavbarSettingListRequest { });
            return Ok(NavbarSettings);
        }

        [HttpGet]
        [Route("get-ActiveNavbarSettings")]
        public async Task<ActionResult<NavbarSettingDto>> GetActive()
        {
            var NavbarSettings = await _mediator.Send(new GetActiveNavbarSettingRequest { });
            return Ok(NavbarSettings);
        }

        [HttpGet]
        [Route("get-NavbarSettingDetail/{id}")]
        public async Task<ActionResult<NavbarSettingDto>> Get(int id)
        {
            var NavbarSetting = await _mediator.Send(new GetNavbarSettingDetailRequest { Id = id });
            return Ok(NavbarSetting);
        }

        [HttpPost]
        [Route("save-NavbarSetting")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateNavbarSettingDto NavbarSetting)
        {
            var command = new CreateNavbarSettingCommand { NavbarSettingDto = NavbarSetting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-NavbarSetting/{id}")]
        public async Task<ActionResult> Put([FromForm] CreateNavbarSettingDto NavbarSetting)
        {
            var command = new UpdateNavbarSettingCommand { NavbarSettingDto = NavbarSetting };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-NavbarSetting/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteNavbarSettingCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
