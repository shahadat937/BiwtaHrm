﻿using Hrm.Application;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.DTOs.Attendance.Validators;
using Hrm.Application.Features.Attendance.Requests.Commands;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Attendance)]
    [ApiController]
    [Authorize]
    public class AttendanceController:Controller
    {
        private readonly IMediator _mediator;
        
        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Attendance")]
        public async Task<ActionResult> Get([FromQuery] AttendanceFilterDto filters)
        {
            var command = new GetAttendanceRequest { Filters = filters};
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-AttendanceSummary")]
        public async Task<ActionResult> GetAttendanceSummary([FromQuery] GetAttendanceSummaryDto GetAttendanceSummarydto)
        {
            var command = new GetAttendanceSummaryByEmpRequest { AtdSummaryDto = GetAttendanceSummarydto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-AttendanceSummaryDetail")]
        public async Task<ActionResult> GetAttendanceSummaryDetail([FromQuery] int EmpId, [FromQuery] DateTime StartDate, [FromQuery] DateTime EndDate)
        {
            var command = new GetAttendanceSummaryDetailRequest { EmpId = EmpId, StartDate = StartDate, EndDate = EndDate };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-AttendanceReportByFilter")]
        public async Task<ActionResult> GetAttendanceReportByFilter([FromQuery] AttendanceReportFilterDto AttendanceReportFilterdto)
        {
            var command = new GetAttendanceReportByFilterRequest { AtdReportFilter = AttendanceReportFilterdto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-AttendanceById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var command = new GetAttendanceByIdRequest { AttendanceId = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-TotalPresentAbsentEmp")]
        public async Task<ActionResult> GetTotalPresentAbsentEmp([FromQuery] AttendanceReportFilterDto AtdReportFilterDto)
        {
            var command = new GetAttendanceSummaryByOfficeDepartmentSectionRequest { AtdReportFilterDto = AtdReportFilterDto };
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet]
        [Route("get-WorkingDays")]
        public async Task<ActionResult> GetWorkingDays([FromQuery] DateTime From, [FromQuery] DateTime To, [FromQuery] int? LeaveTypeId)
        {
            var commnad = new GetWorkingDaysRequest { From = From, To = To, LeaveTypeId = LeaveTypeId };
            var response = await _mediator.Send(commnad);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-IsHolidayWeekend")]
        public async Task<ActionResult> GetIsHolidayWeekend([FromQuery] int Month, [FromQuery] int Year)
        {
            var command = new GetIsDateHolidayWeekendRequest { Year = Year, Month = Month };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("save-AttendanceFromDevice")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateAttendanceDto attendance)
        {
            
            var command = new CreateAttendanceFromDeviceCommand { Attendancedto = attendance };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("save-ManualAttendance")]
        public async Task<ActionResult<BaseCommandResponse>> ManualAttendance([FromBody] CreateAttendanceDto attendance)
        {
            var command = new CreateManualAttendanceCommand { Attendancedto = attendance };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-AttendanceById")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateAttendanceById([FromBody] CreateAttendanceDto attendance)
        {
            var command = new UpdateAttendanceByIdCommand { Attendancedto = attendance };
            var response = await _mediator.Send(command);
            return Ok(response);

        }


        [HttpGet]
        [Route("update-AttendanceQuery")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateAttendanceQuery([FromQuery] DateOnly fromDate, DateOnly toDate)
        {
            var command = new UpdateAttendanceQueryRequest { FromDate = fromDate, ToDate = toDate };
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("save-BulkAttendance")]
        public async Task<ActionResult<BaseCommandResponse>> SaveBulkAttendance([FromForm] CreateBulkAttendanceDto createAtdDto)
        {
            var command = new CreateBulkAttendanceCommand { csvFile = createAtdDto.csvFile };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("delete-AttendanceById/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> delete(int id)
        {
            var command = new DeleteAttendanceByIdRequest { AttendanceId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
