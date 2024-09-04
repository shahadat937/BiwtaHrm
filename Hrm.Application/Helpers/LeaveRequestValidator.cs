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

            bool haveMinAge = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == leaveTypeId && x.RuleName == LeaveRule.MinimumAge).AnyAsync();

            await IsExceedMaxRequest(empId, leaveTypeId);
            await IsCorrectGender(empId, leaveTypeId);
            await HaveMinimumAge(empId, leaveTypeId);

            var totalLeaveDays =await AttendanceHelper.calculateWorkingDay(startDate, endDate, startDate.Year, _unitOfWork);

            int leaveDue = await this.CalculateLeaveAmount(empId, leaveTypeId, startDate, endDate, startDate.Year);

            if (leaveDue == -1)
            {
                return true;
            }

            return (bool)(totalLeaveDays <= leaveDue);

            
        }

        public async Task<bool> IsCorrectGender(int empId, int leaveTypeId)
        {
            var GenderRule = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == leaveTypeId && x.RuleName == LeaveRule.Gender).FirstOrDefaultAsync();

            if(GenderRule == null)
            {
                return true;
            }


            var empPersonalInfo = await _unitOfWork.Repository<Hrm.Domain.EmpPersonalInfo>().Where(x => x.EmpId == empId).FirstOrDefaultAsync();

            if(empPersonalInfo == null)
            {
                throw new BadRequestException("Employee Personal Info is not found");
            }

            if(empPersonalInfo.GenderId == null)
            {
                throw new BadRequestException("Employee Gender Information is not found");
            }

            if(GenderRule.RuleValue != empPersonalInfo.GenderId)
            {
                throw new BadRequestException("Leave type is not available for this particular gender");
            }

            return true;

        }


        public async Task HaveMinimumAge(int empId, int leaveTypeId)
        {
            var haveMinAgeRule = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == leaveTypeId && x.RuleName == LeaveRule.MinimumAge).FirstOrDefaultAsync();

            if (haveMinAgeRule == null)
            {
                return;
            }

            var empInfo = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Get(empId);

            if(empInfo.DateOfBirth == null)
            {
                throw new BadRequestException("The birthdate of employee is not found");
            }

            int age = CalculateAge((DateOnly)empInfo.DateOfBirth);

            if(age < haveMinAgeRule.RuleValue)
            {
                throw new BadRequestException($"Employee age must be {haveMinAgeRule.RuleValue}");
            }

        }

        private int CalculateAge(DateOnly birthdate)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            int age = today.Year - birthdate.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (today < birthdate.AddYears(age))
            {
                age--;
            }

            return age;
        }

        public async Task IsExceedMaxRequest(int empId, int leaveTypeId)
        {
            var haveMaxRequestLifeTime = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == leaveTypeId && x.RuleName == LeaveRule.MaxRequestLifeTime).FirstOrDefaultAsync();

            if (haveMaxRequestLifeTime == null)
            {
                return;
            }

            var totalApprovedRequest = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Where(x => x.EmpId == empId && x.LeaveTypeId == leaveTypeId && x.Status == (int)LeaveStatusOption.FinalApproved).CountAsync();

            int maxRequest = haveMaxRequestLifeTime.RuleValue;

            if(maxRequest<=totalApprovedRequest)
            {
                throw new BadRequestException("Maximum Request is exceed");
            }

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
                maxDaysPerYear = leaveRules.Where(x=> x.RuleName == LeaveRule.MaxDaysPerYear).Select(x=>x.RuleValue).ToList()[0];
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

                return Math.Max(0, totalLeave - takenLeave);
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

            if(haveAccuralRule)
            {
                int takenLeave = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveTypeId == LeaveTypeId).CountAsync();

                return accuralLeave - takenLeave;
            }




            return -1;            

        }
    }
}
