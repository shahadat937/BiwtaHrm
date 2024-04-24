using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.UserRoles.Requests.Queries;
using Hrm.Application.DTOs.UserRole;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.UserRoles.Handlers.Queries
{
    public class GetUserRoleDetailRequestHandler : IRequestHandler<GetUserRoleDetailRequest, UserRoleDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.UserRole> _UserRoleRepository;
        public GetUserRoleDetailRequestHandler(IHrmRepository<Hrm.Domain.UserRole> UserRoleRepository, IMapper mapper)
        {
            _UserRoleRepository = UserRoleRepository;
            _mapper = mapper;
        }
        public async Task<UserRoleDto> Handle(GetUserRoleDetailRequest request, CancellationToken cancellationToken)
        {
            var UserRole = await _UserRoleRepository.Get(request.UserRoleId);
            return _mapper.Map<UserRoleDto>(UserRole);
        }
    }
}
