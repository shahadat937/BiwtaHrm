using Hrm.Application;
using Hrm.Application.DTOs.HolidayType;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Features.HolidayType.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;
using Hrm.Application.Features.HolidayType.Handlers.Queries;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.HolidayType)]
    [ApiController]
    public class HolidayType : Controller
    {
        private readonly IMediator _mediator;
        public HolidayType(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-HolidayType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHolidayTypeDto HolidayType)
        {
            var command = new CreateHolidayTypeCommand { HolidayTypeDto = HolidayType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-HolidayType")]
        public async Task<ActionResult> Get()
        {
            var HolidayType = await _mediator.Send(new GetHolidayTypeRequest { });
            return Ok(HolidayType);
        }

        [HttpGet]
        [Route("get-HolidayTypebyid/{id}")]
        public async Task<ActionResult<HolidayTypeDto>> Get(int id)
        {
            var HolidayType = await _mediator.Send(new GetHolidayTypeByIdRequest { HolidayTypeId = id });
            return Ok(HolidayType);

        }

        //[HttpGet]
        //[Route("get-selectedHolidayType")]
        //public async Task<ActionResult<List<SelectedModel>>> GetSelectedHolidayType()
        //{
        //    var HolidayType = await _mediator.Send(new GetSelectedHolidayTypeRequest { });
        //    return Ok(HolidayType);
        //}

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-HolidayType/{id}")]
        public async Task<ActionResult> Put([FromBody] HolidayTypeDto HolidayType)
        {
            var command = new UpdateHolidayTypeCommand { HolidayTypeDto = HolidayType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-HolidayType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteHolidayTypeCommand { HolidayTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
