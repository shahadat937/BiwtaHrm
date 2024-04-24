using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankBranch;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.BankBranch.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankBranch.Handlers.Queries
{
    public class GetBankBranchByIdRequestHandler : IRequestHandler<GetBankBranchByIdRequest, BankBranchDto>
    {

        private readonly IHrmRepository<Hrm.Domain.BankBranch> _BankBranchRepository;
        private readonly IMapper _mapper;
        public GetBankBranchByIdRequestHandler(IHrmRepository<Hrm.Domain.BankBranch> BankBranchRepositoy, IMapper mapper)
        {
            _BankBranchRepository = BankBranchRepositoy;
            _mapper = mapper;
        }

        public async Task<BankBranchDto> Handle(GetBankBranchByIdRequest request, CancellationToken cancellationToken)
        {
            var BankBranch = await _BankBranchRepository.Get(request.BankBranchId);
            return _mapper.Map<BankBranchDto>(BankBranch);
        }
    }
}
