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
using Hrm.Application.DTOs.Year;
using Hrm.Application.Features.Year.Requests.Queries;
using Hrm.Shared.Models;
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
        [Route("save-holidayType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHolidayTypeDto HolidayType)
        {
            var command = new CreateEmployeeCommand { HolidayTypeDto = HolidayType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-holidayTypeDetail/{id}")]
        public async Task<ActionResult<HolidayTypeDto>> Get(int id)
        {
            var HolidayType = await _mediator.Send(new GetHolidayTypeDetailRequest { HolidayTypeId = id });
            return Ok(HolidayType);
        }
        [HttpGet]
        [Route("get-selectedHolidayType")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedHolidayType()
        {
            var HolidayType = await _mediator.Send(new GetSelectedHolidayTypeRequest { });
            return Ok(HolidayType);
        }


        [HttpPut]
        [Route("update-holidayType/{id}")]
        public async Task<ActionResult> Put([FromBody] HolidayTypeDto HolidayType)
        {
            var command = new UpdateHolidayTypeCommand { HolidayTypeDto = HolidayType };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete-holidayType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteHolidayTypeCommand { HolidayTypeId = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
