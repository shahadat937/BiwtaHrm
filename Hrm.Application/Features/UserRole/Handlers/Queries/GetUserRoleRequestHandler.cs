using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.UserRole;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.UserRole.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.UserRole.Handlers.Queries
{
    public class GetUserRoleRequestHandler : IRequestHandler<GetUserRoleRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.UserRole> _UserRoleRepository;
        private readonly IMapper _mapper;
        public GetUserRoleRequestHandler(IHrmRepository<Hrm.Domain.UserRole> UserRoleRepository, IMapper mapper)
        {
            _UserRoleRepository = UserRoleRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetUserRoleRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.UserRole> UserRoles = _UserRoleRepository.Where(x => true);

            // Order blood groups by descending order
            UserRoles = UserRoles.OrderByDescending(x => x.UserRoleId);

            // Map the ordered blood groups to UserRoleDto
            var UserRoleDtos = _mapper.Map<List<UserRoleDto>>(UserRoles.ToList());

            return UserRoleDtos;
        }
    }
}
