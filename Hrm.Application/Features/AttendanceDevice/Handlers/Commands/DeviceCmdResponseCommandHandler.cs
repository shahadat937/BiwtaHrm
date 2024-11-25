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
            bool IsAuthorized = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == request.SN).AnyAsync();

            if(!IsAuthorized)
            {
                return "Not Authorized Terminal";
            }

            if(request.CommandResponse == null)
            {
                return "OK\n";
            }
            int commandId;
            if(!Int32.TryParse(request.CommandResponse.Id, out commandId))
            {
                return "OK\n";
            }

            var command = await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Where(x=>x.Id == commandId).FirstOrDefaultAsync();

            if(command != null)
            {
                await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Delete(command);
                await _unitOfWork.Save();
            }

            return "OK\n";
        }
    }
}
