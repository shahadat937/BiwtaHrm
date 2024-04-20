using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.BankAccountType.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BankAccountType.Handlers.Queries
{ 
    public class GetSelectedBankAccountTypeRequestHandler : IRequestHandler<GetSelectedBankAccountTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.BankAccountType> _BankAccountTypeRepository;


        public GetSelectedBankAccountTypeRequestHandler(IHrmRepository<Hrm.Domain.BankAccountType> BankAccountTypeRepository)
        {
            _BankAccountTypeRepository = BankAccountTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBankAccountTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.BankAccountType> BankAccountTypes = await _BankAccountTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = BankAccountTypes.Select(x => new SelectedModel 
            {
                Name = x.BankAccountTypeName,
                Id = x.BankAccountTypeId
            }).ToList();
            return selectModels;
        }
    }
}
 