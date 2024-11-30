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
using System.Runtime.InteropServices;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class AddCustomCommandCommandHandler : IRequestHandler<AddCustomCommandCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendanceDevice _attendanceDevice;

        public AddCustomCommandCommandHandler(IUnitOfWork unitOfWork, IAttendanceDevice attendanceDevice)
        {
            _unitOfWork = unitOfWork;
            _attendanceDevice = attendanceDevice;
        }

        public async Task<BaseCommandResponse> Handle(AddCustomCommandCommand request, CancellationToken cancellationToken)
        {
            var device = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.Id == request.DeviceId && x.Status == true).FirstOrDefaultAsync();

            if(device == null)
            {
                throw new NotFoundException("Attendance Device", request.DeviceId);
            }

            bool ok = await _attendanceDevice.CustomCommand(device.SN, request.Command);

            var response = new BaseCommandResponse();
            response.Success = ok;
            response.Message = ok ? "Successful" : "Unsuccessful";

            return response;

        }
    }
}
