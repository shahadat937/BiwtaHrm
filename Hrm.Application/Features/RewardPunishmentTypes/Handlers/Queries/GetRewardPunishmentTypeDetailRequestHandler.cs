using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.RewardPunishmentTypes.Requests.Queries;
using Hrm.Application.DTOs.RewardPunishmentType;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.RewardPunishmentTypes.Handlers.Queries
{
    public class GetRewardPunishmentTypeDetailRequestHandler : IRequestHandler<GetRewardPunishmentTypeDetailRequest, RewardPunishmentTypeDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Domain.RewardPunishmentType> _RewardPunishmentTypeRepository;
        public GetRewardPunishmentTypeDetailRequestHandler(IHrmRepository<Domain.RewardPunishmentType> RewardPunishmentTypeRepository, IMapper mapper)
        {
            _RewardPunishmentTypeRepository = RewardPunishmentTypeRepository;
            _mapper = mapper;
        }
        public async Task<RewardPunishmentTypeDto> Handle(GetRewardPunishmentTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var RewardPunishmentType = await _RewardPunishmentTypeRepository.Get(request.RewardPunishmentTypeId);
            return _mapper.Map<RewardPunishmentTypeDto>(RewardPunishmentType);
        }
    }
}
