using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.RewardPunishmentType;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.RewardPunishmentTypes.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Domain;

namespace Hrm.Application.Features.RewardPunishmentTypes.Handlers.Queries
{
    public class GetRewardPunishmentTypeRequestHandler : IRequestHandler<GetRewardPunishmentTypeRequest, object>
    {

        private readonly IHrmRepository<Domain.RewardPunishmentType> _RewardPunishmentTypeRepository;
        private readonly IMapper _mapper;
        public GetRewardPunishmentTypeRequestHandler(IHrmRepository<Hrm.Domain.RewardPunishmentType> RewardPunishmentTypeRepository, IMapper mapper)
        {
            _RewardPunishmentTypeRepository = RewardPunishmentTypeRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetRewardPunishmentTypeRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.RewardPunishmentType> RewardPunishmentTypes = _RewardPunishmentTypeRepository.Where(x => true);

            RewardPunishmentTypes = RewardPunishmentTypes.OrderByDescending(x => x.Id);

            var RewardPunishmentTypeDtos = _mapper.Map<List<RewardPunishmentTypeDto>>(RewardPunishmentTypes.ToList());

            return RewardPunishmentTypeDtos;
        }
    }
}
