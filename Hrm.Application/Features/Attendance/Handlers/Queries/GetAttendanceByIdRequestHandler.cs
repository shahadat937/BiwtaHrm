using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetAttendanceByIdRequestHandler: IRequestHandler<GetAttendanceByIdRequest, AttendanceDto>
    {
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;
        private readonly IMapper _mapper;

        public GetAttendanceByIdRequestHandler(IHrmRepository<Hrm.Domain.Attendance> AttendanceRepository, IMapper mapper)
        {
            _AttendanceRepository = AttendanceRepository;
            _mapper = mapper;
        }

        public async Task<AttendanceDto> Handle(GetAttendanceByIdRequest request, CancellationToken cancellationToken)
        {
            var Attendance = await _AttendanceRepository.Where(x => x.AttendanceId == request.AttendanceId)
                 .Include(at => at.AttendanceType)
                .Include(at => at.EmpBasicInfo)
                .Include(at => at.Office)
                .Include(at => at.OfficeBranch)
                .Include(at => at.ShiftType)
                .Include(at => at.ShiftType)
                    .ThenInclude(ss => ss.ShiftSetting)
                .Include(at => at.DayType)
                .Include(at => at.AttendanceStatus)
                .FirstOrDefaultAsync();

            if (Attendance == null)
            {
                throw new NotFoundException(nameof(Attendance), request.AttendanceId);
            }

            //Attendance = Attendance.Where(at => at.AttendanceId == request.AttendanceId);

            var attendancedto = _mapper.Map<AttendanceDto>(Attendance);

            return attendancedto;


        }

    }
}
