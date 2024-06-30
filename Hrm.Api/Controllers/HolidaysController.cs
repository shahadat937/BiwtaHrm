using AutoMapper;
using Hrm.Application;
using Hrm.Application.Features.Holidays.Requests.Queries;
using Hrm.Application.Features.HolidayType.Requests.Queries;
using Hrm.Persistence.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

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
    }
}
