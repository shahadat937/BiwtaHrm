using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.AttDevice;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Queries
{
    public class GetDeviceRequestHandler : IRequestHandler<GetDeviceRequest,List<AttDevicesDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.AttDevices> _AttDevicesRepo;
        private readonly IMapper _mapper;

        public GetDeviceRequestHandler(IHrmRepository<Domain.AttDevices> AttDevicesRepo, IMapper mapper)
        {
            _AttDevicesRepo = AttDevicesRepo;
            _mapper = mapper;
        }

        public async Task<List<AttDevicesDto>> Handle(GetDeviceRequest request, CancellationToken cancellationToken)
        {
            var attDevices = _AttDevicesRepo.Where(x => true);

            var attDevicesdto = _mapper.Map<List<AttDevicesDto>>(await attDevices.ToListAsync());


            return attDevicesdto;
        }
    }
}
