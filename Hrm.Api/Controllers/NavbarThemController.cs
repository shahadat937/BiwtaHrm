using Hrm.Application;
using Hrm.Application.DTOs.NavbarThem;
using Hrm.Application.Features.NavbarThems.Requests.Commands;
using Hrm.Application.Features.NavbarThems.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.NavbarThem)]
    [ApiController]
    [Authorize]
    public class NavbarThemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NavbarThemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-NavbarThem")]
        public async Task<ActionResult> GetNavbarThem()
        {
            var NavbarThem = await _mediator.Send(new GetNavbarThemRequest { });
            return Ok(NavbarThem);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-NavbarThem")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateNavbarThemDto NavbarThem)
        {
            var command = new CreateNavbarThemCommand { NavbarThemDto = NavbarThem };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-NavbarThem/{id}")]
        public async Task<ActionResult> Put([FromBody] NavbarThemDto NavbarThem)
        {
            var command = new UpdateNavbarThemCommand { NavbarThemDto = NavbarThem };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-NavbarThem/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteNavbarThemCommand { NavbarThemId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-NavbarThembyid/{id}")]
        public async Task<ActionResult<NavbarThemDto>> Get(int id)
        {
            var NavbarThem = await _mediator.Send(new GetNavbarThemByIdRequest { NavbarThemId = id });
            return Ok(NavbarThem);

        }

        [HttpGet]
        [Route("get-selectedNavbarThem")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedNavbarThem()
        {
            var NavbarThem = await _mediator.Send(new GetSelectedNavbarThemRequest { });
            return Ok(NavbarThem);
        }
    }
}