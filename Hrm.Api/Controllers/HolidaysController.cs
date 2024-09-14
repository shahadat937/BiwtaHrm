using AutoMapper;
using Hrm.Application;
using Hrm.Application.DTOs.Holidays;
using Hrm.Application.Features.Holidays.Requests.Commands;
using Hrm.Application.Features.Holidays.Requests.Queries;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Features.HolidayType.Requests.Queries;
using Hrm.Persistence.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Holidays)]
    public class HolidaysController: Controller
    {
        private readonly IMediator _mediator;

        public HolidaysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("save-Holidays")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHolidayDto holidayDto, [FromQuery] DateOnly HolidayFrom, [FromQuery] DateOnly HolidayTo)
        {
            var command = new CreateHolidaysCommand { HolidayDto = holidayDto, DateFrom = HolidayFrom, DateTo = HolidayTo };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-Holidays")]
        public async Task<ActionResult> GetHolidays()
        {
            var holidays = await _mediator.Send(new GetHolidaysRequest());
            return Ok(holidays);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-HolidaysById/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var holiday = await _mediator.Send(new GetHolidaysByIdRequest { HolidayId = id });

            return Ok(holiday);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("get-HolidaysByYear/{year}")]
        public async Task<ActionResult> GetHolidaysByYear(int year)
        {
            var holidays = await _mediator.Send(new GetHolidaysByYearRequest { YearName = year });
            return Ok(holidays);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-Holidays/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(int id)
        {
            var command = new DeleteHolidaysCommand { HolidayId=id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-HolidaysByGroupId/{GroupId}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteByGroupId(int GroupId)
        {
            var command = new DeleteHolidaysByGroupIdCommand { GroupId = GroupId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-Holidays")]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] CreateHolidayDto holidayDto)
        {
            var command = new UpdateHolidaysCommand { HolidayDto = holidayDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("update-HolidaysByGroupId")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateByGroupId([FromQuery] int GroupId, [FromQuery] DateOnly From, [FromQuery] DateOnly To, [FromBody] CreateHolidayDto holidayDto)
        {
            var command = new UpdateHolidaysByGroupIdCommand { From = From, To = To, GroupId = GroupId, HolidayDto = holidayDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }


    }
}
