using Hrm.Application;
using Hrm.Application.DTOs.EmpBasicInfo;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries;
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
        public async Task<ActionResult<object>> GetEmployeeTypeCount()
        {
            var employeeType = await _mediator.Send(new GetEmpCountOnEmployeeTypeRequest { });
            return Ok(employeeType);
        }


        [HttpGet]
        [Route("get-bloodGroupReportingResult")]
        public async Task<ActionResult<List<object>>> GetbloodGroupReportingResult([FromQuery] QueryParams queryParams, int? id)
        {
            var result = await _mediator.Send(new GetBloodGroupReportingResultRequest { QueryParams = queryParams, Id = id });
            return Ok(result);
        }
    }
}
