using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Branch;
using Hrm.Application.DTOs.Branch;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Branch.Requests.Queries;
using Hrm.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Branch.Handlers.Queries
{
    public class GetBranchRequestHandler : IRequestHandler<GetBranchRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Branch> _BranchRepository;
        private readonly IMapper _mapper;
        public GetBranchRequestHandler(IHrmRepository<Hrm.Domain.Branch> BranchRepository, IMapper mapper)
        {
            _BranchRepository = BranchRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetBranchRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Branch> Branch = _BranchRepository.Where(x => true);

            var BranchDtos = _mapper.Map<List<BranchDto>>(Branch);

            return BranchDtos;
        }
    }
}