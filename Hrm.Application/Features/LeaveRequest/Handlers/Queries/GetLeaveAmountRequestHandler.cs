using Hrm.Application.Constants;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TaskName;
using Hrm.Application.Enum;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using Hrm.Application.Helpers;
using Hrm.Application.Exceptions;
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
        private readonly IHrmRepository<Hrm.Domain.Attendance> _AttendanceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LeaveAtdHelper leaveAtdHelper;

        public GetLeaveAmountRequestHandler(IHrmRepository<Domain.LeaveRequest> leaveRequestRepository, IHrmRepository<Domain.LeaveRules> leaveRulesRepository,
            IHrmRepository<Domain.Attendance> AttendanceRepository,
            IUnitOfWork unitOfWork)
        {
            _LeaveRequestRepository = leaveRequestRepository;
            _LeaveRulesRepository = leaveRulesRepository;
            _AttendanceRepository = AttendanceRepository;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<object> Handle(GetLeaveAmountRequest request, CancellationToken cancellationToken)
        {
            var leaveRules = await _LeaveRulesRepository.Where(x => x.LeaveTypeId == request.leaveAmountRequestDto.LeaveTypeId).ToListAsync();

            DateOnly? joiningDate = null;

            if(_unitOfWork.Repository<Domain.EmpJobDetail>().Where(x=>x.EmpId == request.leaveAmountRequestDto.EmpId).Any())
            {
                var temp = await _unitOfWork.Repository<Domain.EmpJobDetail>().Where(x => x.EmpId == request.leaveAmountRequestDto.EmpId).Select(x => x.JoiningDate).ToListAsync();

                joiningDate = temp[0];
            }

            int totalLeave = -1;
            int totalDue = 0;
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int accuralTotalLeave = -1;
            

            if(leaveRules.Where(x=>x.RuleName == LeaveRule.AccrualRate).Any() && leaveRules.Where(x=>x.RuleName == LeaveRule.AccrualFrequency).Any())
            {
                if(joiningDate == null)
                {
                    throw new BadRequestException("Joining Date was not found in Employee Job Detail");
                }

                DateTime joiningDateDt = new DateTime(joiningDate.Value.Year, joiningDate.Value.Month, joiningDate.Value.Day);

                int accuralRate = leaveRules.Where(x => x.RuleName == LeaveRule.AccrualRate).Select(x => x.RuleValue).ToList()[0];

                int accuralFreq = leaveRules.Where(x=>x.RuleName == LeaveRule.AccrualFrequency).Select(x=>x.RuleValue).ToList()[0];

                int totalWorkingDays = await AttendanceHelper.calculateWorkingDay(joiningDateDt, DateTime.Now, currentYear, _unitOfWork);

                accuralTotalLeave = (totalWorkingDays / accuralFreq) * accuralRate;

                var totalLeaveTaken = _AttendanceRepository.Where(x => x.AttendanceStatus.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.EmpId == request.leaveAmountRequestDto.EmpId && x.DayTypeId == (int)DayTypeOption.Workday && x.LeaveRequest.LeaveTypeId == request.leaveAmountRequestDto.LeaveTypeId).Count();

                totalLeave = accuralTotalLeave;
                totalDue = accuralTotalLeave - totalLeaveTaken;

            }
            

            if(leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysLifetime).Any())
            {
                totalLeave = leaveRules.Where(x => x.RuleName == LeaveRule.MaxDaysLifetime).ToList()[0].RuleValue;

                var totalLeaveTaken = _AttendanceRepository.Where(x => x.AttendanceStatus.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.EmpId == request.leaveAmountRequestDto.EmpId && x.DayTypeId == (int)DayTypeOption.Workday && x.LeaveRequest.LeaveTypeId == request.leaveAmountRequestDto.LeaveTypeId).Count();

                if (accuralTotalLeave != -1)
                    totalLeave = Math.Min(totalLeave, accuralTotalLeave);
                
                totalDue = totalLeave - totalLeaveTaken;

            } else if(leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerYear).Any())
            {
                totalLeave = leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerYear).ToList()[0].RuleValue;

                var totalLeaveTaken = _AttendanceRepository.Where(x => x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.AttendanceDate.Year == currentYear && x.DayTypeId == (int)DayTypeOption.Workday && x.LeaveRequest.LeaveTypeId == request.leaveAmountRequestDto.LeaveTypeId).Count();

                totalDue = totalLeave - totalLeaveTaken;
            } else if(leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerMonth).Any())
            {
                totalLeave = leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerMonth).ToList()[0].RuleValue;

                var totalLeaveTaken = _AttendanceRepository.Where(x => x.AttendanceDate.Month == currentMonth && x.AttendanceDate.Year == currentYear && x.DayTypeId == (int)DayTypeOption.Workday && x.LeaveRequest.LeaveTypeId == request.leaveAmountRequestDto.LeaveTypeId).Count();

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
