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

namespace Hrm.Application.Features.LeaveRequest.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler: IRequestHandler<DeleteLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteLeaveRequestCommand request,  CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Get(request.LeaveRequestId);

            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.LeaveRequestId);
            }

            await _unitOfWork.Repository<Hrm.Domain.LeaveRequest>().Delete(leaveRequest);

            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.LeaveRequestId;
            return response;
        }
    }
}
