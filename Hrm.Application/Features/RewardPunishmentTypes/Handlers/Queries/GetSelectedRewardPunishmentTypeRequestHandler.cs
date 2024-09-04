using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.RewardPunishmentTypes.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentTypes.Handlers.Queries
{ 
    public class GetSelectedRewardPunishmentTypeRequestHandler : IRequestHandler<GetSelectedRewardPunishmentTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.RewardPunishmentType> _RewardPunishmentTypeRepository;


        public GetSelectedRewardPunishmentTypeRequestHandler(IHrmRepository<Hrm.Domain.RewardPunishmentType> RewardPunishmentTypeRepository)
        {
            _RewardPunishmentTypeRepository = RewardPunishmentTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedRewardPunishmentTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.RewardPunishmentType> RewardPunishmentTypes = await _RewardPunishmentTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = RewardPunishmentTypes.Select(x => new SelectedModel 
            {
                Name = x.Name.ToString(),
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 