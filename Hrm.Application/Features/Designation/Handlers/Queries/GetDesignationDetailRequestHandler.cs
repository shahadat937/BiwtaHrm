using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Designations.Requests.Queries;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Designations.Handlers.Queries
{
    public class GetDesignationDetailRequestHandler : IRequestHandler<GetDesignationDetailRequest, DesignationDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        public GetDesignationDetailRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IMapper mapper)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;
        }
        public async Task<DesignationDto> Handle(GetDesignationDetailRequest request, CancellationToken cancellationToken)
        {
            var Designation = await _DesignationRepository.Get(request.DesignationId);
            return _mapper.Map<DesignationDto>(Designation);
        }
    }
}
