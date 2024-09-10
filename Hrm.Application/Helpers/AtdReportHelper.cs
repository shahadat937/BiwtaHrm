using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Helpers
{
    public class AtdReportHelper
    {
        /* public static int totalWorkingDay(DateOnly from, DateOnly to, IHrmRepository<Hrm.Domain.Holidays> _HolidaysRepository)
         {
             DateTime datetimeFrom = new DateTime(from.Year, from.Month, from.Day);
             DateTime datetimeTo = new DateTime(to.Year, to.Month, to.Day);

             TimeSpan timespan = datetimeTo.Subtract(datetimeFrom);
             int totalDays = (int)timespan.TotalDays+1;
             int holidays = _HolidaysRepository.Where(hd => hd.IsActive && hd.HolidayDate >= from && hd.HolidayDate <= to).Count();

             return (totalDays - holidays);
         } */

        public static async Task<int> calculateWorkingDay(DateOnly startDateOnly, DateOnly endDateOnly, int curYear, IUnitOfWork _unitOfWork)
        {

            var startDate = new DateTime(startDateOnly.Year, startDateOnly.Month, startDateOnly.Day);
            var endDate = new DateTime(endDateOnly.Year, endDateOnly.Month, endDateOnly.Day);

            Dictionary<string, int> weekends = new Dictionary<string, int>
            {
                {"Saturday", 6 },
                {"Sunday", 0 },
                {"Monday", 1 },
                {"Tuesday", 2 },
                {"Wednesday", 3 },
                {"Thursday", 4 },
                {"Friday", 5 }
            };

            var workDays = await _unitOfWork.Repository<Hrm.Domain.Workday>().Where(x => x.IsActive == true && x.year.YearName == curYear).Select(x => x.weekDay.WeekDayName).ToListAsync();

            foreach (var workday in workDays)
            {
                weekends.Remove(workday);
            }

            int totalWeekend = 0;
            int totalWorkday = (int)endDate.Subtract(startDate).TotalDays + 1;

            foreach (var weekend in weekends)
            {
                DateTime tempStartDate = startDate;
                DateTime tempEndDate = endDate;

                int extradays = (weekend.Value - (int)tempStartDate.DayOfWeek + 7) % 7;
                int excludedays = ((int)tempEndDate.DayOfWeek - weekend.Value + 7) % 7;

                tempStartDate = tempStartDate.AddDays(extradays);
                tempEndDate = tempEndDate.AddDays(-excludedays);

                if (tempStartDate <= tempEndDate)
                {
                    TimeSpan totaldays = tempEndDate.Subtract(tempStartDate);
                    totalWeekend += ((int)totaldays.TotalDays + 7) / 7;
                }
            }

            int holidays = await _unitOfWork.Repository<Hrm.Domain.Holidays>().Where(x => x.HolidayDate >= DateOnly.FromDateTime(startDate) && x.HolidayDate <= DateOnly.FromDateTime(endDate) && x.IsActive && x.IsWeekend == false).CountAsync();

            int cancelledWeekend = await _unitOfWork.Repository<Hrm.Domain.CancelledWeekend>().Where(x => x.CancelDate >= startDate && x.CancelDate <= endDate && x.IsActive == true).CountAsync();

            return totalWorkday - totalWeekend - holidays + cancelledWeekend;

        }

        public static async Task<int> totalAbsent(DateOnly from, DateOnly to, int EmpId, 
            IHrmRepository<Domain.Attendance> _AttendanceRepository,
            IHrmRepository<Domain.Holidays> _HolidaysRepository, IUnitOfWork unitOfWork)
        {

            int totalAbsent = 0;
            int totalWorkingdays = await calculateWorkingDay(from, to, from.Year, unitOfWork);
            int totalEntry = _AttendanceRepository.Where(at => at.AttendanceDate >= from && at.AttendanceDate <= to && at.EmpId == EmpId && at.AttendanceStatusId !=(int) AttendanceStatusOption.Absent).Count();
            totalAbsent = totalWorkingdays - totalEntry;

            return totalAbsent;
        }

        public static int totalLate(DateOnly from, DateOnly to, int EmpId, 
            IHrmRepository<Domain.Attendance> _AttendanceRepository)
        {
            int totalLate = _AttendanceRepository.Where(at => at.AttendanceDate >= from && at.AttendanceDate <= to && at.EmpId == EmpId && at.AttendanceStatusId == (int)AttendanceStatusOption.Late).Count();
            return totalLate;
        }

        public static int? totalOverTime(DateOnly from, DateOnly to, int EmpId,
            IHrmRepository<Domain.Attendance> _AttendanceRepository)
        {
            int? overtimeSum = _AttendanceRepository.Where(at => at.AttendanceDate >= from && at.AttendanceDate <= to && at.EmpId == EmpId).Sum(at => at.OverTime);
            return overtimeSum;
        }

        public static int? totalWorkingHour(DateOnly from, DateOnly to, int EmpId,
            IHrmRepository<Domain.Attendance> _AttendanceRepository)
        {
            int? workingHour = _AttendanceRepository.Where(at => at.AttendanceDate >= from && at.AttendanceDate <= to && at.EmpId == EmpId).Sum(at => at.WorkHour);
            return workingHour;
        }

        public static int totalSiteVisit(DateOnly from, DateOnly to, int EmpId,
            IHrmRepository<Domain.Attendance> _AttendanceRepository)
        {
            int totalSiteVisit = _AttendanceRepository.Where(at => at.AttendanceDate >= from && at.AttendanceDate <= to && at.EmpId == EmpId && at.AttendanceStatusId == (int) AttendanceStatusOption.OnSiteVisit).Count();

            return totalSiteVisit;
        }

        public static async Task<int> totalOnLeave(DateOnly from, DateOnly to, int EmpId, IHrmRepository<Domain.Attendance> _AttendanceRepository)
        {
            int totalOnLeave = await _AttendanceRepository.Where(at => at.AttendanceDate >= from && at.AttendanceDate <= to && at.EmpId == EmpId && at.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave).CountAsync();

            return totalOnLeave;
        }

    }
}
