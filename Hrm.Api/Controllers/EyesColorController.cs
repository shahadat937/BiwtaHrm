using Hrm.Application;
using Hrm.Application.DTOs.EyesColor;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.EyesColor.Requests.Commands;
using Hrm.Application.Features.EyesColor.Requests.Queries;
using Hrm.Application.Features.EyesColors.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.EyesColor)]
    [ApiController]
    public class EyesColorController : Controller
    {
        private readonly IMediator _mediator;
        public EyesColorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-eyesColor")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEyesColorDto EyesColor)
        {
            var command = new CreateEyesColorCommand { EyesColorDto = EyesColor };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-eyesColor")]
        public async Task<ActionResult> Get()
        {
            var EyesColor = await _mediator.Send(new GetEyesColorRequest { });
            return Ok(EyesColor);
        }
        [HttpGet]
        [Route("get-eyesColorDetail/{id}")]
        public async Task<ActionResult<EyesColorDto>> Get(int id)
        {
            var EyesColors = await _mediator.Send(new GetEyesColorDetailRequest { EyesColorId = id });
            return Ok(EyesColors);
        }
        [HttpGet]
        [Route("get-selectedEyesColors")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedEyesColor()
        {
            var EyesColor = await _mediator.Send(new GetSelectedEyesColorRequest { });
            return Ok(EyesColor);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-eyesColor/{id}")]
        public async Task<ActionResult> Put([FromBody] EyesColorDto EyesColor)
        {
            var command = new UpdateEyesColorCommand { EyesColorDto = EyesColor };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-eyesColor/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEyesColorCommand { EyesColorId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
