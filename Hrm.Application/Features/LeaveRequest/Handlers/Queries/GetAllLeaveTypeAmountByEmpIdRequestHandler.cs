using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveType;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using Hrm.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetAllLeaveTypeAmountByEmpIdRequestHandler: IRequestHandler<GetAllLeaveTypeAmountByEmpIdRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LeaveRequestValidator _leaveValidator;
        
        public GetAllLeaveTypeAmountByEmpIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _leaveValidator = new LeaveRequestValidator(unitOfWork);
            _mapper = mapper;
            
        }

        class LeaveAmount
        {
            public int LeaveTypeId { get; set; }
            public string LeaveTypeName {  get; set; }
            public int LeaveDue { get; set; }
            public int TotalAmount { get; set; }
            public int Availed {  get; set; }
            public int Applied { get; set; }
        }

        public async Task<object> Handle(GetAllLeaveTypeAmountByEmpIdRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _unitOfWork.Repository<Hrm.Domain.LeaveType>().Where(x => true).ToListAsync();
            var leaveTypeDto = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            
            List<LeaveAmount> leaveAmounts = new List<LeaveAmount>();
            foreach(var type in leaveTypeDto)
            {
                LeaveAmount leaveAmount = new LeaveAmount();
                List<int> leaveAmountAndDue = await _leaveValidator.CalculateLeaveAmount(request.EmpId, type.LeaveTypeId, DateTime.Now, DateTime.Now, DateTime.Now.Year);
                leaveAmount.LeaveTypeId = type.LeaveTypeId;
                leaveAmount.LeaveTypeName = type.LeaveTypeName;
                leaveAmount.TotalAmount = leaveAmountAndDue[0];
                leaveAmount.LeaveDue = leaveAmountAndDue[1];
                leaveAmount.Availed = await _leaveValidator.GetAvailedLeave(request.EmpId, type.LeaveTypeId);
                leaveAmount.Applied = await _leaveValidator.GetTotalApplied(request.EmpId, type.LeaveTypeId);
                leaveAmounts.Add(leaveAmount);
            }

            return leaveAmounts;
        }
    }
}
