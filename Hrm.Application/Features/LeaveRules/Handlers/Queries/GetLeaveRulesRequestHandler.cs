using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveRules;
using Hrm.Application.Features.LeaveRules.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRules.Handlers.Queries
{
    public class GetLeaveRulesRequestHandler: IRequestHandler<GetLeaveRulesRequest,List<LeaveRulesDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveRules> _LeaveRulesRepository;
        private readonly IMapper _mapper;
        public GetLeaveRulesRequestHandler (IHrmRepository<Hrm.Domain.LeaveRules> LeaveRulesRepository, IMapper mapper)
        {
            _LeaveRulesRepository = LeaveRulesRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaveRulesDto>> Handle(GetLeaveRulesRequest request, CancellationToken cancellationToken)
        {
            var leaveRules = await _LeaveRulesRepository.Where(x => true)
                                    .Include(e => e.LeaveType).ToListAsync();

            var leaveRulesDtos = _mapper.Map<List<LeaveRulesDto>>(leaveRules);

            return leaveRulesDtos;
        }
    }
}
