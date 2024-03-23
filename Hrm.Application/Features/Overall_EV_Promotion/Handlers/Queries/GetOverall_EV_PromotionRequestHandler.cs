using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Queries;
using Hrm.Application.Models;
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
            IQueryable<Hrm.Domain.Overall_EV_Promotion> Overall_EV_Promotion = _Overall_EV_PromotionRepository.Where(x => true);

            var Overall_EV_PromotionDtos = await Task.Run(() => _mapper.Map<List<Overall_EV_PromotionDto>>(Overall_EV_Promotion));

            return Overall_EV_PromotionDtos;
        }
    }
}