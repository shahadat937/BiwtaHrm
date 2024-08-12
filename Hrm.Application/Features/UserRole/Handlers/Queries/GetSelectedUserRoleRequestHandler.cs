using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.UserRoles.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.UserRoles.Handlers.Queries
{ 
    public class GetSelectedUserRoleRequestHandler : IRequestHandler<GetSelectedUserRoleRequest, List<SelectedStringModel>>
    {
        private readonly IHrmRepository<AspNetRoles> _UserRoleRepository;


        public GetSelectedUserRoleRequestHandler(IHrmRepository<AspNetRoles> UserRoleRepository)
        {
            _UserRoleRepository = UserRoleRepository;
        }

        public async Task<List<SelectedStringModel>> Handle(GetSelectedUserRoleRequest request, CancellationToken cancellationToken)
        {
            ICollection<AspNetRoles> UserRoles = await _UserRoleRepository.FilterAsync(x => true);
            List<SelectedStringModel> selectModels = UserRoles.Select(x => new SelectedStringModel 
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 