using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ShiftType;
using Hrm.Application.Features.ShiftTypes.Requests.Queries;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.ShiftTypes.Handlers.Queries
{
    public class GetShiftTypesDetailRequestHandler : IRequestHandler<GetShiftTypeDetailRequest, ShiftTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<ShiftType> _ShiftTypeRepository;
        public GetShiftTypesDetailRequestHandler(IHrmRepository<ShiftType> ShiftTypeRepository, IMapper mapper)
        {
            _ShiftTypeRepository = ShiftTypeRepository;
            _mapper = mapper;
        }
        public async Task<ShiftTypeDto> Handle(GetShiftTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var ShiftType = await _ShiftTypeRepository.Get(request.Id);
            return _mapper.Map<ShiftTypeDto>(ShiftType);
        }
    }
}
