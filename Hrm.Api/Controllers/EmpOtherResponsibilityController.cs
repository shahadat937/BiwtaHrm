using Hrm.Application;
using Hrm.Application.DTOs.EmpOtherResponsibility;
using Hrm.Application.Features.EmpOtherResponsibilities.Requests.Commands;
using Hrm.Application.Features.EmpOtherResponsibilities.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.EmpOtherResponsibility)]
    [ApiController]
    [Authorize]
    public class EmpOtherResponsibilityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpOtherResponsibilityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("save-EmpOtherResponsibility")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpOtherResponsibilityDto EmpOtherResponsibility)
        {
            var command = new CreateEmpOtherResponsibilityCommand { EmpOtherResponsibilityDto = EmpOtherResponsibility };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-EmpOtherResponsibilityByEmpId/{id}")]
        public async Task<ActionResult<EmpOtherResponsibilityDto>> Get(int id)
        {
            var EmpOtherResponsibilities = await _mediator.Send(new GetEmpOtherResponsibilityByEmpIdRequest { Id = id });
            return Ok(EmpOtherResponsibilities);
        }

        [HttpGet]
        [Route("get-InActiveEmpOtherResponsibilityByEmpId/{id}")]
        public async Task<ActionResult<EmpOtherResponsibilityDto>> GetActive(int id)
        {
            var EmpOtherResponsibilities = await _mediator.Send(new GetInActiveEmpOtherResponsibilityRequest { Id = id });
            return Ok(EmpOtherResponsibilities);
        }

        [HttpGet]
        [Route("get-EmpOtherResponsibilityDetails/{id}")]
        public async Task<ActionResult<EmpOtherResponsibilityDto>> GetById(int id)
        {
            var EmpOtherResponsibilities = await _mediator.Send(new GetEmpOtherResponsibilityDetailsRequest { Id = id });
            return Ok(EmpOtherResponsibilities);
        }

        [HttpGet]
        [Route("get-allEmpOtherResponsibilityByEmpId/{id}")]
        public async Task<ActionResult<EmpOtherResponsibilityDto>> GetAll(int id)
        {
            var EmpOtherResponsibilities = await _mediator.Send(new GetAllEmpOtherResponsibilityByEmpIdRequest { Id = id });
            return Ok(EmpOtherResponsibilities);
        }

        [HttpGet]
        [Route("update-EmpOtherResponsibilityStatusByEmpId/{id}")]
        public async Task<ActionResult<EmpOtherResponsibilityDto>> Update(int id)
        {
            var EmpOtherResponsibilities = await _mediator.Send(new UpdateEmpOtherResponsibilityStatusCommand { Id = id });
            return Ok(EmpOtherResponsibilities);
        }

        [HttpDelete]
        [Route("delete-EmpOtherResponsibility/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmpOtherResponsibilityCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
