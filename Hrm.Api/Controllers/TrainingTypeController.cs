using Hrm.Application;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Features.TrainingType.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.TrainingType.Requests.Commands;
using Hrm.Application.Features.TrainingType.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.TrainingType)]
    [ApiController]
    [Authorize]
    public class TrainingTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrainingTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("save-trainingType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTrainingTypeDto traineeType)
        {
            var command = new CreateTrainingTypeCommand { TrainingTypeDto = traineeType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-trainingType")]
        public async Task<ActionResult> Get()
        {
            var TrainingType = await _mediator.Send(new GetTrainingTypeRequest { });
            return Ok(TrainingType);
        }


        [HttpPut]
        [Route("update-trainingType/{id}")]
        public async Task<ActionResult> Put([FromBody] TrainingTypeDto trainingTypeDto)
        {
            var command = new UpdateTrainingTypeCommand { TrainingTypeDto = trainingTypeDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-trainingType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteTrainingTypeCommand { TrainingTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-trainingtypebyid/{id}")]
        public async Task<ActionResult<TrainingTypeDto>> Get(int id)
        {
            var TrainingType = await _mediator.Send(new GetTrainingTypeByIdRequest { TrainingTypeId = id });
            return Ok(TrainingType);

        }

        [HttpGet]
        [Route("get-selectedtrainingtype")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedTrainingType()
        {
            var TrainingType = await _mediator.Send(new GetSelectedTrainingTypeRequest { });
            return Ok(TrainingType);
        }
    }
}
