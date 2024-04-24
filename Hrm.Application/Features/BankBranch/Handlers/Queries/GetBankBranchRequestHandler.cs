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
    public class GetBankBranchRequestHandler : IRequestHandler<GetBankBranchRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.BankBranch> _BankBranchRepository;
        private readonly IMapper _mapper;
        public GetBankBranchRequestHandler(IHrmRepository<Hrm.Domain.BankBranch> BankBranchRepository, IMapper mapper)
        {
            _BankBranchRepository = BankBranchRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetBankBranchRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.BankBranch> BankBranchs = _BankBranchRepository.Where(x => true);

            var BankBranchDtos = _mapper.Map<List<BankBranchDto>>(BankBranchs);

            return BankBranchDtos;
        }
    }
}
