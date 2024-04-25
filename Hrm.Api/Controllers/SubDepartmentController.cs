using Hrm.Application;
using Hrm.Application.DTOs.SubDepartment;
using Hrm.Application.Features.SubDepartment.Requests.Commands;
using Hrm.Application.Features.SubDepartment.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Shared.Models;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.SubDepartment)]
    [ApiController]
    public class SubDepartmentController : Controller
    {
        private readonly IMediator _mediator;
        public SubDepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-subDepartment")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSubDepartmentDto SubDepartment)
        {
            var command = new CreateSubDepartmentCommand { SubDepartmentDto = SubDepartment };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-subDepartment")]
        public async Task<ActionResult> Get()
        {
            var SubDepartment = await _mediator.Send(new GetSubDepartmentRequest { });
            return Ok(SubDepartment);
        }

        [HttpGet]
        [Route("get-subDepartmentbyid/{id}")]
        public async Task<ActionResult<SubDepartmentDto>> Get(int id)
        {
            var SubDepartment = await _mediator.Send(new GetSubDepartmentByIdRequest { SubDepartmentId = id });
            return Ok(SubDepartment);

        }

        [HttpGet]
        [Route("get-selectedSubDepartment")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedSubDepartment()
        {
            var SubDepartment = await _mediator.Send(new GetSelectedSubDepartmentRequest { });
            return Ok(SubDepartment);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-subDepartment/{id}")]
        public async Task<ActionResult> Put([FromBody] SubDepartmentDto SubDepartment)
        {
            var command = new UpdateSubDepartmentCommand { SubDepartmentDto = SubDepartment };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-subDepartment/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSubDepartmentCommand { SubDepartmentId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
