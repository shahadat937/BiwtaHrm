using AutoMapper;
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
using Hrm.Application.Helpers;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Commands
{
    public class UpdateLeaveRequestByIdCommandHandler: IRequestHandler<UpdateLeaveRequestByIdCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveRequestByIdCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveRequest = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Get(request.LeaveRequestDto.LeaveRequestId);

            if(leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.LeaveRequestDto.LeaveRequestId);
            }


            if(leaveRequest.Status != (int) LeaveStatusOption.Pending)
            {
                throw new BadRequestException("To Update , leave request should be in pending state");
            }

            var leaveValidator = new LeaveRequestValidator(_unitOfWork);

            bool leaveValidationResult = await leaveValidator.Validate(request.LeaveRequestDto.FromDate, request.LeaveRequestDto.ToDate, request.LeaveRequestDto.EmpId, request.LeaveRequestDto.LeaveTypeId);

            if(!leaveValidationResult)
            {
                response.Success = false;
                response.Message = "Leave balance is not enough";
                response.Id = request.LeaveRequestDto.LeaveTypeId;
                return response;
            }

            _mapper.Map(request.LeaveRequestDto, leaveRequest);

            await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Update(leaveRequest);
            await _unitOfWork.Save();

            
            response.Success = true;
            response.Message = "Update Successful";
            response.Id = leaveRequest.LeaveRequestId;

            return response;
        }
    }
}
