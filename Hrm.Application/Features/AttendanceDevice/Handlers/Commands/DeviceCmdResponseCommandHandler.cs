using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using Hrm.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class DeviceCmdResponseCommandHandler: IRequestHandler<DeviceCmdResponseCommand,object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceCmdResponseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeviceCmdResponseCommand request, CancellationToken cancellationToken)
        {
            var device = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == request.SN).FirstOrDefaultAsync();

            if(device==null)
            {
                return "Not Authorized Terminal";
            }

            if(request.CommandResponse == null)
            {
                return "Invalid Options\n";
            }
            int commandId;
            if(!Int32.TryParse(request.CommandResponse.Id, out commandId))
            {
                // Todo: 
            }

            var command = await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Where(x=>x.Id == commandId).FirstOrDefaultAsync();

            if(command != null)
            {
                await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Delete(command);
                await _unitOfWork.Save();
            }

            bool updateDeviceInfo = false;
            if(request.CommandResponse.DeviceName!=null)
            {
                updateDeviceInfo = true;
                device.DeviceName = request.CommandResponse.DeviceName;
            }

            if(request.CommandResponse.MAC!=null)
            {
                updateDeviceInfo = true;
                device.MAC = request.CommandResponse.MAC;
            }

            if(request.CommandResponse.IpAddress!=null)
            {
                updateDeviceInfo = true;
                device.LocalIpAddress = request.CommandResponse.IpAddress;
            }

            if(updateDeviceInfo)
            {
                await _unitOfWork.Repository<Domain.AttDevices>().Update(device);
                await _unitOfWork.Save();
            }
            return "OK\n";
        }
    }
}
