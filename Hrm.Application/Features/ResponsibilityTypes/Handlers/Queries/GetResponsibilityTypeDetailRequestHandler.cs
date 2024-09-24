using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.ResponsibilityTypes.Requests.Queries;
using Hrm.Application.DTOs.ResponsibilityType;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.ResponsibilityTypes.Handlers.Queries
{
    public class GetResponsibilityTypeDetailRequestHandler : IRequestHandler<GetResponsibilityTypeDetailRequest, ResponsibilityTypeDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.ResponsibilityType> _ResponsibilityTypeRepository;
        public GetResponsibilityTypeDetailRequestHandler(IHrmRepository<Hrm.Domain.ResponsibilityType> ResponsibilityTypeRepository, IMapper mapper)
        {
            _ResponsibilityTypeRepository = ResponsibilityTypeRepository;
            _mapper = mapper;
        }
        public async Task<ResponsibilityTypeDto> Handle(GetResponsibilityTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var ResponsibilityType = await _ResponsibilityTypeRepository.Get(request.ResponsibilityTypeId);
            return _mapper.Map<ResponsibilityTypeDto>(ResponsibilityType);
        }
    }
}
