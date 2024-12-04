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
using System.Reflection.Metadata.Ecma335;
using CsvHelper.Configuration.Attributes;
using Microsoft.VisualBasic;
using MediatR;

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
            bool IsSandwichLeave = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == leaveTypeId && x.IsActive == true && x.RuleName == LeaveRule.SandwichLeave).AnyAsync();

            if (leaveRules.Count<=0)
            {
                return true;
            }

            bool haveMinAge = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == leaveTypeId && x.RuleName == LeaveRule.MinimumAge).AnyAsync();

            await IsExceedMaxRequest(empId, leaveTypeId);
            await IsCorrectGender(empId, leaveTypeId);
            await HaveMinimumAge(empId, leaveTypeId);
            await ValidateApplyFreq(empId, leaveTypeId, startDate);


            int totalLeaveDays = await AttendanceHelper.calculateWorkingDay(startDate, endDate, startDate.Year, _unitOfWork);

            if(IsSandwichLeave)
            {
                totalLeaveDays = (int) endDate.Subtract(startDate).TotalDays + 1;
            }

            if(await IsTotalDayExceedPerRequest(empId, leaveTypeId, totalLeaveDays))
            {
                throw new BadRequestException("Total Leave Days is exceed per request");
            }

            List<int> leaveDue = await this.CalculateLeaveAmount(empId, leaveTypeId, startDate, endDate, startDate.Year);

            if (leaveDue[1] == -1)
            {
                return true;
            }

            return (bool)(totalLeaveDays <= leaveDue[1]);

            
        }

        public async Task<bool> IsTotalDayExceedPerRequest(int empId, int leaveTypeId, int totalLeave)
        {
            var leaveRule = await _unitOfWork.Repository<Domain.LeaveRules>().Where(x=>x.LeaveTypeId == leaveTypeId && x.RuleName==LeaveRule.MaxDaysPerRequest).FirstOrDefaultAsync();

            if(leaveRule == null)
            {
                return false;
            }

            return totalLeave > leaveRule.RuleValue;

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


        public async Task<List<int>> CalculateLeaveAmount(int empId, int LeaveTypeId, DateTime startDate, DateTime endYear, int curYear)
        {
            var leaveRules = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == LeaveTypeId).ToListAsync();

            List<int> leaveAmountDue = new List<int>(2);
            
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

                totalWorkingDayLIfetime = await calculateWorkingDays(empId, joiningDate, startDate);

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
                int takenLeave = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.EmpId==empId && x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveTypeId == LeaveTypeId && x.AttendanceDate.Month == startDate.Month && x.AttendanceDate.Year == startDate.Year).CountAsync();

                int totalLeave = maxDaysPerMonth;

                if(haveAccuralRule)
                {
                    totalLeave = Math.Min(totalLeave, accuralLeave);

                }
                
                if(haveMaxDaysPerYear)
                {
                    totalLeave = Math.Min(totalLeave, maxDaysPerYear);
                }

                if(haveMaxDaysLifeTime)
                {
                    totalLeave = Math.Min(totalLeave, maxDaysLifeTime);
                }

                leaveAmountDue.Add(totalLeave);
                leaveAmountDue.Add(Math.Max(0, totalLeave - takenLeave));
                return leaveAmountDue;

            }

            if(haveMaxDaysPerYear)
            {
                int takenLeave = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.EmpId==empId && x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveTypeId == LeaveTypeId && x.AttendanceDate.Year == startDate.Year).CountAsync();

                int totalLeave = maxDaysPerYear;

                if(haveAccuralRule)
                {
                    totalLeave = Math.Min(totalLeave, accuralLeave);
                }

                if(haveMaxDaysLifeTime)
                {
                    totalLeave = Math.Min(totalLeave, maxDaysLifeTime);
                }

                leaveAmountDue.Add(totalLeave);
                leaveAmountDue.Add(Math.Max(0, totalLeave - takenLeave));
                return leaveAmountDue;
            }

            if(haveMaxDaysLifeTime)
            {
                int takenLeave = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.EmpId==empId && x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveTypeId == LeaveTypeId).CountAsync();
                
                int totalLeave = maxDaysLifeTime;

                if(haveAccuralRule)
                {
                    Math.Min(totalLeave, accuralLeave);
                }

                leaveAmountDue.Add(totalLeave);
                leaveAmountDue.Add(Math.Max(0, totalLeave - takenLeave));
                return leaveAmountDue;
            }

            if(haveAccuralRule)
            {
                int takenLeave = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.EmpId==empId && x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveTypeId == LeaveTypeId).CountAsync();

                leaveAmountDue.Add(accuralLeave);
                leaveAmountDue.Add(Math.Max(0, accuralLeave - takenLeave));

                return leaveAmountDue;
            }


            leaveAmountDue.Add(-1);
            leaveAmountDue.Add(-1);
            return leaveAmountDue;

        }

        private async Task<int> calculateWorkingDays(int empId, DateTime startDate, DateTime endDate)
        {
            int totalDays = (int) endDate.Subtract(startDate).TotalDays;

            int totalLeaves = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x => x.AttendanceStatusId == (int)AttendanceStatusOption.OnLeave && x.LeaveRequest.LeaveType.ELWorkDayCal == true && x.EmpId == empId && x.AttendanceDate < DateOnly.FromDateTime(endDate)).CountAsync();

            return totalDays - totalLeaves;
        }

        public async Task<int> GetAvailedLeave(int empId, int leaveTypeId)
        {
            int availed = await _unitOfWork.Repository<Hrm.Domain.Attendance>().Where(x=>x.AttendanceStatusId == (int) AttendanceStatusOption.OnLeave&& x.LeaveRequest.LeaveTypeId == (int) leaveTypeId && x.EmpId == empId).CountAsync();

            return availed;
        }

        public async Task<int> GetTotalApplied(int empId, int leaveTypeId)
        {
            int applied = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Where(x=>x.EmpId == empId && x.LeaveTypeId ==  leaveTypeId).CountAsync();

            return applied;
        }

        public async Task<bool> ValidateApplyFreq(int empId, int leaveTypeId, DateTime startDate)
        {
            var haveAppliedFreqRule = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.LeaveTypeId == leaveTypeId && x.RuleName == LeaveRule.ApplyFreq).FirstOrDefaultAsync();

            if(haveAppliedFreqRule==null)
            {
                return true;
            }

            var leaveRequests = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Where(x => x.EmpId == empId && x.LeaveTypeId == leaveTypeId && x.FromDate <= startDate).OrderBy(x => x.DateCreated).ToListAsync();


            if(leaveRequests.Count() <= 0)
            {
                return true;
            }

            DateTime previousApplicationDate = leaveRequests[^1].FromDate;

            for(var i = leaveRequests.Count - 2; i>=0;i--)
            {
                if (leaveRequests[i].Status == (int) LeaveStatusOption.ReviewerApproved || leaveRequests[i].Status == (int) LeaveStatusOption.FinalApproved)
                {
                    break;
                }

                if (leaveRequests[i].Status == (int)LeaveStatusOption.ReviewerDenied || leaveRequests[i].Status == (int)LeaveStatusOption.FinalDenied)
                {
                    previousApplicationDate = (DateTime)leaveRequests[i].DateCreated;
                }
            }


            TimeSpan days= startDate.Subtract(previousApplicationDate);

            if(days.TotalDays>haveAppliedFreqRule.RuleValue)
            {
                return true;
            }

            throw new BadRequestException($"You should apply after {haveAppliedFreqRule.RuleValue} days from the previous application");

        }
    }
}
