using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.DTOs.Attendance.Validators;
using Hrm.Application.DTOs.TaskName;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetAttendanceReportByFilterRequestHandler: IRequestHandler<GetAttendanceReportByFilterRequest, object>
    {
        private readonly IHrmRepository<object> _AttendanceRepository;
        private readonly IMapper _mapper;

        public GetAttendanceReportByFilterRequestHandler(IHrmRepository<object> AttendanceRepository, IMapper mapper)
        {
            _AttendanceRepository = AttendanceRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceReportByFilterRequest request, CancellationToken cancellationToken)
        {
            var validator = new AttendanceReportFilterDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AtdReportFilter);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var officeId = request.AtdReportFilter.OfficeId == null ? "NULL" : request.AtdReportFilter.OfficeId.ToString();

            var departmentId = request.AtdReportFilter.DepartmentId == null ? "NULL" : request.AtdReportFilter.DepartmentId.ToString();

            var designationId = request.AtdReportFilter.DesignationId == null ? "NULL" : request.AtdReportFilter.DesignationId.ToString();

            var sectionId = request.AtdReportFilter.SectionId == null ? "NULL" : request.AtdReportFilter.SectionId.ToString();

            var empId = request.AtdReportFilter.EmpId == null ? "NULL" : request.AtdReportFilter.EmpId.ToString();

            string query = $"EXEC [dbo].[GetAttendanceReportRange] @startDate='{request.AtdReportFilter.From}', @endDate='{request.AtdReportFilter.To}', @officeId={officeId}, @departmentId={departmentId}, @designationId={designationId}, @sectionId={sectionId}, @empId={empId}";


            return _AttendanceRepository.ExecWithSqlQuery(query);

        }
    }
}
