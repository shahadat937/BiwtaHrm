using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.DTOs.TaskName;
using Hrm.Application.Features.Attendance.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetAttendanceReportByFilterRequestHandler: IRequestHandler<GetAttendanceReportByFilterRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;
        private readonly IMapper _mapper;

        public GetAttendanceReportByFilterRequestHandler(IHrmRepository<Hrm.Domain.Attendance> AttendanceRepository, IMapper mapper)
        {
            _AttendanceRepository = AttendanceRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceReportByFilterRequest request, CancellationToken cancellationToken)
        {
            var Attendances = _AttendanceRepository.Where(at => at.AttendanceDate >= request.AtdReportFilter.From && at.AttendanceDate <= request.AtdReportFilter.To)
              .Include(at => at.EmpBasicInfo)
              .Include(at => at.AttendanceStatus)
              .AsQueryable();

            if(request.AtdReportFilter.EmpId.HasValue)
            {
                Attendances = Attendances.Where(at => at.EmpId == request.AtdReportFilter.EmpId);
            }

            if(request.AtdReportFilter.OfficeId.HasValue)
            {
                Attendances = Attendances.Where(at => at.EmpBasicInfo.EmpJobDetail.Any() && at.EmpBasicInfo.EmpJobDetail.ToList()[0].OfficeId == request.AtdReportFilter.OfficeId);
            }

            if(request.AtdReportFilter.DepartmentId.HasValue)
            {
                Attendances = Attendances.Where(at => at.EmpBasicInfo.EmpJobDetail.Any() && at.EmpBasicInfo.EmpJobDetail.ToList()[0].DepartmentId == request.AtdReportFilter.DepartmentId);
            }

            if(request.AtdReportFilter.SectionId.HasValue)
            {
                Attendances = Attendances.Where(at => at.EmpBasicInfo.EmpJobDetail.Any() && at.EmpBasicInfo.EmpJobDetail.ToList()[0].SectionId == request.AtdReportFilter.SectionId);
            }

            var AttendancesList = Attendances.OrderBy(at => at.EmpId).ThenBy(at => at.AttendanceDate).ToList();

            List<AttendanceReportDto> AttendanceReportdtos = new List<AttendanceReportDto>();

            int currentEmpId = -1;

            foreach (var attendance in AttendancesList)
            {
                if(currentEmpId != attendance.EmpId)
                {
                    AttendanceReportDto dto = new AttendanceReportDto();
                    dto.EmpId = attendance.EmpId;
                    dto.EmpFirstName = attendance.EmpBasicInfo.FirstName;
                    dto.EmpLastName = attendance.EmpBasicInfo.LastName;
                    AttendanceReportdtos.Add(dto);

                    currentEmpId = attendance.EmpId;
                }

                PresentDateDto presentDatedto = new PresentDateDto();
                presentDatedto.AttendanceDate = attendance.AttendanceDate;
                presentDatedto.AttendanceStatus = attendance.AttendanceStatus.AttendanceStatusName;
                AttendanceReportdtos[AttendanceReportdtos.Count - 1].PresentDates.Add(presentDatedto);
            }

            return AttendanceReportdtos;

        }
    }
}
