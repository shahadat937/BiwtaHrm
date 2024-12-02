using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeviceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Get(request.DeviceId);

            if(device == null)
            {
                throw new NotFoundException("Attendance Device", request.DeviceId);
            }

            var response = new BaseCommandResponse();

            await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Delete(device);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Successfully Deleted";
            response.Id = request.DeviceId;

            return response;
        }
    }
}
