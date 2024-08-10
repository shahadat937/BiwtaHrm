using Hrm.Application.Constants;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TaskName;
using Hrm.Application.Enum;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetLeaveAmountRequestHandler: IRequestHandler<GetLeaveAmountRequest, object>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveRequest> _LeaveRequestRepository;
        private readonly IHrmRepository<Hrm.Domain.LeaveRules> _LeaveRulesRepository;
        private readonly IHrmRepository<Hrm.Domain.Workday> _WorkdayRepository;
        private readonly IHrmRepository<Hrm.Domain.Year> _YearRepository;
        private readonly IHrmRepository<Hrm.Domain.Holidays> _HolidaysRepository;
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;

        public GetLeaveAmountRequestHandler(IHrmRepository<Domain.LeaveRequest> leaveRequestRepository, IHrmRepository<Domain.LeaveRules> leaveRulesRepository,
            IHrmRepository<Domain.Attendance> AttendanceRepository)
        {
            _LeaveRequestRepository = leaveRequestRepository;
            _LeaveRulesRepository = leaveRulesRepository;
            _AttendanceRepository = AttendanceRepository;
        }

        public async Task<object> Handle(GetLeaveAmountRequest request, CancellationToken cancellationToken)
        {
            var leaveRules = await _LeaveRulesRepository.Where(x => x.LeaveTypeId == request.leaveAmountRequestDto.LeaveTypeId).ToListAsync();

            int totalLeave = -1;
            int totalDue = 0;
            int currentYear = (new DateTime()).Year;
            int currentMonth = (new DateTime()).Month;

            if(leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysLifetime).Any())
            {
                totalLeave = leaveRules.Where(x => x.RuleName == LeaveRule.MaxDaysLifetime).ToList()[0].RuleValue;

                var totalLeaveTaken = _AttendanceRepository.Where(x => x.AttendanceStatus.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.EmpId == request.leaveAmountRequestDto.EmpId && x.DayTypeId == (int)DayTypeOption.Workday).Count();

                totalDue = totalLeave - totalLeaveTaken;
            } else if(leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerYear).Any())
            {
                totalLeave = leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerYear).ToList()[0].RuleValue;

                var totalLeaveTaken = _AttendanceRepository.Where(x => x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.AttendanceDate.Year == currentYear && x.DayTypeId == (int)DayTypeOption.Workday).Count();

                totalDue = totalLeave - totalLeaveTaken;
            } else if(leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerMonth).Any())
            {
                totalLeave = leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerMonth).ToList()[0].RuleValue;

                var totalLeaveTaken = _AttendanceRepository.Where(x => x.AttendanceDate.Month == currentMonth && x.AttendanceDate.Year == currentYear && x.DayTypeId == (int)DayTypeOption.Workday).Count();

                totalDue = totalLeave - totalLeaveTaken;
            }

            if(totalLeave==-1)
            {
                totalDue = -1;
            }
            return new {totalLeave = totalLeave, totalDue = totalDue};
            
        }

        /*public List<DateTime> generateDate(DateTime startDate, DateTime endDate)
        {
            List<DateTime> result = new List<DateTime>();

            for(DateTime start = startDate; start <= endDate;start = startDate.AddDays(1))
            {
                result.Add(start);
            }

            return result;
        }*/

        /*public List<DateTime> filterDates(DateTime startDate, DateTime endDate)
        {

            var dates = generateDate(startDate, endDate);

            var listQuerable = dates.AsQueryable();
            DateTime currentDates = new DateTime();
            var workdays = _WorkdayRepository.Where(x => x.year.YearName == currentDates.Year);

            List<string> days = ["Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];

            foreach(var workday in  workdays)
            {
                days.Remove(workday.weekDay.WeekDayName);
            }

            foreach(var day in days)
            {
                listQuerable = listQuerable.Where(x => x.DayOfWeek.ToString() != day);
            }

            var holidaysDate = _HolidaysRepository.Where(x => x.Year.YearName == currentDates.Year).Select(x => new DateTime()).ToList();


            return listQuerable.ToList();
        } */
    }
}
