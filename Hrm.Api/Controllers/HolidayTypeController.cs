using Hrm.Application;
using Hrm.Application.DTOs.HolidayType;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Features.HolidayType.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("save-holidayType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHolidayTypeDto HolidayType)
        {
            var command = new CreateHolidayTypeCommand { HolidayTypeDto = HolidayType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-holidayType/{id}")]
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
        [Route("delete-holidayType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteHolidayTypeCommand { HolidayTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
