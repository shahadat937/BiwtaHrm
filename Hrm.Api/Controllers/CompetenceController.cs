using Hrm.Application;
using Hrm.Application.DTOs.Competence;
using Hrm.Application.Features.Competence.Requests.Commands;
using Hrm.Application.Features.Competence.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Shared.Models;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Competence)]
    [ApiController]
    public class Competence : Controller
    {
        private readonly IMediator _mediator;
        public Competence(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-competence")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCompetenceDto Competence)
        {
            var command = new CreateCompetenceCommand { CompetenceDto = Competence };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-competence")]
        public async Task<ActionResult> Get()
        {
            var Competence = await _mediator.Send(new GetCompetenceRequest { });
            return Ok(Competence);
        }

        [HttpGet]
        [Route("get-competencebyid/{id}")]
        public async Task<ActionResult<CompetenceDto>> Get(int id)
        {
            var Competence = await _mediator.Send(new GetCompetenceByIdRequest { CompetenceId = id });
            return Ok(Competence);

        }

        [HttpGet]
        [Route("get-selectedcompetence")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedCompetence()
        {
            var competence = await _mediator.Send(new GetSelectedCompetenceRequest { });
            return Ok(competence);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-competence/{id}")]
        public async Task<ActionResult> Put([FromBody] CompetenceDto Competence)
        {
            var command = new UpdateCompetenceCommand { CompetenceDto = Competence };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-competence/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCompetenceCommand { CompetenceId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
