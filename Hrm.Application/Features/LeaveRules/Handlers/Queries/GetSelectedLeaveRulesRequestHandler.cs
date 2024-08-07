using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.LeaveRules.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Application.Constants;
using System.Reflection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRules.Handlers.Queries
{
    public class GetSelectedLeaveRulesRequestHandler: IRequestHandler<GetSelectedLeaveRulesRequest,object>
    {
        private readonly IHrmRepository<Hrm.Domain.LeaveRules> _LeaveRulesRepository;
        private readonly IMapper _mapper;

        public GetSelectedLeaveRulesRequestHandler(IHrmRepository<Domain.LeaveRules> leaveRulesRepository, IMapper mapper)
        {
            _LeaveRulesRepository = leaveRulesRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSelectedLeaveRulesRequest request, CancellationToken cancellationToken)
        {
            Type ruleName = typeof(LeaveRule);
            FieldInfo[] fieldinfo = ruleName.GetFields(BindingFlags.Public | BindingFlags.Static);

            var leaveRulesName = fieldinfo.Select(x => new
            {
                Id = x.Name,
                Name = x.Name
            }).ToList();

            return leaveRulesName;

        }
    }
}
