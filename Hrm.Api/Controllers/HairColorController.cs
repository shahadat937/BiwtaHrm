using Hrm.Application;
using Hrm.Application.DTOs.HairColor;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.HairColor.Requests.Commands;
using Hrm.Application.Features.HairColor.Requests.Queries;
using Hrm.Application.Features.HairColors.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.HairColor)]
    [ApiController]
    [Authorize]
    public class HairColorController : Controller
    {
        private readonly IMediator _mediator;
        public HairColorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-hairColor")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHairColorDto HairColor)
        {
            var command = new CreateHairColorCommand { HairColorDto = HairColor };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-hairColor")]
        public async Task<ActionResult> Get()
        {
            var HairColor = await _mediator.Send(new GetHairColorRequest { });
            return Ok(HairColor);
        }
        [HttpGet]
        [Route("get-hairColorDetail/{id}")]
        public async Task<ActionResult<HairColorDto>> Get(int id)
        {
            var HairColors = await _mediator.Send(new GetHairColorDetailRequest { HairColorId = id });
            return Ok(HairColors);
        }
        [HttpGet]
        [Route("get-selectedHairColors")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedHairColor()
        {
            var HairColor = await _mediator.Send(new GetSelectedHairColorRequest { });
            return Ok(HairColor);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-hairColor/{id}")]
        public async Task<ActionResult> Put([FromBody] HairColorDto HairColor)
        {
            var command = new UpdateHairColorCommand { HairColorDto = HairColor };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-hairColor/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteHairColorCommand { HairColorId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
