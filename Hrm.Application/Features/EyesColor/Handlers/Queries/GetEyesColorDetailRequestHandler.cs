using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.EyesColors.Requests.Queries;
using Hrm.Application.DTOs.EyesColor;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.EyesColors.Handlers.Queries
{
    public class GetEyesColorDetailRequestHandler : IRequestHandler<GetEyesColorDetailRequest, EyesColorDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.EyesColor> _EyesColorRepository;
        public GetEyesColorDetailRequestHandler(IHrmRepository<Hrm.Domain.EyesColor> EyesColorRepository, IMapper mapper)
        {
            _EyesColorRepository = EyesColorRepository;
            _mapper = mapper;
        }
        public async Task<EyesColorDto> Handle(GetEyesColorDetailRequest request, CancellationToken cancellationToken)
        {
            var EyesColor = await _EyesColorRepository.Get(request.EyesColorId);
            return _mapper.Map<EyesColorDto>(EyesColor);
        }
    }
}
