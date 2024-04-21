using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Overall_EV_Promotion.Handlers.Queries
{
    public class GetOverall_EV_PromotionRequestHandler : IRequestHandler<GetOverall_EV_PromotionRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Overall_EV_Promotion> _Overall_EV_PromotionRepository;
        private readonly IMapper _mapper;
        public GetOverall_EV_PromotionRequestHandler(IHrmRepository<Hrm.Domain.Overall_EV_Promotion> Overall_EV_PromotionRepository, IMapper mapper)
        {
            _Overall_EV_PromotionRepository = Overall_EV_PromotionRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetOverall_EV_PromotionRequest request, CancellationToken cancellationToken)
        {
            // Fetch blood groups from repository
            IQueryable<Hrm.Domain.Overall_EV_Promotion> Overall_EV_Promotions = _Overall_EV_PromotionRepository.Where(x => true);

            // Order blood groups by descending order
            Overall_EV_Promotions = Overall_EV_Promotions.OrderByDescending(x => x.Overall_EV_PromotionId);

            // Map the ordered blood groups to Overall_EV_PromotionDto
            var Overall_EV_PromotionDtos = _mapper.Map<List<Overall_EV_PromotionDto>>(Overall_EV_Promotions.ToList());

            return Overall_EV_PromotionDtos;
        }
    }
}
