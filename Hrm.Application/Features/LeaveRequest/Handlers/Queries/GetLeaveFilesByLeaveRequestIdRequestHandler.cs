using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveRequest.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Handlers.Queries
{
    public class GetLeaveFilesByLeaveRequestIdRequestHandler: IRequestHandler<GetLeaveFilesByLeaveRequestIdRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveFiles> _LeaveFilesRepo;

        public GetLeaveFilesByLeaveRequestIdRequestHandler(IHrmRepository<Domain.LeaveFiles> leaveFilesRepo)
        {
            _LeaveFilesRepo = leaveFilesRepo;
        }

        public async Task<object> Handle(GetLeaveFilesByLeaveRequestIdRequest request, CancellationToken cancellationToken)
        {
            var leaveFiles = await _LeaveFilesRepo.Where(x => x.LeaveRequestId == request.LeaveRequestId).ToListAsync();

            return leaveFiles;
        }
    }
}
