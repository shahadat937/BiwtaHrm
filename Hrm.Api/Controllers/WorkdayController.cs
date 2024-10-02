using Hrm.Application.DTOs.Workday;
using Microsoft.AspNetCore.Mvc;
using Hrm.Shared.Models;
using Hrm.Domain;
using Hrm.Application;
using Hrm.Application.Features.Workday.Requests.Queries;
using Hrm.Application.Features.Workday.Requests.Commands;
using Hrm.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Workday)]
    [ApiController]
    [Authorize]
    public class WorkdayController : Controller
    {
        private readonly IMediator _mediator;

        public WorkdayController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Workday")]
        public async Task<ActionResult> GetWork()
        {
            var workday = await _mediator.Send(new GetWorkdayRequest { });
            return Ok(workday);
        }

        [HttpGet]
        [Route("get-workdayById/{id}")]
        public async Task<ActionResult> Get(int id)
        {

            var workday = await _mediator.Send(new GetWorkdayByIdRequest { WorkdayId = id });
            return Ok(workday);
        }

        [HttpGet]
        [Route("get-SelectedWorkdayByYear/{yearId}")]
        public async Task<ActionResult> GetSelected(int yearId)
        {
            var command = new GetSelectedWorkdayByYearRequest { yearId = yearId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("save-Workday")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateWorkdayDto Workdaydto)
        {
            var command = new CreateWorkdayCommand { WorkdayDto = Workdaydto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Route("update-Workday/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] CreateWorkdayDto workdaydto, int id)
        {
            var command = new UpdateWorkdayCommand { WorkdayDto = workdaydto};
            var response = await _mediator.Send(command);

            return Ok(response);
        }


        [HttpDelete]
        [Route("delete-Workday/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(int id)
        {
            var command = new DeleteWorkdayCommand { WorkdayId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

    }
}
