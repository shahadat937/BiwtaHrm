using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Enum;
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
        public static int totalWorkingDay(DateOnly from, DateOnly to, IHrmRepository<Hrm.Domain.Holidays> _HolidaysRepository)
        {
            DateTime datetimeFrom = new DateTime(from.Year, from.Month, from.Day);
            DateTime datetimeTo = new DateTime(to.Year, to.Month, to.Day);

            TimeSpan timespan = datetimeTo.Subtract(datetimeFrom);
            int totalDays = (int)timespan.TotalDays;
            int holidays = _HolidaysRepository.Where(hd => hd.IsActive && hd.HolidayDate >= from && hd.HolidayDate <= to).Count();

            return (totalDays - holidays);
        }

        public static int totalAbsent(DateOnly from, DateOnly to, int EmpId, 
            IHrmRepository<Domain.Attendance> _AttendanceRepository,
            IHrmRepository<Domain.Holidays> _HolidaysRepository)
        {
            int totalAbsent = 0;
            int totalWorkingdays = totalWorkingDay(from, to, _HolidaysRepository);
            int totalEntry = _AttendanceRepository.Where(at => at.AttendanceDate >= from && at.AttendanceDate <= to && at.EmpId == EmpId).Count();
            totalAbsent = totalWorkingdays - totalEntry;

            return totalAbsent;
        }

        public static int totalLate(DateOnly from, DateOnly to, int EmpId, 
            IHrmRepository<Domain.Attendance> _AttendanceRepository)
        {
            int totalLate = _AttendanceRepository.Where(at => at.AttendanceDate >= from && at.AttendanceDate <= to && at.EmpId == EmpId && at.AttendanceStatusId == (int)AttendanceStatusOption.Late).Count();
            return totalLate;
        }

    }
}
