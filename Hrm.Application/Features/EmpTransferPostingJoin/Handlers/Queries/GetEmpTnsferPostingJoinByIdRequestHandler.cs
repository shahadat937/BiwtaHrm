using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTnsferPostingJoin;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Queries
{
    public class GetEmpTnsferPostingJoinByIdRequestHandler : IRequestHandler<GetEmpTnsferPostingJoinByIdRequest, EmpTnsferPostingJoinDto>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> _EmpTnsferPostingJoinRepository;
        private readonly IMapper _mapper;
        public GetEmpTnsferPostingJoinByIdRequestHandler(IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoinRepositoy, IMapper mapper)
        {
            _EmpTnsferPostingJoinRepository = EmpTnsferPostingJoinRepositoy;
            _mapper = mapper;
        }

        public async Task<EmpTnsferPostingJoinDto> Handle(GetEmpTnsferPostingJoinByIdRequest request, CancellationToken cancellationToken)
        {
            var EmpTnsferPostingJoin = await _EmpTnsferPostingJoinRepository.Get(request.EmpTnsferPostingJoinId);
            return _mapper.Map<EmpTnsferPostingJoinDto>(EmpTnsferPostingJoin);
        }
    }
}
