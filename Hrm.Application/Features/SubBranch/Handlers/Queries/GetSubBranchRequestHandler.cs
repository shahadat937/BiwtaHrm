using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.Features.SubBranch.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Handlers.Queries
{
    public class GetSubBranchRequestHandler : IRequestHandler<GetSubBranchRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.SubBranch> _SubBranchRepository;
        private readonly IMapper _mapper;
        public GetSubBranchRequestHandler(IHrmRepository<Hrm.Domain.SubBranch> SubBranchRepository, IMapper mapper)
        {
            _SubBranchRepository = SubBranchRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSubBranchRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.SubBranch> SubBranch = _SubBranchRepository.Where(x => true);

            //var SubBranchDtos = _mapper.Map<List<SubBranchDto>>(SubBranch);

            //return SubBranchDtos;



            IQueryable<Hrm.Domain.SubBranch> SubBranch = _SubBranchRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var SubBranchDtos = await Task.Run(() => _mapper.Map<List<SubBranchDto>>(SubBranch));

            return SubBranchDtos;
        }
    }
}
