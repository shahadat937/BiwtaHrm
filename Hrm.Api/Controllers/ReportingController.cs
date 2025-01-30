using Hrm.Application;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Gender.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Increment_and_Promotion.Request.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Religions.Requests.Queries;
using Hrm.Domain;
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
        public async Task<ActionResult<List<object>>> GetEmployeeTypeReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmployeeTypeReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }


        [HttpGet]
        [Route("get-bloodGroupReportingResult")]
        public async Task<ActionResult<List<object>>> GetBloodGroupReportingResult([FromQuery] QueryParams queryParams, int? id)
        {
            var result = await _mediator.Send(new GetBloodGroupReportingResultRequest { QueryParams = queryParams, Id = id });
            return Ok(result);
        }

        //Region
        [HttpGet]
        [Route("get-religionCount")]
        public async Task<ActionResult<object>> GetRegionCount(int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmpCountOnReligionRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }
        [HttpGet]
        [Route("get-religionReportingResult")]
        public async Task<ActionResult<List<object>>> GetReligionReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
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
        public async Task<ActionResult<List<object>>> GetGenderReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
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
        public async Task<ActionResult<List<object>>> GetMaritalStatusReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetMaritalStatusReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        [HttpGet]
        [Route("get-PromotionIncrementReportingResult")]
        public async Task<ActionResult<List<object>>> GetPromotionIncrementRepotingResult([FromQuery] QueryParams queryParams, string? PromotionIncrementType)
        {
            var result = await _mediator.Send(new GetPromotionIncrementReportingRequest { QueryParams = queryParams, PromotionIncrementType = PromotionIncrementType });
            return Ok(result);
        }
        [HttpGet]
        [Route("get-PromotionIncrementReportingCountResult")]
        public async Task<ActionResult<List<object>>> GetPromotionIncrementReportingCount([FromQuery] QueryParams queryParams, string? PromotionType, DateOnly? OrderDateFrom, DateOnly? OrderDateTo, DateOnly? ApproveFrom, DateOnly? ApproveTo)
        {
            var result = await _mediator.Send(new GetPromotionIncrementReportingCountRequest { QueryParams = queryParams, PromotionType = PromotionType, OrderDateFrom = OrderDateFrom, OrderDateTo = OrderDateTo, ApproveFrom = ApproveFrom, ApproveTo = ApproveTo});
            return Ok(result);
        }
    }
}
