using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BloodGroups.Handlers.Queries
{ 
    public class GetSelectedBloodGroupRequestHandler : IRequestHandler<GetSelectedBloodGroupRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.BloodGroup> _BloodGroupRepository;


        public GetSelectedBloodGroupRequestHandler(IHrmRepository<Hrm.Domain.BloodGroup> BloodGroupRepository)
        {
            _BloodGroupRepository = BloodGroupRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBloodGroupRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.BloodGroup> BloodGroups = await _BloodGroupRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = BloodGroups.Select(x => new SelectedModel 
            {
                Name = x.BloodGroupName,
                Id = x.BloodGroupId
            }).ToList();
            return selectModels;
        }
    }
}
 