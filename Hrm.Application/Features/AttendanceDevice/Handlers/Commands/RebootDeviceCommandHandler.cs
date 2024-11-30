using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Interfaces;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class RebootDeviceCommandHandler : IRequestHandler<RebootDeviceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendanceDevice _attendanceDevice;

        public RebootDeviceCommandHandler(IUnitOfWork unitOfWork, IAttendanceDevice attendanceDevice)
        {
            _unitOfWork = unitOfWork;
            _attendanceDevice = attendanceDevice;
        }

        public async Task<BaseCommandResponse> Handle(RebootDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.Id == request.DeviceId && x.Status == true).FirstOrDefaultAsync();

            if(device == null)
            {
                throw new NotFoundException("Attendance Device", request.DeviceId);
            }

            var response = new BaseCommandResponse();

            bool ok = await _attendanceDevice.RebootDevice(device.SN);

            response.Success = ok;
            response.Message = ok ? "Successful" : "Unsuccessful";
            response.Id = request.DeviceId;

            return response;
        }
    }
}
