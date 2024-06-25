using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Year.Requests.Queries;
using Hrm.Application.DTOs.Year;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;
using Hrm.Application.DTOs.HolidayType;

namespace Hrm.Application.Features.Year.Handlers.Queries
{
    public class GetHolidayTypeDetailRequestHandler : IRequestHandler<GetHolidayTypeDetailRequest, HolidayTypeDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Domain.HolidayType> _HolidayTypeRepository;
        public GetHolidayTypeDetailRequestHandler(IHrmRepository<Domain.HolidayType> HolidayTypeRepository, IMapper mapper)
        {
            _HolidayTypeRepository = HolidayTypeRepository;
            _mapper = mapper;
        }
        public async Task<HolidayTypeDto> Handle(GetHolidayTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var Year = await _HolidayTypeRepository.Get(request.HolidayTypeId);
            return _mapper.Map<HolidayTypeDto>(Year);
        }
    }
}
