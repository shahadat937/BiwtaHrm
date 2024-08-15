using Hrm.Application.Constants;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using Hrm.Domain;
using Hrm.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Enum;

namespace Hrm.Application.Helpers
{
    public class LeaveRequestValidator
    {
        private readonly IUnitOfWork _unitOfWork;
        public LeaveRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Validate(DateTime startDate, DateTime endDate, int empId, int leaveTypeId)
        {
            var leaveRules = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == leaveTypeId).ToListAsync();

            if(leaveRules.Count<=0)
            {
                return true;
            }

            var totalLeaveDays =await AttendanceHelper.calculateWorkingDay(startDate, endDate, startDate.Year, _unitOfWork);

            int leaveDue = await this.CalculateLeaveAmount(empId, leaveTypeId, startDate, endDate, startDate.Year);


            return (bool)(totalLeaveDays <= leaveDue);

            
        }


        public async Task<int> CalculateLeaveAmount(int empId, int LeaveTypeId, DateTime startDate, DateTime endYear, int curYear)
        {
            var leaveRules = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == LeaveTypeId).ToListAsync();


            
            bool haveAccuralRule = false;
            bool haveMaxDaysPerMonth = false;
            bool haveMaxDaysPerYear = false;
            bool haveMaxDaysLifeTime = false;

            int maxDaysPerMonth = 0;
            int maxDaysPerYear = 0;
            int maxDaysLifeTime = 0;
            int accuralLeave = 0;
            
            int totalWorkingDayLIfetime = 0;

            if (leaveRules.Where(x=> x.RuleName == LeaveRule.AccrualRate).Any()&& leaveRules.Where(x=>x.RuleName == LeaveRule.AccrualFrequency).Any())
            {
                haveAccuralRule = true;
                var empJobDetail = await _unitOfWork.Repository<Hrm.Domain.EmpJobDetail>().FindOneAsync(x=>x.EmpId == empId);

                int accuralRate = leaveRules.Where(x => x.RuleName == LeaveRule.AccrualRate).Select(x => x.RuleValue).ToList()[0];

                int accuralFreq = leaveRules.Where(x => x.RuleName == LeaveRule.AccrualFrequency).Select(x => x.RuleValue).ToList()[0];

                if (empJobDetail == null || empJobDetail.JoiningDate==null)
                {
                    throw new BadRequestException("Joining Date is not found for the employee");
                }

                DateTime joiningDate = new DateTime(empJobDetail.JoiningDate.Value.Year, empJobDetail.JoiningDate.Value.Month,empJobDetail.JoiningDate.Value.Day);

                totalWorkingDayLIfetime = await AttendanceHelper.calculateWorkingDay(joiningDate, endYear, curYear, _unitOfWork);

                accuralLeave = (totalWorkingDayLIfetime / accuralFreq) * accuralRate;
            }

            if (leaveRules.Where(x => x.RuleName == LeaveRule.MaxDaysPerMonth).Any())
            {
                haveMaxDaysPerMonth = true;
                maxDaysPerMonth = leaveRules.Where(x => x.RuleName == LeaveRule.MaxDaysPerMonth).Select(x => x.RuleValue).ToList()[0];
            }

            if(leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysPerYear).Any())
            {
                haveMaxDaysPerYear = true;
                maxDaysPerMonth = leaveRules.Where(x=> x.RuleName == LeaveRule.MaxDaysPerYear).Select(x=>x.RuleValue).ToList()[0];
            }

            if(leaveRules.Where(x=>x.RuleName == LeaveRule.MaxDaysLifetime).Any())
            {
                haveMaxDaysLifeTime = true;
                maxDaysLifeTime = leaveRules.Where(x => x.RuleName == LeaveRule.MaxDaysLifetime).Select(x => x.RuleValue).ToList()[0];
            }


            if (haveMaxDaysPerMonth)
            {
                int takenLeave = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveTypeId == LeaveTypeId && x.AttendanceDate.Month == startDate.Month && x.AttendanceDate.Year == startDate.Year).CountAsync();

                int totalLeave = maxDaysPerMonth;

                if(haveAccuralRule)
                {
                    totalLeave = Math.Min(totalLeave, accuralLeave);

                    return Math.Max(0, totalLeave);
                }
                
                if(haveMaxDaysPerYear)
                {
                    totalLeave = Math.Min(totalLeave, maxDaysPerYear);
                }

                if(haveMaxDaysLifeTime)
                {
                    totalLeave = Math.Min(totalLeave, maxDaysLifeTime);
                }

                return Math.Max(0, totalLeave - takenLeave);

            }

            if(haveMaxDaysPerYear)
            {
                int takenLeave = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveTypeId == LeaveTypeId && x.AttendanceDate.Year == startDate.Year).CountAsync();

                int totalLeave = maxDaysPerYear;

                if(haveAccuralRule)
                {
                    totalLeave = Math.Min(totalLeave, accuralLeave);
                }

                if(haveMaxDaysLifeTime)
                {
                    totalLeave = Math.Min(totalLeave, maxDaysLifeTime);
                }

                return Math.Max(0, takenLeave - totalLeave);
            }

            if(haveMaxDaysLifeTime)
            {
                int takenLeave = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveTypeId == LeaveTypeId).CountAsync();
                
                int totalLeave = maxDaysLifeTime;

                if(haveAccuralRule)
                {
                    Math.Min(totalLeave, accuralLeave);
                }

                return Math.Max(0, totalLeave - takenLeave);
            }


            return -1;            

        }
    }
}
