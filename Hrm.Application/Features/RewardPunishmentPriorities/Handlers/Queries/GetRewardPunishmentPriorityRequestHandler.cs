using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.RewardPunishmentPriority;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Domain;

namespace Hrm.Application.Features.RewardPunishmentPrioritys.Handlers.Queries
{
    public class GetRewardPunishmentPriorityRequestHandler : IRequestHandler<GetRewardPunishmentPriorityRequest, object>
    {

        private readonly IHrmRepository<Domain.RewardPunishmentPriority> _RewardPunishmentPriorityRepository;
        private readonly IMapper _mapper;
        public GetRewardPunishmentPriorityRequestHandler(IHrmRepository<Hrm.Domain.RewardPunishmentPriority> RewardPunishmentPriorityRepository, IMapper mapper)
        {
            _RewardPunishmentPriorityRepository = RewardPunishmentPriorityRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetRewardPunishmentPriorityRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.RewardPunishmentPriority> RewardPunishmentPrioritys = _RewardPunishmentPriorityRepository.Where(x => true);

            RewardPunishmentPrioritys = RewardPunishmentPrioritys.OrderByDescending(x => x.Id);

            var RewardPunishmentPriorityDtos = _mapper.Map<List<RewardPunishmentPriorityDto>>(RewardPunishmentPrioritys.ToList());

            return RewardPunishmentPriorityDtos;
        }
    }
}
