using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.BankBranch.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankBranch.Handlers.Queries
{ 
    public class GetSelectedBankBranchRequestHandler : IRequestHandler<GetSelectedBankBranchRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.BankBranch> _BankBranchRepository;


        public GetSelectedBankBranchRequestHandler(IHrmRepository<Hrm.Domain.BankBranch> BankBranchRepository)
        {
            _BankBranchRepository = BankBranchRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBankBranchRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.BankBranch> BankBranchs = await _BankBranchRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = BankBranchs.Select(x => new SelectedModel 
            {
                Name = x.BankBranchName,
                Id = x.BankBranchId
            }).ToList();
            return selectModels;
        }
    }
}
 