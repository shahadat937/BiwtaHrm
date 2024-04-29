using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Branch;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Branch.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Branch.Handlers.Queries
{
    public class GetBranchByIdRequestHandler : IRequestHandler<GetBranchByIdRequest, BranchDto>
    {

        private readonly IHrmRepository<Hrm.Domain.OfficeBranch> _BranchRepository;
        private readonly IMapper _mapper;
        public GetBranchByIdRequestHandler(IHrmRepository<Hrm.Domain.OfficeBranch> BranchRepositoy, IMapper mapper)
        {
            _BranchRepository = BranchRepositoy;
            _mapper = mapper;
        }

        public async Task<BranchDto> Handle(GetBranchByIdRequest request, CancellationToken cancellationToken)
        {
            var Branch = await _BranchRepository.Get(request.BranchId);
            return _mapper.Map<BranchDto>(Branch);
        }
    }
}
