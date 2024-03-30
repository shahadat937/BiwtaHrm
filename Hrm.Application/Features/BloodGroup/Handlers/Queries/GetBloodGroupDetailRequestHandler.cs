using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.BloodGroups.Handlers.Queries
{
    public class GetBloodGroupDetailRequestHandler : IRequestHandler<GetBloodGroupDetailRequest, BloodGroupDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.BloodGroup> _BloodGroupRepository;
        public GetBloodGroupDetailRequestHandler(IHrmRepository<Hrm.Domain.BloodGroup> BloodGroupRepository, IMapper mapper)
        {
            _BloodGroupRepository = BloodGroupRepository;
            _mapper = mapper;
        }
        public async Task<BloodGroupDto> Handle(GetBloodGroupDetailRequest request, CancellationToken cancellationToken)
        {
            var BloodGroup = await _BloodGroupRepository.Get(request.BloodGroupId);
            return _mapper.Map<BloodGroupDto>(BloodGroup);
        }
    }
}
