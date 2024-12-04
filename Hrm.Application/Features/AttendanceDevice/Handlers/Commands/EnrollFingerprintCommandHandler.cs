using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public class EnrollFingerprintCommandHandler : IRequestHandler<EnrollFingerprintCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttendanceDevice _attendanceDevice;

        public EnrollFingerprintCommandHandler(IUnitOfWork unitOfWork, IAttendanceDevice attendanceDevice)
        {
            _unitOfWork = unitOfWork;
            _attendanceDevice = attendanceDevice;
        }

        public async Task<BaseCommandResponse> Handle(EnrollFingerprintCommand request, CancellationToken cancellationToken)
        {
            if(request.EmpId == null && request.IdCardNo == null)
            {
                throw new BadRequestException("At least Employee Id or PMIS should be given");
            }

            var employee = await _unitOfWork.Repository<Hrm.Domain.EmpBasicInfo>().Where(x => x.Id == request.EmpId || x.IdCardNo == request.IdCardNo).FirstOrDefaultAsync();

            if(employee == null)
            {
                throw new NotFoundException("Employee",request.EmpId);
            }

            var device = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.Id == request.DeviceId && x.Status == true).FirstOrDefaultAsync();

            if(device == null)
            {
                throw new NotFoundException("Attendance Device", request.DeviceId);
            }

            int fid = request.FID.HasValue ? (int)request.FID : 5;

            int id = await _attendanceDevice.EnrollFingerPrint(device.SN, employee.IdCardNo, fid);

            var response = new BaseCommandResponse();
            response.Success = id != -1;
            response.Message = id != -1 ? "Successful" : "Unsuccessful";
            response.Id = id;

            return response;


        }
    }
}
