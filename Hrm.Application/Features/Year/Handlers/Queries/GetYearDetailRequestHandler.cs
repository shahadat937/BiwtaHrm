using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Year.Requests.Queries;
using Hrm.Application.DTOs.Year;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Year.Handlers.Queries
{
    public class GetYearDetailRequestHandler : IRequestHandler<GetYearDetailRequest, YearDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Domain.Year> _YearRepository;
        public GetYearDetailRequestHandler(IHrmRepository<Domain.Year> YearRepository, IMapper mapper)
        {
            _YearRepository = YearRepository;
            _mapper = mapper;
        }
        public async Task<YearDto> Handle(GetYearDetailRequest request, CancellationToken cancellationToken)
        {
            var Year = await _YearRepository.Get(request.YearId);
            return _mapper.Map<YearDto>(Year);
        }
    }
}
