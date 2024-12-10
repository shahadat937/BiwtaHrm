using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpRewardPunishment;
using Hrm.Application.Features.EmpRewardPunishments.Requests.Queries;
using Hrm.Application.Features.EmpShiftAssigns.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpRewardPunishments.Handlers.Queries
{
    public class GetEmpRewardPunishmentDetailsRequestHandler : IRequestHandler<GetEmpRewardPunishmentDetailsRequest, object>
    {
        private readonly IHrmRepository<EmpRewardPunishment> _EmpRewardPunishmentRepository;
        private readonly IMapper _mapper;
        public GetEmpRewardPunishmentDetailsRequestHandler(IHrmRepository<EmpRewardPunishment> EmpRewardPunishmentRepository, IMapper mapper)
        {
            _EmpRewardPunishmentRepository = EmpRewardPunishmentRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpRewardPunishmentDetailsRequest request, CancellationToken cancellationToken)
        {
            var empRewardPunishment = await _EmpRewardPunishmentRepository.Where(x => x.Id == request.Id)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.RewardPunishmentPriority)
                .Include(x => x.RewardPunishmentType)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                    .ThenInclude(x => x.DesignationSetup)
                .FirstOrDefaultAsync(cancellationToken);

            var result = _mapper.Map<EmpRewardPunishmentDto>(empRewardPunishment);

            return result;
        }
    }
}
