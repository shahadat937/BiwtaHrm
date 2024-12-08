using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Features.Attendance.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetAttendanceSummaryDetailRequestHandler : IRequestHandler<GetAttendanceSummaryDetailRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceSummaryDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceSummaryDetailRequest request, CancellationToken cancellationToken)
        {
            var attendance = _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.EmpId == request.EmpId &&
                x.AttendanceDate >= DateOnly.FromDateTime(request.StartDate) &&
                x.AttendanceDate <= DateOnly.FromDateTime(request.EndDate)
            ).Include(x => x.AttendanceStatus).OrderBy(x=>x.AttendanceDate);
            var attendanceDto = _mapper.Map<List<AttendanceDto>>(await attendance.ToListAsync());

            List<AttendanceDto> attendanceHolidayDto = new List<AttendanceDto>();
            for(DateTime cur = request.StartDate; cur<=request.EndDate; cur = cur.AddDays(1))
            {

            }

            return "";
        }
    }
}
