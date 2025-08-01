﻿using Hrm.Application;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Gender.Requests.Queries;
using Hrm.Application.Features.Reportings.Increment_and_Promotion.Request.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Language.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.TrainingTypes.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Religions.Requests.Queries;
using Hrm.Application.Features.Reportings.EmployeeList.Requests.Queries;
using Hrm.Application.Features.Reportings.TransferPosting.Requests.Queries;
using Hrm.Application.Features.Reportings.TransferPostingReporting.Requests.Queries;
using Hrm.Application.Features.Reportings.VacancyReport.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Hrm.Application.Features.Reportings.AddressReporting.Requests.Queries;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Reporting)]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<object>> GetLanguageReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetLanguageReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        //TrainingType 
        [HttpGet]
        [Route("get-TrainingTypeCount")]
        public async Task<ActionResult<object>> GetTrainingTypeCount(int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmpCountOnTrainingTypeRequest { DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        [HttpGet]
        [Route("get-TrainingTypeReportingResult")]
        public async Task<ActionResult<object>> GetTrainingTypeReportingResult([FromQuery] QueryParams queryParams, int? id, bool? unAssigned, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetTrainingTypeReportingResultRequest { QueryParams = queryParams, Id = id, UnAssigned = unAssigned, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }


        [HttpGet]
        [Route("get-employeeListReporting")]
        public async Task<ActionResult<object>> GetEmployeeListReporting([FromQuery] QueryParams queryParams, int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetEmployeeListReportingRequest { QueryParams = queryParams,DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }

        //Promotion and increment
        [HttpGet]
        [Route("get-PromotionIncrementReportingResult")]
        public async Task<ActionResult<List<object>>> GetPromotionIncrementRepotingResult([FromQuery] QueryParams queryParams, string? PromotionIncrementType, DateOnly? OrderDateFrom, DateOnly? OrderDateTo, DateOnly? EffectiveDateFrom, DateOnly? EffectiveDateTo, int? CurrentDepartmentId)
        {
            var result = await _mediator.Send(new GetPromotionIncrementReportingRequest { QueryParams = queryParams,CurrentDepartmentId=CurrentDepartmentId, PromotionIncrementType = PromotionIncrementType, OrderDateFrom=OrderDateFrom, OrderDateTo = OrderDateTo, EffectiveDateFrom= EffectiveDateFrom, EffectiveDateTo = EffectiveDateTo });
            return Ok(result);
        }
        [HttpGet]
        [Route("get-PromotionIncrementReportingCountResult")]
        public async Task<ActionResult<List<object>>> GetPromotionIncrementReportingCount([FromQuery] QueryParams queryParams, string? PromotionType, DateOnly? OrderDateFrom, DateOnly? OrderDateTo, DateOnly? ApproveFrom, DateOnly? ApproveTo, DateOnly? EffectiveDateFrom, DateOnly? EffectiveDateTo)
        {
            var result = await _mediator.Send(new GetPromotionIncrementReportingCountRequest { QueryParams = queryParams, PromotionType = PromotionType, OrderDateFrom = OrderDateFrom, OrderDateTo = OrderDateTo, ApproveFrom = ApproveFrom, ApproveTo = ApproveTo, EffectiveDateFrom=EffectiveDateFrom, EffectiveDateTo=EffectiveDateTo});
            return Ok(result);
        }

        //TransferPosting 
        [HttpGet]
        [Route("get-TransferPostingCount")]
        public async Task<ActionResult<object>> GetTransferPostingCount(int departmentFrom, int sectionFrom, int departmentTo, int sectionTo, DateOnly? dateTo, DateOnly? dateFrom)
        {
            var result = await _mediator.Send(new GetTransferPostingCountRequest {
            DepartmentFrom = departmentFrom,
            DepartmentTo = departmentTo,
            SectionFrom = sectionFrom,
            SectionTo = sectionTo,
            DateTo = dateTo,
            DateFrom = dateFrom
            });
            return Ok(result);
        }
        
        [HttpGet]
        [Route("get-TransferPostingReport")]
        public async Task<ActionResult<object>> GetTransferPostingReport( [FromQuery]QueryParams queryParams, int departmentFrom, int sectionFrom, int departmentTo, int sectionTo,  DateOnly? dateFrom, DateOnly? dateTo, bool? departmentStatus, bool? joiningStatus)
        {
            var result = await _mediator.Send(new GetTransferPostingResultRequest
            {
            DepartmentFrom = departmentFrom,
            DepartmentTo = departmentTo,
            SectionFrom = sectionFrom,
            SectionTo = sectionTo,
            DateTo = dateTo,
            DateFrom = dateFrom,
            QueryParams = queryParams,
            DepartmentStatus = departmentStatus,
            JoiningStatus = joiningStatus
            });
            return Ok(result);
        }


        //Vacant Reporting
        [HttpGet]
        [Route("get-vacantReportingResult")]
        public async Task<ActionResult<object>> GetVacantReportingResult([FromQuery] QueryParams queryParams,int? departmentId, int? sectionId)
        {
            var result = await _mediator.Send(new GetVacancyReportRequest { QueryParams = queryParams, DepartmentId = departmentId, SectionId = sectionId });
            return Ok(result);
        }


        //Address Reporting
        [HttpGet]
        [Route("get-addressReportingResult")]
        public async Task<ActionResult<object>> GetAddressReportingResult([FromQuery] QueryParams queryParams, bool isPresentAddress, int? departmentId, int? sectionId, int? countryId, int? divisionId, int? districtId, int? upazilaId)
        {
            var result = await _mediator.Send(new GetAddressReportingRequest { QueryParams = queryParams, IsPresentAddress = isPresentAddress, DepartmentId = departmentId, SectionId = sectionId, CountryId = countryId, DivisionId = divisionId, DistrictId = districtId, UpazilaId = upazilaId });
            return Ok(result);
        }


        //Leave Reporting
        [HttpGet]
        [Route("get-leaveReportingResult")]
        public async Task<ActionResult<object>> GetLeaveReportingResult([FromQuery] QueryParams queryParams, int? employeeId, int? departmentId, int? sectionId, int? designationId, int? leaveType, string? fromDate, string? toDate)
        {
            var result = await _mediator.Send(new GetLeaveReportRequest { 
                QueryParams = queryParams, 
                EmployeeId = employeeId,
                DepartmentId = departmentId,
                SectionId = sectionId,
                DesignationId = designationId,
                LeaveTypeId = leaveType,
                FromDate = fromDate,
                ToDate = toDate
            });
            return Ok(result);
        }


        //Leave Reporting
        [HttpGet]
        [Route("get-prlReportingResult")]
        public async Task<ActionResult<object>> GetPrlReportingResult([FromQuery] QueryParams QueryParams , string? CurrentDate, string? StartDate, string? EndDate,int? DepartmentId, int? SectionId, int? DesignationId, bool IsPRL, bool IsRetirment, bool IsGone, bool IsWillGone)
        {
            var result = await _mediator.Send(new GetPRLReportRequest
            {
                QueryParams = QueryParams,
                CurrentDate = CurrentDate,
                StartDate = StartDate,
                EndDate = EndDate,
                DepartmentId = DepartmentId,
                SectionId = SectionId,
                DesignationId = DesignationId,
                IsPRL = IsPRL,
                IsRetirment = IsRetirment,
                IsGone = IsGone,
                IsWillGone = IsWillGone
            });
            return Ok(result);
        }
    }
}
