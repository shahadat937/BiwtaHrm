﻿using Hrm.Application;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Features.Designations.Requests.Queries;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.OfficeBranch.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.Designation)]
    [ApiController]
    [Authorize]
    public class DesignationController : Controller
    {
        private readonly IMediator _mediator;
        public DesignationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-designationDetail/{id}")]
        public async Task<ActionResult<DesignationDto>> Get(int id)
        {
            var Designations = await _mediator.Send(new GetDesignationDetailRequest { DesignationId = id });
            return Ok(Designations);
        }
        [HttpGet]
        [Route("get-selectedDesignations")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDesignation()
        {
            var Designation = await _mediator.Send(new GetSelectedDesignationRequest { });
            return Ok(Designation);
        }


        [HttpGet]
        [Route("get-designation")]
        public async Task<ActionResult> GetDesignation()
        {
            var Designation = await _mediator.Send(new GetDesignationRequest { });
            return Ok(Designation);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-designation")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDesignationDto Designation)
        {
            var command = new CreateDesignationCommand { DesignationDto = Designation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-Designation/{id}")]
        public async Task<ActionResult> Put([FromBody] DesignationDto Designation)
        {
            var command = new UpdateDesignationCommand { DesignationDto = Designation };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Designation/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDesignationCommand { DesignationId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-selectedDesignationByDepartmentId/{id}")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDesignationByDepartmentId(int id)
        {
            var branch = await _mediator.Send(new GetDesignationByDepartmentIdRequest { DepartmentId = id });
            return Ok(branch);

        }

        [HttpGet]
        [Route("get-selectedDesignationBySectionId/{id}")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedDesignationBySectionId(int id)
        {
            var branch = await _mediator.Send(new GetDesignationBySectionIdRequest { SectionId = id });
            return Ok(branch);

        }

        [HttpGet]
        [Route("get-designationByOfficeId/{id}")]
        public async Task<ActionResult<DesignationDto>> GetDesignationByOfficeId(int id)
        {
            var Designations = await _mediator.Send(new GetDesignationByOfficeIdRequest { OfficeId = id });
            return Ok(Designations);
        }

        [HttpGet]
        [Route("get-selectedDesignationByDepartment")]
        public async Task<ActionResult<DesignationDto>> GetDesignationByDepartmentId(int departmentId, int empJobDetailId)
        {
            var Designations = await _mediator.Send(new GetSelectedDesignationByDepartmentIdRequest { DepartmentId = departmentId, EmpJobDetailId = empJobDetailId });
            return Ok(Designations);
        }

        [HttpGet]
        [Route("get-selectedDesignationBySection")]
        public async Task<ActionResult<DesignationDto>> GetDesignationBySectionId(int sectionId, int empJobDetailId)
        {
            var Designations = await _mediator.Send(new GetSelectedDesignationBySectionIdRequest { SectionId = sectionId, EmpJobDetailId = empJobDetailId });
            return Ok(Designations);
        }

        [HttpGet]
        [Route("get-selectedDesignationByOffice")]
        public async Task<ActionResult<DesignationDto>> GetDesignationByOfficeId(int officeId, int empJobDetailId)
        {
            var Designations = await _mediator.Send(new GetSelectedDesignationByOfficeIdRequest { OfficeId = officeId, EmpJobDetailId = empJobDetailId });
            return Ok(Designations);
        }

        [HttpGet]
        [Route("get-designationByOfficeIdAndDepartmentId")]
        public async Task<ActionResult<DesignationDto>> GetDesignationByOfficeIdAndDepartmentId(int officeId, int departmentId)
        {
            var Designations = await _mediator.Send(new GetDesignationByOfficeIdAndDepartmentIdRequest { OfficeId = officeId, DepartmentId = departmentId });
            return Ok(Designations);
        }

        [HttpGet]
        [Route("get-designationPosition")]
        public async Task<ActionResult<DesignationDto>> GetDesignationPosition(int departmentId, int sectionId)
        {
            var Designations = await _mediator.Send(new GetDesignationLastPositionRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(Designations);
        }

        [HttpGet]
        [Route("get-designationNameByDesignationById")]
        public async Task<ActionResult<DesignationDto>> GetDesignationNameByDesignationId(int designationId)
        {
            var Designations = await _mediator.Send(new GetDesignationNameByDesignationIdRequest
            {
                DesignationId = designationId
            });
            return Ok(Designations);
        }
    }
}
