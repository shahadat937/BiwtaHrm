using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.PromotionTypes.Requests.Queries;
using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.PromotionTypes.Handlers.Queries
{
    public class GetPromotionTypeDetailRequestHandler : IRequestHandler<GetPromotionTypeDetailRequest, PromotionTypeDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.PromotionType> _PromotionTypeRepository;
        public GetPromotionTypeDetailRequestHandler(IHrmRepository<Hrm.Domain.PromotionType> PromotionTypeRepository, IMapper mapper)
        {
            _PromotionTypeRepository = PromotionTypeRepository;
            _mapper = mapper;
        }
        public async Task<PromotionTypeDto> Handle(GetPromotionTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var PromotionType = await _PromotionTypeRepository.Get(request.PromotionTypeId);
            return _mapper.Map<PromotionTypeDto>(PromotionType);
        }
    }
}
