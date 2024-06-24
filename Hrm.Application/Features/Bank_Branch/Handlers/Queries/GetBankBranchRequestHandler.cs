using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BankBranch;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.BankBranch.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IHrmRepository<Hrm.Domain.Bank> _BankRepository;
        private readonly IMapper _mapper;
        public GetBankBranchRequestHandler(IHrmRepository<Hrm.Domain.BankBranch> BankBranchRepository, IMapper mapper, IHrmRepository<Domain.Bank> BankRepository)
        {
            _BankBranchRepository = BankBranchRepository;
            _mapper = mapper;
            _BankRepository = BankRepository;
        }

        public async Task<object> Handle(GetBankBranchRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.BankBranch> BankBranchs = _BankBranchRepository.FilterWithInclude(x => true);
            BankBranchs = BankBranchs.OrderByDescending(x => x.BankBranchId);

            var BankBranchDtos = new List<BankBranchDto>();

            foreach (var BankBranch in BankBranchs)
            {
                var BankBranchDto = _mapper.Map<BankBranchDto>(BankBranch);
                var BankName = await GetBankName(BankBranch.BankId);
                BankBranchDto.BankName = BankName;
                BankBranchDtos.Add(BankBranchDto);
            }

            return BankBranchDtos;
        }

        private async Task<string?> GetBankName(int? BankId)
        {
            if (BankId.HasValue)
            {
                var Bank = await _BankRepository.Get(BankId.Value);
                return Bank?.BankName;
            }
            return null;
        }
    }
}
