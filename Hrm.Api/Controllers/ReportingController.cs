using Hrm.Application;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Gender.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Language.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Religions.Requests.Queries;
using Hrm.Application.Features.Reportings.EmployeeList.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Reporting)]
    [ApiController]
    //[Authorize]
    public class ReportingController : Controller
    {
        private readonly IMediator _mediator;
        public ReportingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Employee Information Start from Here
            //Employee Type
        [HttpGet]
        [Route("get-employeeTypeCount")]
        public async Task<ActionResult<object>> GetEmployeeTypeCount(int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmpCountOnEmployeeTypeRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }
        [HttpGet]
        [Route("get-employeeTypeReportingResult")]
        public async Task<ActionResult<object>> GetEmployeeTypeReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmployeeTypeReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        //Blood Group
        [HttpGet]
        [Route("get-BloodGroupCount")]
        public async Task<ActionResult<object>> GetBloodGroupCount(int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmpCountOnBloodGroupRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }
        [HttpGet]
        [Route("get-BloodGroupReportingResult")]
        public async Task<ActionResult<object>> GetBloodGroupReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetBloodGroupReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        //Religion
        [HttpGet]
        [Route("get-religionCount")]
        public async Task<ActionResult<object>> GetReligionCount(int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmpCountOnReligionRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }
        [HttpGet]
        [Route("get-religionReportingResult")]
        public async Task<ActionResult<object>> GetReligionReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetReligionReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        //Gender 
        [HttpGet]
        [Route("get-genderCount")]
        public async Task<ActionResult<object>> GetGenderCount(int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmpCountOnGenderRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        [HttpGet]
        [Route("get-genderReportingResult")]
        public async Task<ActionResult<object>> GetGenderReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetGenderReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }  
        
        //MaritalStatus 
        [HttpGet]
        [Route("get-maritalStatusCount")]
        public async Task<ActionResult<object>> GetMaritalStatusCount(int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmpCountOnMaritalStatusRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        [HttpGet]
        [Route("get-MaritalStatusReportingResult")]
        public async Task<ActionResult<object>> GetMaritalStatusReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetMaritalStatusReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }


        [HttpGet]
        [Route("get-employeeListReporting")]
        public async Task<ActionResult<object>> GetEmployeeListReporting([FromQuery] QueryParams queryParams, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmployeeListReportingRequest { QueryParams = queryParams,DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        //Language
        [HttpGet]
        [Route("get-languageCount")]
        public async Task<ActionResult<object>> GetLanguageCount(int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmpCountOnLanguageRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        [HttpGet]
        [Route("get-LanguageReportingResult")]
        public async Task<ActionResult<List<object>>> GetLanguageReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetLanguageReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }
    }
}
