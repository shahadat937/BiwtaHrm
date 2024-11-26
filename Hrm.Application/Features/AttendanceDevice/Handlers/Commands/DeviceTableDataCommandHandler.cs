using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Commands
{
    public class DeviceTableDataCommandHandler : IRequestHandler<DeviceTableDataCommand,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeviceTableDataCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(DeviceTableDataCommand request, CancellationToken cancellationToken)
        {
            var IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == request.SN && x.Status == true).AnyAsync();

            if(!IsAuthorizedDevice)
            {
                return "Invalid Options";
            }

            return "OK\n";
        }
    }
}
