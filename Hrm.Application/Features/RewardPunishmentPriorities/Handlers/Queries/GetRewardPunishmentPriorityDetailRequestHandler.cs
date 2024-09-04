using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Queries;
using Hrm.Application.DTOs.RewardPunishmentPriority;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.RewardPunishmentPrioritys.Handlers.Queries
{
    public class GetRewardPunishmentPriorityDetailRequestHandler : IRequestHandler<GetRewardPunishmentPriorityDetailRequest, RewardPunishmentPriorityDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Domain.RewardPunishmentPriority> _RewardPunishmentPriorityRepository;
        public GetRewardPunishmentPriorityDetailRequestHandler(IHrmRepository<Domain.RewardPunishmentPriority> RewardPunishmentPriorityRepository, IMapper mapper)
        {
            _RewardPunishmentPriorityRepository = RewardPunishmentPriorityRepository;
            _mapper = mapper;
        }
        public async Task<RewardPunishmentPriorityDto> Handle(GetRewardPunishmentPriorityDetailRequest request, CancellationToken cancellationToken)
        {
            var RewardPunishmentPriority = await _RewardPunishmentPriorityRepository.Get(request.RewardPunishmentPriorityId);
            return _mapper.Map<RewardPunishmentPriorityDto>(RewardPunishmentPriority);
        }
    }
}
