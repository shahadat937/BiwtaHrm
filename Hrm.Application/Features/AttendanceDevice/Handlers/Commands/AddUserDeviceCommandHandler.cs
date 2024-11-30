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
    public class AddUserDeviceCommandHandler : IRequestHandler<AddUserDeviceCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendanceDevice _attendanceDevice;

        public AddUserDeviceCommandHandler(IUnitOfWork unitOfWork, IAttendanceDevice attendanceDevice)
        {
            _unitOfWork = unitOfWork;
            _attendanceDevice = attendanceDevice;
        }

        public async Task<BaseCommandResponse> Handle(AddUserDeviceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var empBasicInfo = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Where(x => x.Id == request.AddUserDeviceDto.EmpId || x.IdCardNo == request.AddUserDeviceDto.IdCardNo).FirstOrDefaultAsync();
            var device = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.Id == request.AddUserDeviceDto.DeviceId && x.Status == true).FirstOrDefaultAsync();
            if(empBasicInfo == null)
            {
                throw new NotFoundException("Employee", request.AddUserDeviceDto.EmpId);
            }

            if (device == null)
            {
                throw new NotFoundException("Attendance Device", request.AddUserDeviceDto.DeviceId);

            }

            var empPhoto = await _unitOfWork.Repository<Hrm.Domain.EmpPhotoSign>().Where(x => x.EmpId== empBasicInfo.Id).FirstOrDefaultAsync();
            string base64Image = "";
            if (empPhoto !=null)
            {
                try
                {
                    var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\EmpPhoto", empPhoto.PhotoUrl);
                    byte[] imageBytes = File.ReadAllBytes(photoPath);

                    base64Image = Convert.ToBase64String(imageBytes);
                    //await _attendanceDevice.UpdateUserPic(device.SN, empBasicInfo.IdCardNo, base64Image);
                } catch (Exception e)
                {

                }
            }

            bool ok = await _attendanceDevice.AddUser(device.SN, empBasicInfo.IdCardNo, empBasicInfo.FirstName, request.AddUserDeviceDto.Passwd, request.AddUserDeviceDto.GroupId, request.AddUserDeviceDto.Privilage);
            if(base64Image!="")
            {
                await _attendanceDevice.UpdateUserPic(device.SN, empBasicInfo.IdCardNo, base64Image);
            }
            response.Success = ok;
            response.Message = ok ? "Successful" : "Unsuccessful";
            return response;
        }
    }
}
