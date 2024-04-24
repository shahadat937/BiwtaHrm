using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.UserRoles.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.UserRoles.Handlers.Queries
{ 
    public class GetSelectedUserRoleRequestHandler : IRequestHandler<GetSelectedUserRoleRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.UserRole> _UserRoleRepository;


        public GetSelectedUserRoleRequestHandler(IHrmRepository<Hrm.Domain.UserRole> UserRoleRepository)
        {
            _UserRoleRepository = UserRoleRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedUserRoleRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.UserRole> UserRoles = await _UserRoleRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = UserRoles.Select(x => new SelectedModel 
            {
                Name = x.UserRoleName,
                Id = x.UserRoleId
            }).ToList();
            return selectModels;
        }
    }
}
 