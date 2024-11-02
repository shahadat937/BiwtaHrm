using AutoMapper;
using Hrm.Application.Constants;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Attendance.Requests.Queries;
using Hrm.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Handlers.Queries
{
    public class GetWorkingDaysRequestHandler: IRequestHandler<GetWorkingDaysRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWorkingDaysRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetWorkingDaysRequest request, CancellationToken cancellationToken)
        {
            bool IsSandwichLeave = true;
            if(request.LeaveTypeId.HasValue)
            {
                var leaveRules = await _unitOfWork.Repository<Hrm.Domain.LeaveRules>().Where(x => x.IsActive == true && x.LeaveTypeId == request.LeaveTypeId && x.RuleName == LeaveRule.SandwichLeave).AnyAsync();
                IsSandwichLeave = leaveRules;
            }

            int workingDay;

            if(IsSandwichLeave)
            {
                workingDay = (int)request.To.Subtract(request.From).TotalDays + 1;

                return workingDay;
            }

            workingDay = await AttendanceHelper.calculateWorkingDay(request.From, request.To, request.From.Year, _unitOfWork);

            return workingDay;
        }
    }
}
