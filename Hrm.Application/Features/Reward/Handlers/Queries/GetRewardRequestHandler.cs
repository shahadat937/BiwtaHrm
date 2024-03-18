using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reward;
using Hrm.Application.DTOs.Reward;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Reward.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reward.Handlers.Queries
{
    public class GetRewardRequestHandler : IRequestHandler<GetRewardRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Reward> _RewardRepository;
        private readonly IMapper _mapper;
        public GetRewardRequestHandler(IHrmRepository<Hrm.Domain.Reward> RewardRepository, IMapper mapper)
        {
            _RewardRepository = RewardRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetRewardRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Reward> Reward = _RewardRepository.Where(x => true);

            var RewardDtos = await Task.Run(() => _mapper.Map<List<RewardDto>>(Reward));

            return RewardDtos;
        }
    }
}