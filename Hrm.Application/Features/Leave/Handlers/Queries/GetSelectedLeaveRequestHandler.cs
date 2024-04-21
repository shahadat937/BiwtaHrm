using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Leaves.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Leaves.Handlers.Queries
{ 
    public class GetSelectedLeaveRequestHandler : IRequestHandler<GetSelectedLeaveRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Leave> _LeaveRepository;


        public GetSelectedLeaveRequestHandler(IHrmRepository<Hrm.Domain.Leave> LeaveRepository)
        {
            _LeaveRepository = LeaveRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedLeaveRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Leave> Leaves = await _LeaveRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Leaves.Select(x => new SelectedModel 
            {
                Name = x.LeaveName,
                Id = x.LeaveId
            }).ToList();
            return selectModels;
        }
    }
}
 