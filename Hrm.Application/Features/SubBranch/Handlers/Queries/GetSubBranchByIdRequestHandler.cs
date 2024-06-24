using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.SubBranch.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Handlers.Queries
{
    public class GetSubBranchByIdRequestHandler : IRequestHandler<GetSubBranchByIdRequest, SubBranchDto>
    {

        private readonly IHrmRepository<Hrm.Domain.SubBranch> _SubBranchRepository;
        private readonly IMapper _mapper;
        public GetSubBranchByIdRequestHandler(IHrmRepository<Hrm.Domain.SubBranch> SubBranchRepositoy, IMapper mapper)
        {
            _SubBranchRepository = SubBranchRepositoy;
            _mapper = mapper;
        }

        public async Task<SubBranchDto> Handle(GetSubBranchByIdRequest request, CancellationToken cancellationToken)
        {
            var SubBranch = await _SubBranchRepository.Get(request.SubBranchId);
            return _mapper.Map<SubBranchDto>(SubBranch);
        }
    }
}
