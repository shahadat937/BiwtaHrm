using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.LeaveRequest.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using Hrm.Application.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Helpers;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Commands
{
    public class ApproveLeaveRequestFinalCommandHandler: IRequestHandler<ApproveLeaveRequestFinalCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LeaveAtdHelper leaveAtdHelper;

        public ApproveLeaveRequestFinalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            leaveAtdHelper = new LeaveAtdHelper(unitOfWork, _mapper);
        }

        public async Task<BaseCommandResponse> Handle(ApproveLeaveRequestFinalCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var leaveRequest = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Get(request.LeaveRequestId);

            if(leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest),request.LeaveRequestId);
            }

            leaveAtdHelper.leaveRequestId = leaveRequest.LeaveRequestId;

            await leaveAtdHelper.deleteAttendance();
            await leaveAtdHelper.saveAttendance(DateOnly.FromDateTime(leaveRequest.FromDate), DateOnly.FromDateTime(leaveRequest.ToDate), leaveRequest.EmpId, leaveRequest.LeaveTypeId);


            leaveRequest.Status = (int) LeaveStatusOption.FinalApproved;

            await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Update(leaveRequest);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Approved";
            response.Id = leaveRequest.LeaveRequestId;

            return response;
        }
    }
}
