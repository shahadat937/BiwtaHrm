using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Features.PromotionType.Request.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PromotionType.Handler.Queries
{
    public class GetPromotionTypeRequestHandler : IRequestHandler<GetPromotionTypeRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.PromotionType> _promotionTypeRepository;
        private readonly IMapper _mapper;

        public GetPromotionTypeRequestHandler(IHrmRepository<Hrm.Domain.PromotionType> promotionTypeRepository, IMapper mapper)
        {
            _promotionTypeRepository = promotionTypeRepository;
            _mapper = mapper;
        }


        public async Task<object> Handle(GetPromotionTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.PromotionType> promotionTypes = _promotionTypeRepository.Where(x => true);
            var PromotionTypeDtos = await Task.Run(() => _mapper.Map<List<PromotionTypeDto>>(promotionTypes));

            return PromotionTypeDtos;
        }
    }
}
