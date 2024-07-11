using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.DTOs.Attendance.Validators;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.Enum;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetAttendanceSummaryByOfficeDepartmentSectionRequestHandler:IRequestHandler<GetAttendanceSummaryByOfficeDepartmentSectionRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _EmpJobDetailRepository;
        private readonly IMapper _mapper;
        public GetAttendanceSummaryByOfficeDepartmentSectionRequestHandler(IHrmRepository<Hrm.Domain.Attendance> AttendanceRepository, 
            IHrmRepository<Hrm.Domain.EmpJobDetail> EmpJobDetailRepository, IMapper mapper)
        {
            _AttendanceRepository = AttendanceRepository;
            _EmpJobDetailRepository = EmpJobDetailRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceSummaryByOfficeDepartmentSectionRequest request, CancellationToken cancellationToken)
        {
            var validator = new AttendanceReportFilterDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AtdReportFilterDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }


            // Creating Queryable to filter according to user request
            IQueryable<Hrm.Domain.Attendance> AtdQueryable = _AttendanceRepository.Where(at => at.AttendanceDate >= request.AtdReportFilterDto.From && at.AttendanceDate <= request.AtdReportFilterDto.To && at.AttendanceStatusId != (int)AttendanceStatusOption.Absent)
              .Include(at => at.EmpBasicInfo).AsQueryable();

            IQueryable<Hrm.Domain.EmpJobDetail> JobDetailQueryable = _EmpJobDetailRepository.AsQueryable();


            // Filtering the data according to different fields
            if (request.AtdReportFilterDto.OfficeId.HasValue)
            {
                AtdQueryable = AtdQueryable.Where(at => at.OfficeId == request.AtdReportFilterDto.OfficeId);

                JobDetailQueryable = JobDetailQueryable.Where(jd => jd.OfficeId == request.AtdReportFilterDto.OfficeId);
            }

            if (request.AtdReportFilterDto.DepartmentId.HasValue)
            {
                AtdQueryable = AtdQueryable.Where(at => at.EmpBasicInfo.EmpJobDetail != null && at.EmpBasicInfo.EmpJobDetail.Any() && at.EmpBasicInfo.EmpJobDetail.ToList()[0].DepartmentId == request.AtdReportFilterDto.DepartmentId);

                JobDetailQueryable = JobDetailQueryable.Where(jd => jd.DepartmentId == request.AtdReportFilterDto.DepartmentId);
            }

            if (request.AtdReportFilterDto.SectionId.HasValue)
            {
                AtdQueryable = AtdQueryable.Where(at => at.EmpBasicInfo.EmpJobDetail != null && at.EmpBasicInfo.EmpJobDetail.Any() && at.EmpBasicInfo.EmpJobDetail.ToList()[0].SectionId == request.AtdReportFilterDto.SectionId);

                JobDetailQueryable = JobDetailQueryable.Where(jd => jd.SectionId == request.AtdReportFilterDto.SectionId);
            }

            if (request.AtdReportFilterDto.DesignationId.HasValue)
            {
                AtdQueryable = AtdQueryable.Where(at => at.EmpBasicInfo.EmpJobDetail != null && at.EmpBasicInfo.EmpJobDetail.Any() && at.EmpBasicInfo.EmpJobDetail.ToList()[0].DesignationId == request.AtdReportFilterDto.DesignationId);

                JobDetailQueryable = JobDetailQueryable.Where(jd => jd.DesignationId == request.AtdReportFilterDto.DesignationId);
            }


            // Calculating total number of Employee according to given filter
            int totalEmployee = JobDetailQueryable.Select(jd => jd.EmpId).Distinct().Count();
            


            // Generating intermediate reports
            var reports = AtdQueryable.GroupBy(at => at.AttendanceDate).Select(
                    g => new TotalPresentAbsentEmpDto
                    {
                        Date = g.Key,
                        totalPresentEmp = g.Select(at => at.EmpId).Distinct().Count()
                    }
                )
                .ToList();


            // Generating all dates in a given date range and merging the reports for all dates
            DateTime startDate = new DateTime(request.AtdReportFilterDto.From.Year, request.AtdReportFilterDto.From.Month, request.AtdReportFilterDto.From.Day);

            DateTime endDate = new DateTime(request.AtdReportFilterDto.To.Year, request.AtdReportFilterDto.To.Month, request.AtdReportFilterDto.To.Day);

            var allDates = Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
                .Select(offset => startDate.AddDays(offset))
                .ToList();

            var allReports = allDates.Select(dt => new TotalPresentAbsentEmpDto
            {
                Date = DateOnly.FromDateTime(dt),
                totalPresentEmp = reports.FirstOrDefault(s => s.Date == DateOnly.FromDateTime(dt))?.totalPresentEmp ?? 0
            }).OrderBy(dt=>dt.Date).ToList();


            // Calculating total Absent Employees
            foreach (var item in allReports)
            {
                item.totalAbsentEmp = totalEmployee - item.totalPresentEmp;
            }

            return allReports;
        }
    }
}
