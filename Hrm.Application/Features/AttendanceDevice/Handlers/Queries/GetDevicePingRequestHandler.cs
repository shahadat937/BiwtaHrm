using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Queries
{
    public class GetDevicePingRequestHandler : IRequestHandler<GetDevicePingRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.AttDevices> _repo;

        public GetDevicePingRequestHandler(IHrmRepository<Domain.AttDevices> repo)
        {
            _repo = repo;
        }

        public async Task<object> Handle(GetDevicePingRequest request, CancellationToken cancellationToken)
        {
            var IsAuthorizedDevice = await _repo.Where(x => x.SN == request.SN && x.Status == true).AnyAsync();

            if(!IsAuthorizedDevice)
            {
                return "Invalid Options";
            }

            return "OK";
        }
    }
}
