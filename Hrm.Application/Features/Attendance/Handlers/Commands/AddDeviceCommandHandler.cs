using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AttDevice.Validators;
using Hrm.Application.Features.Attendance.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.Features.AttendanceDevice.Interfaces;

namespace Hrm.Application.Features.Attendance.Handlers.Commands
{
    public class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendanceDevice _attendanceDevice;
        public readonly IMapper _mapper;

        public AddDeviceCommandHandler(IUnitOfWork unitOfWork, IAttendanceDevice attendanceDevice, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _attendanceDevice = attendanceDevice;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateAttDeviceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Device);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var IsAddedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == request.Device.SN).FirstOrDefaultAsync();

            if(IsAddedDevice!=null)
            {
                response.Success = false;
                response.Message = "Device is already added";
                response.Id = 0;

                return response;
            }
            var device = _mapper.Map<Hrm.Domain.AttDevices>(request.Device);
            var pendingDevice = await _unitOfWork.Repository<Hrm.Domain.PendingDevice>().Where(x => x.SN == device.SN).FirstOrDefaultAsync();

            if(pendingDevice!=null)
            {
                await _unitOfWork.Repository<Hrm.Domain.PendingDevice>().Delete(pendingDevice);
            }

            await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Add(device);
            await _unitOfWork.Save();

            if(device.Status == true)
            {
                await _attendanceDevice.CustomCommand(device.SN, "CHECK");
                await _attendanceDevice.CustomCommand(device.SN, "INFO");
            }

            response.Success = true;
            response.Message = "Successful";
            response.Id = device.Id;

            return response;

        }
    }
}
