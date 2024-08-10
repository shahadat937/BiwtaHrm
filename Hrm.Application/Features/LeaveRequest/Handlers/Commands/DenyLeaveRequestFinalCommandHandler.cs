using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.LeaveRequest.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Helpers;
using Hrm.Application.Enum;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Commands
{
    public class DenyLeaveRequestFinalCommandHandler: IRequestHandler<DenyLeaveRequestFinalCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LeaveAtdHelper leaveAtdHelper;

        public DenyLeaveRequestFinalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            leaveAtdHelper = new LeaveAtdHelper(_unitOfWork,_mapper);
        }

        public async Task<BaseCommandResponse> Handle(DenyLeaveRequestFinalCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveRequest = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Get(request.LeaveRequestId);

            if(leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest),request.LeaveRequestId);
            }

            if(leaveRequest.Status == (int)LeaveStatusOption.FinalDenied)
            {
                response.Success = false;
                response.Message = "Request has already been denied";
                response.Id  = leaveRequest.LeaveRequestId;
                return response;
            }

            leaveAtdHelper.leaveRequestId = leaveRequest.LeaveRequestId;

            await leaveAtdHelper.deleteAttendance();

            leaveRequest.Status = (int)LeaveStatusOption.FinalDenied;

            await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Update(leaveRequest);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Leave Request is denied";
            response.Id = leaveRequest.LeaveRequestId;

            return response;
            
        }
    }
}
