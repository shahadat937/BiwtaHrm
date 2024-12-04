using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.AttendanceDevice.Interfaces;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class DeleteUserDeviceCommandHandler : IRequestHandler<DeleteUserDeviceCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendanceDevice _attendanceDevice;

        public DeleteUserDeviceCommandHandler(IUnitOfWork unitOfWork, IAttendanceDevice attendanceDevice)
        {
            _unitOfWork = unitOfWork;
            _attendanceDevice = attendanceDevice;
        }

        public async Task<BaseCommandResponse> Handle(DeleteUserDeviceCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Where(x => x.Id == request.EmpId).FirstOrDefaultAsync();
            var device = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.Id == request.DeviceId && x.Status == true).FirstOrDefaultAsync();

            if(employee == null)
            {
                throw new NotFoundException("Employee", request.EmpId);
            }

            if(device == null)
            {
                throw new NotFoundException("Attendance Device", request.DeviceId);
            }

            bool ok = await _attendanceDevice.DeleteUser(device.SN, employee.IdCardNo);
            var response = new BaseCommandResponse();
            response.Success = ok;
            response.Message = ok ? "Successful" : "Unsuccessful";

            return response;
            
        }
    }
}
