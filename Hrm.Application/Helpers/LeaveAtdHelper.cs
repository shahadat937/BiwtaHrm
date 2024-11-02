using AutoMapper;
using Hrm.Application.Constants;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Enum;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Helpers
{
    public class LeaveAtdHelper
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public int leaveRequestId { get; set; }
        public LeaveAtdHelper(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public List<Hrm.Domain.Attendance> getAttendance()
        {
            if (this.leaveRequestId == 0)
            {
                return new List<Hrm.Domain.Attendance>();
            }

            var attendanceList = _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.LeaveRequestId == this.leaveRequestId).ToList();

            return attendanceList;
        }

        public async Task<bool> deleteAttendance()
        {
            var attendanceList = this.getAttendance();
            foreach (var attendance in attendanceList)
            {
                await _unitOfWork.Repository<Hrm.Domain.Attendance>().Delete(attendance);
            }

            await _unitOfWork.Save();
            return true;
        }

        public async Task<bool> saveAttendance(DateOnly from, DateOnly to, int empId, int leaveTypeId)
        {
            List<CreateAttendanceDto> list = new List<CreateAttendanceDto>();

            bool IsSandwichLeave = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.IsActive == true && x.LeaveTypeId == leaveTypeId && x.RuleName == LeaveRule.SandwichLeave).AnyAsync();

            for (DateOnly curDate = from; curDate <= to; curDate = curDate.AddDays(1))
            {
                int daytypeId = (int)DayTypeOption.Workday;

                daytypeId = IsWorkkDay(curDate) ? daytypeId : (int)DayTypeOption.Weekend; 

                daytypeId = IsHoliday(curDate) ? (int)DayTypeOption.Holiday : daytypeId;

                var attendance =new CreateAttendanceDto
                {
                    EmpId = empId,
                    AttendanceDate = curDate,
                    DayTypeId = daytypeId,
                    AttendanceStatusId = (int)AttendanceStatusOption.OnLeave,
                    LeaveRequestId = leaveRequestId

                };

                attendance.DayTypeId = AttendanceHelper.SetDayTypeId(attendance,_unitOfWork.Repository<Domain.Workday>(),_unitOfWork.Repository<Domain.Holidays>(),_unitOfWork.Repository<Domain.CancelledWeekend>());

                if(attendance.DayTypeId == (int) DayTypeOption.Workday || IsSandwichLeave)
                {
                    list.Add(attendance);
                }
            }

            var attendances = _mapper.Map<List<Hrm.Domain.Attendance>>(list);

            await _unitOfWork.Repository<Hrm.Domain.Attendance>().AddRangeAsync(attendances);
            await _unitOfWork.Save();
            return true;
        }

        public bool IsHoliday(DateOnly GivenDate)
        {
            var IsHoliday = _unitOfWork.Repository<Hrm.Domain.Holidays>().Where(x => x.Year.YearName == GivenDate.Year && x.IsActive == true && x.HolidayDate == GivenDate).Any();
            return IsHoliday;
        }

        public bool IsWorkkDay(DateOnly GivenDate)
        {
            var IsWeekDay = _unitOfWork.Repository<Hrm.Domain.Workday>().Where(x => x.IsActive == true && x.weekDay.WeekDayName == GivenDate.DayOfWeek.ToString() && x.year.YearName == GivenDate.Year).Any();
            return !IsWeekDay;
        }

    }
}
