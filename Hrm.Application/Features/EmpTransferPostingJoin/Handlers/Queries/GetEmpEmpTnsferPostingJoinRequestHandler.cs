using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTnsferPostingJoin;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Queries
{
    public class GetEmpTnsferPostingJoinRequestHandler : IRequestHandler<GetEmpTnsferPostingJoinRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> _EmpTnsferPostingJoinRepository;
        private readonly IMapper _mapper;
        public GetEmpTnsferPostingJoinRequestHandler(IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoinRepository, IMapper mapper)
        {
            _EmpTnsferPostingJoinRepository = EmpTnsferPostingJoinRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEmpTnsferPostingJoinRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoin = _EmpTnsferPostingJoinRepository.Where(x => true);

            var EmpTnsferPostingJoinDtos = _mapper.Map<List<EmpTnsferPostingJoinDto>>(EmpTnsferPostingJoin);

            return EmpTnsferPostingJoinDtos;
        }
    }
}
