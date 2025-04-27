using Hrm.Application;
using Hrm.Application.DTOs.EmpJobDetail;
using Hrm.Application.Features.EmpJobDetails.Requests.Commands;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.EmpJobDetail)]
    [ApiController]
    [Authorize]
    public class EmpJobDetailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmpJobDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-EmpJobDetailByEmpId/{id}")]
        public async Task<ActionResult<EmpJobDetailDto>> GetEmpJobDetailsById(int id)
        {
            var EmpJobDetails = await _mediator.Send(new GetEmpJobDetailByIdRequest { Id = id });
            return Ok(EmpJobDetails);
        }


        [HttpPost]
        [Route("save-EmpJobDetails")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEmpJobDetailDto EmpJobDetails)
        {
            var command = new CreateEmpJobDetailCommand { EmpJobDetailDto = EmpJobDetails };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [Route("update-EmpJobDetails/{id}")]
        public async Task<ActionResult> Put([FromBody] EmpJobDetailDto EmpJobDetails)
        {
            var command = new UpdateEmpJobDetailCommand { EmpJobDetailDto = EmpJobDetails };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-selectedDepatmentNameById/{empId}")]
        public async Task<ActionResult> SelectedDepartmentByEmpId(int empId)
        {
            var command = new GetSelectedDepartmentByEmpIdRequest { EmpId = empId };
            var EmpJobDetails = await _mediator.Send(command);
            return Ok(EmpJobDetails);
        }

        [HttpGet]
        [Route("get-selectedSelectionNameByEmpIdAndDpepartmentId")]
        public async Task<ActionResult> SelectedSectionByEmpIdAndDepartmentId(int empId, int departmentId)
        {
            var command = new GetSelectedSectionByEmpIdAndDepartmentIdRequest {
                EmpId = empId,
                DepartmentId = departmentId
            };
            var EmpJobDetails = await _mediator.Send(command);
            return Ok(EmpJobDetails);
        }

        [HttpGet]
        [Route("get-selectedDesignationByEmpIdAndDpepartmentIdAndSectionId")]
        public async Task<ActionResult> SelectedSectionByEmpIdAndDepartmentIdAndSectionId(int empId, int departmentId, int sectionId)
        {
            var command = new GetSelectedDesignationByEmpIdAndDepartmentIdAndSectionIdRequest
            {
                EmpId = empId,
                DepartmentId = departmentId,
                SectionId = sectionId
            };
            var EmpJobDetails = await _mediator.Send(command);
            return Ok(EmpJobDetails);
        }
    }
}
