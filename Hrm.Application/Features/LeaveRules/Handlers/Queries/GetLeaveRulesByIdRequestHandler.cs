using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.LeaveRules;
using Hrm.Application.Features.LeaveRules.Requests.Queries;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRules.Handlers.Queries
{
    public class GetLeaveRulesByIdRequestHandler: IRequestHandler<GetLeaveRulesByIdRequest, LeaveRulesDto>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveRules> _LeaveRulesRepository;
        private readonly IMapper _mapper;
        public GetLeaveRulesByIdRequestHandler(IHrmRepository<Hrm.Domain.LeaveRules> LeaveRulesRepository, IMapper mapper)
        {
            _LeaveRulesRepository = LeaveRulesRepository;
            _mapper = mapper;
        }

        public async Task<LeaveRulesDto> Handle(GetLeaveRulesByIdRequest request, CancellationToken cancellationToken)
        {
            var leaveRules = await _LeaveRulesRepository.Get(request.LeaveRulesId);

            if(leaveRules==null)
            {
                throw new NotFoundException(nameof(leaveRules), request.LeaveRulesId);
            }

            var leaveRulesDto = _mapper.Map<LeaveRulesDto>(leaveRules);

            return leaveRulesDto;
        }
    }
}
