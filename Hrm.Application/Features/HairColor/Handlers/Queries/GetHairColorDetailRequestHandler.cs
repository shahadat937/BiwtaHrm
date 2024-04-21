using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.HairColors.Requests.Queries;
using Hrm.Application.DTOs.HairColor;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.HairColors.Handlers.Queries
{
    public class GetHairColorDetailRequestHandler : IRequestHandler<GetHairColorDetailRequest, HairColorDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.HairColor> _HairColorRepository;
        public GetHairColorDetailRequestHandler(IHrmRepository<Hrm.Domain.HairColor> HairColorRepository, IMapper mapper)
        {
            _HairColorRepository = HairColorRepository;
            _mapper = mapper;
        }
        public async Task<HairColorDto> Handle(GetHairColorDetailRequest request, CancellationToken cancellationToken)
        {
            var HairColor = await _HairColorRepository.Get(request.HairColorId);
            return _mapper.Map<HairColorDto>(HairColor);
        }
    }
}
