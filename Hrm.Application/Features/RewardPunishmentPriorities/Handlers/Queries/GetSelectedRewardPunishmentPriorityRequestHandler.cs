using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentPrioritys.Handlers.Queries
{ 
    public class GetSelectedRewardPunishmentPriorityRequestHandler : IRequestHandler<GetSelectedRewardPunishmentPriorityRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.RewardPunishmentPriority> _RewardPunishmentPriorityRepository;


        public GetSelectedRewardPunishmentPriorityRequestHandler(IHrmRepository<Hrm.Domain.RewardPunishmentPriority> RewardPunishmentPriorityRepository)
        {
            _RewardPunishmentPriorityRepository = RewardPunishmentPriorityRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedRewardPunishmentPriorityRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.RewardPunishmentPriority> RewardPunishmentPrioritys = await _RewardPunishmentPriorityRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = RewardPunishmentPrioritys.Select(x => new SelectedModel 
            {
                Name = x.Name.ToString(),
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 