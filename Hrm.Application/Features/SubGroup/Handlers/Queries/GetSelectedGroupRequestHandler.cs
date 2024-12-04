using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Group.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Handlers.Queries
{ 
    public class GetSelectedGroupRequestHandler : IRequestHandler<GetSelectedGroupRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.SubGroup> _GroupRepository;


        public GetSelectedGroupRequestHandler(IHrmRepository<Hrm.Domain.SubGroup> GroupRepository)
        {
            _GroupRepository = GroupRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedGroupRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.SubGroup> Groups = await _GroupRepository.FilterAsync(x => x.ExamTypeId == request.Id && x.IsActive == true);
            List<SelectedModel> selectModels = Groups.Select(x => new SelectedModel 
            {
                Name = x.GroupName,
                Id = x.GroupId
            }).ToList();
            return selectModels;
        }
    }
}
 