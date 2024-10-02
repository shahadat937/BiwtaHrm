using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpRewardPunishment;
using Hrm.Application.DTOs.Institute;
using Hrm.Application.Features.EmpRewardPunishments.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpRewardPunishments.Handlers.Queries
{
    public class GetEmpRewardPunishmentByEmpIdRequestHandler : IRequestHandler<GetEmpRewardPunishmentByEmpIdRequest, object>
    {
        private readonly IHrmRepository<EmpRewardPunishment> _EmpRewardPunishmentRepository;
        private readonly IMapper _mapper;
        public GetEmpRewardPunishmentByEmpIdRequestHandler(IHrmRepository<EmpRewardPunishment> EmpRewardPunishmentRepository, IMapper mapper)
        {
            _EmpRewardPunishmentRepository = EmpRewardPunishmentRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpRewardPunishmentByEmpIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpRewardPunishment> empRewardPunishment = _EmpRewardPunishmentRepository.Where(x => x.EmpId == request.EmpId)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.RewardPunishmentPriority)
                .Include(x => x.RewardPunishmentType);

            var result = await Task.Run(() => _mapper.Map<List<EmpRewardPunishmentDto>>(empRewardPunishment));

            return result;
        }
    }
}
