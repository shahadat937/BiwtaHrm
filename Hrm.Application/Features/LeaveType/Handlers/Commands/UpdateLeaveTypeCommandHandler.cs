using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveType.Validators;
using Hrm.Application.Features.LeaveType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveType.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler: IRequestHandler<UpdateLeaveTypeCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.leaveTypeDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var leaveType = await _unitOfWork.Repository<Hrm.Domain.LeaveType>().Get(request.leaveTypeDto.LeaveTypeId);

            if (leaveType == null)
            {
                throw new NotFoundException(nameof(leaveType), request.leaveTypeDto.LeaveTypeId);
            }

            _mapper.Map(request.leaveTypeDto, leaveType);

            await _unitOfWork.Repository<Hrm.Domain.LeaveType>().Update(leaveType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = leaveType.LeaveTypeId;

            return response;
        }
    }
}
