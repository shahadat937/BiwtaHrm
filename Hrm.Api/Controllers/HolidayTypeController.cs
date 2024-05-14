using Hrm.Application;
using Hrm.Application.DTOs.HolidayType;
using Hrm.Application.DTOs.Year;
using Hrm.Application.Features.HolidayType.Handlers.Queries;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Features.HolidayType.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Features.Year.Requests.Queries;
using Hrm.Shared.Models;
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
        [Route("save-holidayType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHolidayTypeDto HolidayType)
        {
            var command = new CreateHolidayTypeCommand { HolidayTypeDto = HolidayType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-holidayType")]
        public async Task<ActionResult> Get()
        {
            var Year = await _mediator.Send(new GetHolidayTypeRequest { });
            return Ok(Year);
        }
        [HttpGet]
        [Route("get-holidayTypeDetail/{id}")]
        public async Task<ActionResult<YearDto>> Get(int id)
        {
            var HolidayType = await _mediator.Send(new GetHolidayTypeDetailRequest { HolidayTypeId = id });
            return Ok(HolidayType);
        }
        [HttpGet]
        [Route("get-selectedholidayTypes")]
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
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-holidayType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteHolidayTypeCommand { HolidayTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
