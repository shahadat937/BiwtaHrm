using Hrm.Application;
using Hrm.Application.DTOs.Institute;
using Hrm.Application.Features.Institute.Requests.Commands;
using Hrm.Application.Features.Institute.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Institute)]
    [ApiController]
    public class Institute : Controller
    {
        private readonly IMediator _mediator;
        public Institute(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-institute")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateInstituteDto Institute)
        {
            var command = new CreateInstituteCommand { InstituteDto = Institute };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-institute")]
        public async Task<ActionResult> Get()
        {
            var Institute = await _mediator.Send(new GetInstituteRequest { });
            return Ok(Institute);
        }

        [HttpGet]
        [Route("get-institutebyid/{id}")]
        public async Task<ActionResult<InstituteDto>> Get(int id)
        {
            var Institute = await _mediator.Send(new GetInstituteByIdRequest { InstituteId = id });
            return Ok(Institute);

        }

        [HttpGet]
        [Route("get-selectedinstitute")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedInstitute()
        {
            var institute = await _mediator.Send(new GetSelectedInstituteRequest { });
            return Ok(institute);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-institute/{id}")]
        public async Task<ActionResult> Put([FromBody] InstituteDto Institute)
        {
            var command = new UpdateInstituteCommand { InstituteDto = Institute };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-institute/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteInstituteCommand { InstituteId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
