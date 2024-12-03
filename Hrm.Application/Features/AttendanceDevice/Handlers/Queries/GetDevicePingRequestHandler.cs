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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHrmRepository<Hrm.Domain.AttDevices> _repo;

        public GetDevicePingRequestHandler(IHrmRepository<Domain.AttDevices> repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(GetDevicePingRequest request, CancellationToken cancellationToken)
        {
            var device = await _repo.Where(x => x.SN == request.SN && x.Status == true).FirstOrDefaultAsync();

            if(device == null)
            {
                return "Invalid Options";
            }

            device.LastOnline = DateTime.Now;
            await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Update(device);
            await _unitOfWork.Save();

            return "OK";
        }
    }
}
