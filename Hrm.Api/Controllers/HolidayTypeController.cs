using Hrm.Application;
using Hrm.Application.DTOs.HolidayType;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.Features.HolidayType.Handlers.Queries;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.HolidayType)]
    [ApiController]
    public class HolidayTypeController : Controller
    {
        private readonly IMediator _mediator;
        public HolidayTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-holidayType")]
        public async Task<ActionResult> GetHolidayType()
        {
            var HolidayType = await _mediator.Send(new GetHolidayTypeRequest { });
            return Ok(HolidayType);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-holidayType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHolidayTypeDto HolidayType)
        {
            var command = new CreateEmployeeCommand { HolidayTypeDto = HolidayType };
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
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-holidayType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteHolidayTypeCommand { HolidayTypeId = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
