using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Enum;
using Hrm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Helpers
{
    public class AttendanceHelper
    {
        public static int? SetAttendanceStatus(CreateAttendanceDto dto, IHrmRepository<Hrm.Domain.Shift> _ShiftRepository)
        {
            if (dto.ShiftId == null || dto.InTime == null)
            {
                return null;
            }

            var shift =  _ShiftRepository.Get((int)dto.ShiftId).Result;

            if (shift==null)
            {
                return null;
            }


            if (dto.InTime > shift.AbsentTime)
            {
                return (int)AttendanceStatusOption.Absent;
            }

            if (dto.InTime > shift.BufferTime)
            {
                return (int)AttendanceStatusOption.Late;
            }

            return (int)AttendanceStatusOption.Present;
        }

        public static bool IsHoliday(DateOnly GivenDate, IHrmRepository<Hrm.Domain.Holidays> _HolidayRepository)
        {
            var IsHoliday = _HolidayRepository.Where(x => x.Year.YearName == GivenDate.Year && x.IsActive == true && x.HolidayDate == GivenDate).Any();
            return IsHoliday;
        }

        public static bool IsWeekDay(DateOnly GivenDate, IHrmRepository<Hrm.Domain.Workday> _WorkdayRepository)
        {
            var IsWeekDay = _WorkdayRepository.Where(x => x.weekDay.WeekDayName == GivenDate.DayOfWeek.ToString()).Any();
            return IsWeekDay;
        }

        public static int? SetWorkHour(CreateAttendanceDto dto)
        {
            if((!dto.InTime.HasValue)||(!dto.OutTime.HasValue))
            {
                return null;
            }

            TimeOnly time1 = (TimeOnly)dto.InTime;
            TimeOnly time2 = (TimeOnly)dto.OutTime;

            DateTime dateTime1 = DateTime.Today.AddHours(time1.Hour).AddMinutes(time1.Minute);
            DateTime datetime2 = DateTime.Today.AddHours(time2.Hour).AddMinutes(time2.Minute);

            TimeSpan timespan = datetime2 - dateTime1;

            int difference = (int)timespan.TotalMinutes;

            return difference;


        }

        public static int SetDayTypeId(CreateAttendanceDto dto, IHrmRepository<Workday> _WorkdayRepo, IHrmRepository<Holidays> _HolidaysRepo)
        {
            bool weekday = IsWeekDay(dto.AttendanceDate, _WorkdayRepo);
            bool holiday = IsHoliday(dto.AttendanceDate, _HolidaysRepo);

            int daytype = (int)DayTypeOption.Workday;

            if(holiday)
            {
                daytype = (int) DayTypeOption.Holiday;
            }

            if(!weekday)
            {
                daytype = (int)DayTypeOption.Weekend;
            }


            return daytype;
        }

        public static int? SetOverTime(CreateAttendanceDto dto,IHrmRepository<Hrm.Domain.Shift> _ShiftRepository)
        {
            if((!dto.ShiftId.HasValue)||(!dto.OutTime.HasValue))
            {
                return null;
            }

            var shift = _ShiftRepository.Get((int)dto.ShiftId).Result;

            if(shift==null)
            {
                return null;
            }

            if(shift.EndTime>=dto.OutTime)
            {
                return 0;
            }

            TimeOnly time1 = (TimeOnly)shift.EndTime;
            TimeOnly time2 = (TimeOnly)dto.OutTime;

            DateTime datetime1 = DateTime.Today.AddHours(time1.Hour).AddMinutes(time1.Minute);
            DateTime datetime2 = DateTime.Today.AddHours(time2.Hour).AddMinutes(time2.Minute);

            TimeSpan timespan = datetime2 - datetime1;

            return (int)timespan.TotalMinutes;
        }
    }
}
