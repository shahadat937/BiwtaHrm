﻿using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveRequest.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Enum;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Commands
{
    public class DenyLeaveRequestReviewerCommandHandler: IRequestHandler<DenyLeaveRequestReviewerCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public DenyLeaveRequestReviewerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DenyLeaveRequestReviewerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveRequest = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Get(request.LeaveRequestId);

            if(leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.LeaveRequestId);
            }

            if(leaveRequest.Status == (int)LeaveStatusOption.FinalApproved)
            {
                response.Success = false;
                response.Message = "Request is already approved by higher authority";
                response.Id = leaveRequest.LeaveRequestId;
                return response;
            }

            leaveRequest.Status = (int)LeaveStatusOption.ReviewerDenied;

            await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Update(leaveRequest);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Denied By Reviewer";
            response.Id = leaveRequest.LeaveRequestId;

            return response;
        }
    }
}
