using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetTakenLeaveReportRequestHandler : IRequestHandler<GetTakenLeaveReportRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepo;
        private readonly IHrmRepository<Hrm.Domain.LeaveRequest> _LeaveRequestRepo;
        private readonly IHrmRepository<Hrm.Domain.LeaveType> _LeaveTypeRepo;
        private readonly IMapper _mapper;

        public GetTakenLeaveReportRequestHandler(IHrmRepository<Domain.Attendance> attendanceRepo, IHrmRepository<Domain.LeaveRequest> leaveRequestRepo,IHrmRepository<Domain.LeaveType> leaveTypeRepo, IMapper mapper)
        {
            _AttendanceRepo = attendanceRepo;
            _LeaveRequestRepo = leaveRequestRepo;
            _LeaveTypeRepo = leaveTypeRepo;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetTakenLeaveReportRequest request, CancellationToken cancellationToken)
        {
            var leavetypes = await _LeaveTypeRepo.Where(x => x.ShowReport == true).Select(x => x.LeaveTypeId).ToListAsync();

            var attendance = _AttendanceRepo.Where(x => x.AttendanceDate >= DateOnly.FromDateTime(request.StartDate) && x.AttendanceDate <= DateOnly.FromDateTime(request.EndDate))
                
                            .Include(x => x.LeaveRequest)
                            .AsQueryable();

            var attendanceEnu = attendance.Where(x => request.EmpId.Contains(x.EmpId)).AsEnumerable();

            var report = request.EmpId.Select(emp => new
            {
                EmpId = emp,
                LeaveTypesCount = leavetypes.ToDictionary(leaveType => leaveType,
            leaveType => attendanceEnu.Count(x => x.LeaveRequest.LeaveTypeId == leaveType && x.EmpId == emp))
            }).ToList();

            return report;
        }
    }
}
