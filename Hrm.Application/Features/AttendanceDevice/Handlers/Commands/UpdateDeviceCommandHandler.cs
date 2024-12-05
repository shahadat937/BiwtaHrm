using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AttDevice.Validators;
using Hrm.Application.Features.AttendanceDevice.Interfaces;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttendanceDevice _attendanceDevice;

        public UpdateDeviceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAttendanceDevice attendanceDevice)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attendanceDevice = attendanceDevice;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var validator = new AttDevicesDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Device);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var device = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Get(request.Device.Id);
            var response = new BaseCommandResponse();

            if(device == null)
            {
                throw new NotFoundException("Attendance Device", request.Device.Id);
            }

            _mapper.Map(request.Device, device);

            try
            {
                await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Update(device);
                await _unitOfWork.Save();
                await _attendanceDevice.CustomCommand(device.SN, "CHECK");
            } catch(Exception ex)
            {
                response.Success = false;
                response.Message = "Internal Database Error";
                response.Id = device.Id;
                return response;
            }

            response.Success = true;
            response.Message = "Successful";
            response.Id = device.Id;

            return response;

        }
    }
}
