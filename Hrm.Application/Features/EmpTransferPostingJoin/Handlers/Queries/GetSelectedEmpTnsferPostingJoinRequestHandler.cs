using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Queries
{ 
    public class GetSelectedEmpTnsferPostingJoinRequestHandler : IRequestHandler<GetSelectedEmpTnsferPostingJoinRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> _EmpTnsferPostingJoinRepository;


        public GetSelectedEmpTnsferPostingJoinRequestHandler(IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoinRepository)
        {
            _EmpTnsferPostingJoinRepository = EmpTnsferPostingJoinRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEmpTnsferPostingJoinRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoins = await _EmpTnsferPostingJoinRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = EmpTnsferPostingJoins.Select(x => new SelectedModel 
            {
                Name = x.EmpTnsferPostingJoinName,
                Id = x.EmpTnsferPostingJoinId
            }).ToList();
            return selectModels;
        }
    }
}
 