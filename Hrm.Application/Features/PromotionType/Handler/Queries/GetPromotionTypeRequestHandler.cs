using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PromotionType;
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

        private readonly IHrmRepository<Hrm.Domain.PromotionType> _PromotionTypeRepository;
        private readonly IMapper _mapper;
        public GetPromotionTypeRequestHandler(IHrmRepository<Hrm.Domain.PromotionType> PromotionTypeRepository, IMapper mapper)
        {
            _PromotionTypeRepository = PromotionTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetPromotionTypeRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.PromotionType> PromotionTypes = _PromotionTypeRepository.Where(x => true);

            // Order blood groups by descending order
            PromotionTypes = PromotionTypes.OrderByDescending(x => x.PromotionTypeId);

            // Map the ordered blood groups to PromotionTypeDto
            var PromotionTypeDtos = _mapper.Map<List<PromotionTypeDto>>(PromotionTypes.ToList());

            return PromotionTypeDtos;
        }
    }
}