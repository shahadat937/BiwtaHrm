using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.UserRoles.Requests.Queries;
using Hrm.Application.DTOs.UserRole;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;
using Hrm.Application.DTOs.AspNetUserRoles;

namespace Hrm.Application.Features.UserRoles.Handlers.Queries
{
    public class GetUserRoleDetailRequestHandler : IRequestHandler<GetUserRoleDetailRequest, AspNetUserRolesDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<AspNetUserRoles> _UserRoleRepository;
        public GetUserRoleDetailRequestHandler(IHrmRepository<AspNetUserRoles> UserRoleRepository, IMapper mapper)
        {
            _UserRoleRepository = UserRoleRepository;
            _mapper = mapper;
        }
        public async Task<AspNetUserRolesDto> Handle(GetUserRoleDetailRequest request, CancellationToken cancellationToken)
        {
            var UserRole = await _UserRoleRepository.FindOneAsync(x => x.UserId == request.UserId);
            return _mapper.Map<AspNetUserRolesDto>(UserRole);
        }
    }
}
