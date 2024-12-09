using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpRewardPunishment;
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
    public class GetAllEmpRewardPunishmentRequestHandler : IRequestHandler<GetAllEmpRewardPunishmentRequest, object>
    {
        private readonly IHrmRepository<EmpRewardPunishment> _EmpRewardPunishmentRepository;
        private readonly IMapper _mapper;
        public GetAllEmpRewardPunishmentRequestHandler(IHrmRepository<EmpRewardPunishment> EmpRewardPunishmentRepository, IMapper mapper)
        {
            _EmpRewardPunishmentRepository = EmpRewardPunishmentRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllEmpRewardPunishmentRequest request, CancellationToken cancellationToken)
        {
            IQueryable<EmpRewardPunishment> empRewardPunishment = _EmpRewardPunishmentRepository.Where(x => true)
                .Include(x => x.EmpBasicInfo)
                .Include(x => x.RewardPunishmentPriority)
                .Include(x => x.RewardPunishmentType)
                .Include(x => x.Department)
                .Include(x => x.Section)
                .Include(x => x.Designation)
                    .ThenInclude(x => x.DesignationSetup);

            var result = await Task.Run(() => _mapper.Map<List<EmpRewardPunishmentDto>>(empRewardPunishment));

            return result;
        }
    }
}
