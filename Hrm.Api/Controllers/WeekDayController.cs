using Hrm.Application;
using Hrm.Application.DTOs.WeekDay;
using Hrm.Application.DTOs.Gender;
using Hrm.Application.Features.Weekend.Handlers.Queries;
using Hrm.Application.Features.Weekend.Requests.Commands;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Application.Features.WeekDay.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.WeekDay)]
    [ApiController]
    [Authorize]
    public class WeekDayController : Controller
    {
        private readonly IMediator _mediator;
        public WeekDayController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-weekday")]
        public async Task<ActionResult> GetWeekend()
        {
            var Weekend = await _mediator.Send(new GetWeekDayRequest { });
            return Ok(Weekend);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-weekday")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateWeekDayDto Weekend)
        {
            var command = new CreateWeekDayCommand { WeekendDto = Weekend };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-weekday/{id}")]
        public async Task<ActionResult> Put([FromBody] WeekDayDto Weekend)
        {
            var command = new UpdateWeekDayCommand { WeekendDto = Weekend };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-weekday/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteWeekDayCommand { WeekendId = id };
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet]
        [Route("get-weekDayDetail/{id}")]
        public async Task<ActionResult<WeekDayDto>> Get(int id)
        {
            var WeekDays = await _mediator.Send(new GetWeekDayDetailRequest { WeekDayId = id });
            return Ok(WeekDays);
        }
        [HttpGet]
        [Route("get-selectedWeekDays")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedWeekDay()
        {
            var weekDays = await _mediator.Send(new GetSelectedWeekDayRequest { });
            return Ok(weekDays);
        }

    }
}
