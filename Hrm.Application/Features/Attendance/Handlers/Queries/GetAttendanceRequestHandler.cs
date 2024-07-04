using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
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
    public class GetAttendanceRequestHandler: IRequestHandler<GetAttendanceRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;
        private readonly IMapper _mapper;

        public GetAttendanceRequestHandler(IHrmRepository<Hrm.Domain.Attendance> attendanceRepository, IMapper mapper)
        {
            _AttendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceRequest request, CancellationToken cancellationToken)
        {
            var attendances = _AttendanceRepository.Where(at => true)
                .Include(at => at.AttendanceType)
                .Include(at => at.EmpBasicInfo)
                .Include(at => at.Office)
                .Include(at => at.OfficeBranch)
                .Include(at => at.Shift)
                .Include(at => at.DayType)
                .Include(at => at.AttendanceStatus)
                .ToList();

            var AttendancesDtos =  _mapper.Map<List<AttendanceDto>>(attendances);

            return AttendancesDtos;
        }
    }
}
