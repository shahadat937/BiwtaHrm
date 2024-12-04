using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper.Configuration.Attributes;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PendingDevice;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Queries
{
    public class GetPendingDevicesRequestHandler : IRequestHandler<GetPendingDevicesRequest,List<PendingDeviceDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.PendingDevice> _PendingDeviceRepo;
        private readonly IMapper _mapper;

        public GetPendingDevicesRequestHandler (IHrmRepository<Domain.PendingDevice> PendingDeviceRepo, IMapper mapper)
        {
            _PendingDeviceRepo = PendingDeviceRepo;
            _mapper = mapper;
        }

        public async Task<List<PendingDeviceDto>> Handle(GetPendingDevicesRequest request, CancellationToken cancellationToken)
        {
            var pendingDevice = await _PendingDeviceRepo.Where(x => x.ExpireTime >= DateTime.Now).OrderByDescending(x=>x.ExpireTime).ToListAsync();

            var pendingDeviceDto = _mapper.Map<List<PendingDeviceDto>>(pendingDevice);

            return pendingDeviceDto;
        }
    }
}
