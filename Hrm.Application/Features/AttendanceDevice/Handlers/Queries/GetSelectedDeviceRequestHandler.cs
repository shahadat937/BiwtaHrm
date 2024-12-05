using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Queries
{
    public class GetSelectedDeviceRequestHandler : IRequestHandler<GetSelectedDeviceRequest,List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.AttDevices> _AttDeviceRepo;

        public GetSelectedDeviceRequestHandler(IHrmRepository<Domain.AttDevices> AttDeviceRepo)
        {
            _AttDeviceRepo = AttDeviceRepo;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDeviceRequest request, CancellationToken cancellationToken)
        {
            var selectedDevice = await _AttDeviceRepo.Where(x => x.Status == true).Select(x => new SelectedModel
            {
                Id = x.Id,
                Name = x.Title
            }).ToListAsync();

            return selectedDevice;
        }
    }
}
