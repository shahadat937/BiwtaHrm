using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.DesignationSetups.Requests.Queries;
using Hrm.Application.DTOs.DesignationSetup;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.DesignationSetups.Handlers.Queries
{
    public class GetDesignationSetupDetailRequestHandler : IRequestHandler<GetDesignationSetupDetailRequest, DesignationSetupDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.DesignationSetup> _DesignationSetupRepository;
        public GetDesignationSetupDetailRequestHandler(IHrmRepository<Hrm.Domain.DesignationSetup> DesignationSetupRepository, IMapper mapper)
        {
            _DesignationSetupRepository = DesignationSetupRepository;
            _mapper = mapper;
        }
        public async Task<DesignationSetupDto> Handle(GetDesignationSetupDetailRequest request, CancellationToken cancellationToken)
        {
            var DesignationSetup = await _DesignationSetupRepository.Get(request.DesignationSetupId);
            return _mapper.Map<DesignationSetupDto>(DesignationSetup);
        }
    }
}
