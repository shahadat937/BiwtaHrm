using AutoMapper;
using Hrm.Application.Contracts.Persistence;
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
    public class DeleteLeaveTypeCommandHandler: IRequestHandler<DeleteLeaveTypeCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLeaveTypeCommandHandler (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _unitOfWork.Repository<Hrm.Domain.LeaveType>().Get(request.LeaveTypeId);

            if(leaveType == null)
            {
                throw new NotFoundException(nameof(LeaveType),request.LeaveTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.LeaveType>().Delete(leaveType);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.LeaveTypeId;

            return response;
        }
    }
}
