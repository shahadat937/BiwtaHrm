using Hrm.Application;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.TrainingName;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.TrainingName;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.TrainingName.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.TrainingName.Requests.Commands;
using Hrm.Application.Features.TrainingName.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.TrainingName)]
    [ApiController]
    public class TrainingNameController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrainingNameController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-trainingName")]
        public async Task<ActionResult> Get()
        {
            var TrainingName = await _mediator.Send(new GetTrainingNameRequest { });
            return Ok(TrainingName);
        }

        [HttpPost]
        [ProducesResponseType(200)] 
        [ProducesResponseType(400)]
        [Route("save-trainingName")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTrainingNameDto TrainingName)
        {
            var command = new CreateTrainingNameCommand { TrainingNameDto = TrainingName };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-trainingName")]
        public async Task<ActionResult>Delete (int id)
        {
            var command = new DeleteTrainingNameCommand { TrainingNameId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-trainingName/{id}")]
        public async Task<ActionResult> Put([FromBody] TrainingNameDto TrainingName)
        {
            var command = new UpdateTrainingNameCommand { TrainingNameDto = TrainingName };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpGet]
        [Route("get-trainingNamebyid/{id}")]
        public async Task<ActionResult<TrainingNameDto>> Get(int id)
        {
            var TrainingName = await _mediator.Send(new GetTrainingNameByIdRequest { TrainingNameId = id });
            return Ok(TrainingName);

        }

        [HttpGet]
        [Route("get-selectedTrainingName")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedTrainingName()
        {
            var TrainingName = await _mediator.Send(new GetSelectedTrainingNameRequest { });
            return Ok(TrainingName);
        }


    }
}
