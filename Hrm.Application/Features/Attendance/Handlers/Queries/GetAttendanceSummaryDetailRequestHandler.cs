using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.DTOs.Form;
using Hrm.Application.Enum;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Hrm.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetAttendanceSummaryDetailRequestHandler : IRequestHandler<GetAttendanceSummaryDetailRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHrmRepository<Hrm.Domain.Workday> _WorkdayRepo;
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidaysRepo;
        private readonly IHrmRepository<Hrm.Domain.CancelledWeekend> _CancelledWeekendRepo;
        private readonly IMapper _mapper;

        public GetAttendanceSummaryDetailRequestHandler(IUnitOfWork unitOfWork,
            IHrmRepository<Domain.Workday> workdayRepo,
            IHrmRepository<Domain.Holidays> holidayRepo,
            IHrmRepository<Domain.CancelledWeekend> cancelledWeekendRepo,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _WorkdayRepo = workdayRepo;
            _HolidaysRepo = holidayRepo;
            _CancelledWeekendRepo = cancelledWeekendRepo;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAttendanceSummaryDetailRequest request, CancellationToken cancellationToken)
        {
            var attendance = _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.EmpId == request.EmpId &&
                x.AttendanceDate >= DateOnly.FromDateTime(request.StartDate) &&
                x.AttendanceDate <= DateOnly.FromDateTime(request.EndDate)
            ).Include(x => x.AttendanceStatus).OrderBy(x=>x.AttendanceDate).Include(x => x.Shift);
            var attendanceDto = _mapper.Map<List<AttendanceDto>>(await attendance.ToListAsync());

            List<AttendanceDto> attendanceHolidayDto = new List<AttendanceDto>();
            List<AttendanceDto> result = new List<AttendanceDto>();

            int cursor = 0;
            for(DateTime cur = request.StartDate; cur<=request.EndDate; cur = cur.AddDays(1))
            {
                if (cursor < attendanceDto.Count() && attendanceDto[cursor].AttendanceDate == DateOnly.FromDateTime(cur))
                {
                    if (attendanceDto[cursor].DayTypeId == (int)DayTypeOption.Weekend || attendanceDto[cursor].DayTypeId == (int) DayTypeOption.Holiday)
                    {
                        attendanceDto[cursor].AttendanceStatusName = "HW";
                    }
                    result.Add(attendanceDto[cursor++]);
                } else
                {
                    var dto = new AttendanceDto();
                    dto.EmpId = request.EmpId;
                    dto.AttendanceDate = DateOnly.FromDateTime(cur);
                    bool IsHolidayOrWeekend = AttendanceHelper.IsHoliday(DateOnly.FromDateTime(cur), _HolidaysRepo) | (!AttendanceHelper.IsWeekDay(DateOnly.FromDateTime(cur), _WorkdayRepo, _CancelledWeekendRepo));

                    if(IsHolidayOrWeekend)
                    {
                        dto.AttendanceStatusName = "HW";
                    } else
                    {
                        dto.AttendanceStatusName = "Absent";
                    }
                    result.Add(dto);
                }
            }

            return new { EmployeeInfo = await GetEmpInfo(request.EmpId), Attendance = result };
        }

        public async Task<object> GetEmpInfo(int empId)
        {
            var empBasicInfo = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Get(empId);
            var empJobDetail = await _unitOfWork.Repository<Hrm.Domain.EmpJobDetail>().Where(x => x.EmpId == empId)
                .Include(x => x.Department)
                .Include(x => x.Designation)
                .FirstOrDefaultAsync();
            var empPhoto = await _unitOfWork.Repository<Hrm.Domain.EmpPhotoSign>().Where(x => x.EmpId == empId).FirstOrDefaultAsync();
            string empName = empBasicInfo.FirstName + " " + empBasicInfo.LastName;
            string pmis = empBasicInfo.IdCardNo;
            string department = "";
            string designation = "";
            string photoUrl = "";
            if(empJobDetail!=null)
            {
                department = empJobDetail.Department != null && empJobDetail.Department.DepartmentName!=null? empJobDetail.Department.DepartmentName : department;
            }
            
            if(empPhoto!=null&&empPhoto.PhotoUrl!=null&&empPhoto.PhotoUrl!="")
            {
                photoUrl = empPhoto.PhotoUrl;
            }

            var employeeInfo = new
            {
                EmpName = empName,
                PMIS = pmis,
                Department = department,
                Designation = designation,
                PhotoUrl = photoUrl
            };

            return employeeInfo;

        }
    }
}
